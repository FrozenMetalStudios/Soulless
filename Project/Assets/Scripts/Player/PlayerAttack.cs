using UnityEngine;
using System.Collections;
using ARK.Player.Ability;
using Assets.Scripts.Utility;
using ARK.Player.Ability.Effects;
using Assets.Scripts.Player;

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
        if (Input.GetButtonDown(ButtonNames.BasicAttack1))
        {
            abilityToCast = player.DetermineAbility(eEquippedSlot.AttackSlot1);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown(ButtonNames.BasicAttack2))
        {
            abilityToCast = player.DetermineAbility(eEquippedSlot.AttackSlot2);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown(ButtonNames.Ability1))
        {
            abilityToCast = player.DetermineAbility(eEquippedSlot.SpellSlot1);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown(ButtonNames.Ability2))
        {
            abilityToCast = player.DetermineAbility(eEquippedSlot.SpellSlot2);

            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown(ButtonNames.Ability3))
        {
            abilityToCast = player.DetermineAbility(eEquippedSlot.SpellSlot3);
            CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown(ButtonNames.Ultimate))
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

            switch (ability.type)
            {
                case eAbilityType.Melee:
                    //set the correct trigger
                    abilityTrigger.enabled = true;
                    //update the triggers damage with abilities damage
                    abilityTrigger.GetComponent<SkillTrigger>().CastedAbility = ability;
                    break;
                case eAbilityType.Ranged:
                    //set the correct trigger
                    abilityTrigger.enabled = true;
                    //update the triggers damage with abilities damage
                    abilityTrigger.GetComponent<SkillTrigger>().CastedAbility = ability;
                    break;
                case eAbilityType.Buff:
                    CastBuffEffect(ability);
                    break;
                case eAbilityType.Mobility:
                    break;
                case eAbilityType.Transform:
                    break;
                default:
                    break;

            }
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
    private void CastBuffEffect(Ability ability)
    {
        switch (ability.effect.effectkey)
        {
            case eEffectType.DamageBuff:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Amplifier!");
                StartCoroutine(PerformDamageBuff(ability));
                break;
            case eEffectType.Movement:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Movement!");
                StartCoroutine(PerformMovementBuff(ability.effect.statistics));
                break;
            case eEffectType.undefined:
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Effect is undefined!");
                break;
            default:
                break;
        }
    }
    #region Buff Effects
    public IEnumerator PerformDamageBuff(Ability ability)
    {
        //create temp variable that holds original damage

        //apply modifier
        player.EffectDamageMultipler = ability.effect.statistics.buff.percentage;
        ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage buff applied! " + ability.effect.statistics.duration + "secs with percentage buff: " + ability.effect.statistics.buff.percentage);
        yield return new WaitForSeconds(ability.effect.statistics.duration);
        player.EffectDamageMultipler = 1;
        ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Buff Off!");

        //reset the stat
    }
    public IEnumerator PerformMovementBuff(EffectStatistics stats)
    {
        float movespeed = 0;
        movespeed = this.GetComponent<PlayerController>().maxSpeed;
        this.GetComponent<PlayerController>().maxSpeed = movespeed * stats.buff.percentage;
        ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Player is slowed for " + stats.duration + "secs with percentage slow: " + stats.buff.percentage);
        yield return new WaitForSeconds(stats.duration);
        this.GetComponent<PlayerController>().maxSpeed = movespeed;
    }
    #endregion
}
