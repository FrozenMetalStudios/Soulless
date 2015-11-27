using UnityEngine;
using System.Collections;
using PlayerAbilities;

public class PlayerAttack : MonoBehaviour {

    public Collider2D meleeAttackTrigger;
    private PlayerProfile player;
    private Rigidbody2D rigidBody2D;

    private Animator anim;
    private Abilities abilityToCast;
    private AnimatorStateInfo currentState;

    private int state;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerProfile>();
        meleeAttackTrigger.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //CoolDownHandler();
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

    private void CastAbility(Abilities ability)
    {
        if (ability.isOffCooldown)
        {
            anim.Play(ability.InputTag, 0);
            meleeAttackTrigger.enabled = true;
            meleeAttackTrigger.SendMessage("updateDamage", ability.Damage);
        }
        else
        {
            //print(ability.InputTag + " not off ability cooldown yet!");
        }

    }

    IEnumerator CooldownHandler(Abilities ability)
    {
        //print(ability.InputTag + " on " + ability.CoolDown+" second cooldown");
        ability.isOffCooldown = false;
        yield return new WaitForSeconds(ability.CoolDown);
        ability.isOffCooldown = true;
        //print(ability.InputTag + " is off cooldown");


    }
}
