using UnityEngine;
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
    private AbilityTest abilityToCast;            //The ability player is trying to cast
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
        if (Input.GetButtonDown("BasicAttack1"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(eEquippedSlot.Attack1);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButtonDown("BasicAttack2"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(eEquippedSlot.Attack2);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButtonDown("Ability1"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(eEquippedSlot.Spell1);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButtonDown("Ability2"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(eEquippedSlot.Spell2);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButtonDown("Ability3"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(eEquippedSlot.Spell3);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButtonDown("Ultimate"))
        {
            //check to see if the ability is off cooldown
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
        }
        else
        {
            string message = ability.animationTag + " not off ability cooldown yet!";
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.Warning, message);
        }
        //update the players hud
        

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
