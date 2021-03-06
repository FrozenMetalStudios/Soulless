﻿using UnityEngine;
using System.Collections;
using PlayerAbilityTest;
using Assets.Scripts.Utility;

//Player Attack
//<summary>
// Manage players combat and attacking
//</summary>
public class PlayerAttack : MonoBehaviour
{
    public Collider2D meleeAttackTrigger;       //Melee attack range
    public PlayerProfile player;                //Players profile

    private Animator anim;                      //Animator
    private AnimatorStateInfo currentState;     //The current state the players animator is in

    private int state;

    #region Unity callbacks
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        //player = GetComponent<PlayerProfile>();
        meleeAttackTrigger.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Handles the different inputs the player can use for combat
        AbilityTest abilityToCast;
        if (Input.GetButtonDown("BasicAttack1"))
        {
            abilityToCast = player.determineAbility(eEquippedSlot.Attack1);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown("BasicAttack2"))
        {
            abilityToCast = player.determineAbility(eEquippedSlot.Attack2);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown("Ability1"))
        {
            abilityToCast = player.determineAbility(eEquippedSlot.Spell1);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown("Ability2"))
        {
            abilityToCast = player.determineAbility(eEquippedSlot.Spell2);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown("Ability3"))
        {
            abilityToCast = player.determineAbility(eEquippedSlot.Spell3);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown("Ultimate"))
        {
        }
        else
        {
            meleeAttackTrigger.enabled = false;
        }
    }
    #endregion

    //Casts players ability
    private void CastAbility(AbilityTest ability)
    {
        //Check to see ability is off cooldown
        if (ability.offCooldown && (player.playerHUD.energySlider.value - ability.energy) > 0)
        {
            //play the correct animation
            anim.Play(ability.animationTag, 0);
            //set the correct trigger
            meleeAttackTrigger.enabled = true;
            //update the triggers damage with abilities damage
            meleeAttackTrigger.SendMessage(CombatActions.UpdateDamage, ability.damage);
            player.playerHUD.PlayerCastedAbility(ability);
            StartCoroutine(CooldownHandler(ability));
        }
        else
        {
            string message = ability.animationTag + " not off ability cooldown yet!";
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.Warning, message);
        }
    }

    //Coroutine used for ability cooldown
    IEnumerator CooldownHandler(AbilityTest ability)
    {
        ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, ability.animationTag + " on " + ability.cooldown.ToString()+" second cooldown");
        ability.offCooldown = false;
        yield return new WaitForSeconds(ability.cooldown);
        ability.offCooldown = true;
        ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, ability.animationTag + " is off cooldown");
    }
}
