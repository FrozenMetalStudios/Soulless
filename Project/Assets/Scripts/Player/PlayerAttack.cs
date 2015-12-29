using UnityEngine;
using System.Collections;
using PlayerAbilities;

//Player Attack
//<summary>
// Manage players combat and attacking
//</summary>
public class PlayerAttack : MonoBehaviour {

    public Collider2D meleeAttackTrigger;       //Melee attack range
    public PlayerProfile player;                //Players profile

    private Animator anim;                      //Animator
    private Abilities abilityToCast;            //The ability player is trying to cast
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
            abilityToCast = player.determineAbility(ePlayerAbilities.BasicAttack1);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButton("BasicAttack2"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(ePlayerAbilities.BasicAttack2);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButton("Ability1"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(ePlayerAbilities.Spell1);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButton("Ability2"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(ePlayerAbilities.Spell2);
            CastAbility(abilityToCast);
            StartCoroutine(CooldownHandler(abilityToCast));
        }
        else if (Input.GetButton("Ability3"))
        {
            //check to see if the ability is off cooldown
            abilityToCast = player.determineAbility(ePlayerAbilities.Spell3);
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
    private void CastAbility(Abilities ability)
    {
        //Check to see ability is off cooldown
        if (ability.isOffCooldown)
        {
            //play the correct animation
            anim.Play(ability.InputTag, 0);
            //set the correct trigger
            meleeAttackTrigger.enabled = true;
            //update the triggers damage with abilities damage
            meleeAttackTrigger.SendMessage("updateDamage", ability.Damage);
            //update the players hud
            player.playerHUD.AbilityCasted(ability);
        }
        else
        {
            //print(ability.InputTag + " not off ability cooldown yet!");
        }

    }

    //Coroutine used for ability cooldown
    IEnumerator CooldownHandler(Abilities ability)
    {
        //print(ability.InputTag + " on " + ability.CoolDown+" second cooldown");
        ability.isOffCooldown = false;
        yield return new WaitForSeconds(ability.CoolDown);
        ability.isOffCooldown = true;
        //print(ability.InputTag + " is off cooldown");
    }
}
