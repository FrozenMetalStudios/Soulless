﻿using UnityEngine;
using System.Collections;
using ARK.Player.Ability;
using Assets.Scripts.Utility;

//Player Attack
//<summary>
// Manage players combat and attacking
//</summary>
public class PlayerAttack : MonoBehaviour
{
    public Collider2D abilityTrigger;       //Melee attack range
    public PlayerProfile player;                //Players profile

    private Animator anim;                      //Animator
    private AnimatorStateInfo currentState;     //The current state the players animator is in

    private int state;

    #region Unity callbacks
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        //player = GetComponent<PlayerProfile>();
        abilityTrigger.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Handles the different inputs the player can use for combat
        Ability abilityToCast;
        if (Input.GetButtonDown("BasicAttack1"))
        {
            abilityToCast = player.DetermineAbility(eEquippedSlot.AttackSlot1);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown("BasicAttack2"))
        {
            abilityToCast = player.DetermineAbility(eEquippedSlot.AttackSlot2);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown("Ability1"))
        {
            abilityToCast = player.DetermineAbility(eEquippedSlot.SpellSlot1);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown("Ability2"))
        {
            abilityToCast = player.DetermineAbility(eEquippedSlot.SpellSlot2);

            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown("Ability3"))
        {
            abilityToCast = player.DetermineAbility(eEquippedSlot.SpellSlot3);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown("Ultimate"))
        {
        }
        else
        {
            abilityTrigger.enabled = false;
        }
    }
    #endregion

    //Casts players ability
    private void CastAbility(Ability ability)
    {
        //Check to see ability is off cooldown
        if (ability.offCooldown && (player.playerHUD.energySlider.value - ability.statistics.energy) > 0)
        {
            //play the correct animation
            anim.Play(ability.information.animationKey, 0);
            //set the correct trigger
            abilityTrigger.enabled = true;

            //update the triggers damage with abilities damage
            abilityTrigger.GetComponent<SkillTrigger>().CastedAbility = ability;
            player.playerHUD.PlayerCastedAbility(ability);
            StartCoroutine(CooldownHandler(ability));
        }
        else
        {
            string message = ability.information.animationKey + " not off ability cooldown yet!";
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.Info, message);
        }
    }

    //Coroutine used for ability cooldown
    IEnumerator CooldownHandler(Ability ability)
    {
        ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.Info, ability.information.animationKey + " on " + ability.statistics.cooldown.ToString()+" second cooldown");
        ability.offCooldown = false;
        yield return new WaitForSeconds(ability.statistics.cooldown);
        ability.offCooldown = true;
        ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.Info, ability.information.animationKey + " is off cooldown");
    }
}
