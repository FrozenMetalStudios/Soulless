﻿using UnityEngine;
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
    private EffectManager _EffectManager;

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
        _EffectManager = GetComponent<EffectManager>();
    }


    //Damages Collided enemy object
    public void DamageEnemy(Collider2D collider, Ability ability)
    {
        // ARKTODO: Think about the implications and performance of this call
        // ARKNOTE: To improve the performance, we can replace this call with enemycollider.GetComponent<Health>().TakeDamage(damage) 
        // ARKTODO: Create a generic health class which enemy and player can inherit, so the above call can be generic

       if(ability.effect.effectkey != eEffectType.undefined)
        {
            _EffectManager.CastEffect(collider, ability);
        }
       else
        {
            collider.GetComponent<Health>().TakeDamage(ability.statistics.damage);
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
