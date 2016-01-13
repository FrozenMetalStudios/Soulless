using UnityEngine;
using System.Collections;
using PlayerAbilities;
using Assets.Scripts.Utility.Timers;
//Player Attack
//<summary>
// Manage players combat and attacking
//</summary>
public class PlayerAttack : MonoBehaviour {

    public Collider2D meleeAttackTrigger;       //Melee attack range
    public PlayerProfile player;                //Players profile

    private Animator anim;                      //Animator
    private Ability abilityToCast;            //The ability player is trying to cast
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
        if (Input.GetButton("BasicAttack1"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(eEquippedSlot.Attack1);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButton("BasicAttack2"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(eEquippedSlot.Attack2);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButton("Ability1"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(eEquippedSlot.Spell1);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButton("Ability2"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(eEquippedSlot.Spell2);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButton("Ability3"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(eEquippedSlot.Spell3);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButton("Ultimate"))
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
    private void CastAbility(Ability ability)
    {
        //Check to see ability is off cooldown
        if (ability.offCooldown)
        {
            //play the correct animation
            anim.Play(ability.animationTag, 0);
            //set the correct trigger
            meleeAttackTrigger.enabled = true;
            //update the triggers damage with abilities damage
            meleeAttackTrigger.SendMessage("updateDamage", ability.damage);
        }
        else
        {
            //print(ability.InputTag + " not off ability cooldown yet!");
        }
        //update the players hud
        player.playerHUD.HandleCooldown(ability);

    }

    //Coroutine used for ability cooldown
    IEnumerator CooldownHandler(Ability ability)
    {
        //print(ability.InputTag + " on " + ability.CoolDown+" second cooldown");
        ability.offCooldown = false;
        yield return new WaitForSeconds(ability.cooldown);
        ability.offCooldown = true;
        //print(ability.InputTag + " is off cooldown");
    }
}
