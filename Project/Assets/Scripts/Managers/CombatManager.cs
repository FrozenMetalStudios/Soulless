using UnityEngine;
using ARK.Player.Ability;
using ARK.Player.Ability.Effects;
using Assets.Scripts.Utility;


//Combat Manager
//<summary>
//Manages all game combat with the different types of enemies( trash, bosses, etc) and global combat
public class CombatManager : MonoBehaviour
{
    // --------------------------------------------------------------------
    static CombatManager _Singleton = null;
    private Ability ability;

    // --------------------------------------------------------------------
    public static CombatManager Singleton
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
                                 "CombatManager: Multiple CombatManager violate Singleton pattern.");
        }
        _Singleton = this;
    }


    //Damages Collided enemy object
    public void DamageEnemy(Collider2D collider, Ability ability)
    {
        // ARKTODO: Think about the implications and performance of this call
        // ARKNOTE: To improve the performance, we can replace this call with enemycollider.GetComponent<Health>().TakeDamage(damage) 
        // ARKTODO: Create a generic health class which enemy and player can inherit, so the above call can be generic
        collider.SendMessageUpwards(CombatActions.TakeDamage, ability.Statistics.damage);

        switch (ability.Effect.effectkey)
        {
            case eEffectType.DamageAmp:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Amplifier!");
                collider.GetComponent<Effect>().Cast(collider);
                break;
            case eEffectType.DamageOverTime:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Over Time!");
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
                collider.GetComponent<Health>().TakeDamage(ability.Statistics.damage);
                break;
            default:
                collider.GetComponent<Health>().TakeDamage(ability.Statistics.damage);
                break;
        }
    }
    public void DamageEnemy(Collider2D collider, int dmg)
    {
        // ARKTODO: Think about the implications and performance of this call
        // ARKNOTE: To improve the performance, we can replace this call with enemycollider.GetComponent<Health>().TakeDamage(damage) 
        // ARKTODO: Create a generic health class which enemy and player can inherit, so the above call can be generic
        collider.SendMessageUpwards(CombatActions.TakeDamage, dmg);
    }
}
