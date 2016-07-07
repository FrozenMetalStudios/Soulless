using UnityEngine;
using ARK.Player.Ability;
using ARK.Player.Ability.Effects;
using Assets.Scripts.Utility;
using System.Collections;


//Combat Manager
//<summary>
//Manages all game combat with the different types of enemies( trash, bosses, etc) and global combat
public class EffectManager : MonoBehaviour
{
    // --------------------------------------------------------------------
    static EffectManager _Singleton = null;
    private Ability ability;

    // --------------------------------------------------------------------
    public static EffectManager Singleton
    {
        get { return _Singleton; }
    }

    public Ability CastedAbility
    {
        get { return ability; }
        set { ability = value; }
    }

    void Awake()
    {
        // Ensure only 1 singleton
        if (null != _Singleton)
        {
            ARKLogger.LogMessage(eLogCategory.General,
                                 eLogLevel.System,
                                 "EffectManager: Multiple EffectManager violate Singleton pattern.");
        }
        _Singleton = this;
    }


    //Damages Collided enemy object
    public void CastEffect(Collider2D collider, Ability ability)
    {
        // ARKTODO: Think about the implications and performance of this call
        // ARKNOTE: To improve the performance, we can replace this call with enemycollider.GetComponent<Health>().TakeDamage(damage) 
        // ARKTODO: Create a generic health class which enemy and player can inherit, so the above call can be generic

        switch (ability.effect.effectkey)
        {
            case eEffectType.DamageAmp:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Amplifier!");
                break;
            case eEffectType.DamageOverTime:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Over Time!");
               
                StartCoroutine(PerformDoT(collider, ability.effect.statistics));
                break;
            case eEffectType.Debuff:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Debuff!");
                break;
            case eEffectType.Slow:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Slow");
                break;
            case eEffectType.Stun:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Stun");
                break;
            case eEffectType.undefined:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Effect is undefined!");
                collider.GetComponent<Health>().TakeDamage(ability.statistics.damage);
                break;
            default:
                collider.GetComponent<Health>().TakeDamage(ability.statistics.damage);
                break;
        }
    }

    public IEnumerator PerformDoT(Collider2D target, EffectStatistics dot)
    {
        Health targetHealth = target.GetComponent<Health>();
        float amountDamaged = 0;
        float damagePerLoop = dot.damage / dot.duration;

        while (amountDamaged < dot.damage)
        {
            targetHealth.TakeDamage(damagePerLoop);
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Target damaged: " + damagePerLoop);
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(dot.rate);
        }
        
    }

}
