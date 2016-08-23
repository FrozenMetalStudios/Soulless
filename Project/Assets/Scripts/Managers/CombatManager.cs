using UnityEngine;
using System.Collections;
using ARK.Player.Ability;
using ARK.Player.Ability.Effects;
using Assets.Scripts.Utility;


//Combat Manager
//<summary>
//Manages all game combat with the different types of enemies( trash, bosses, etc) and global combat
namespace Assets.Scripts.Managers
{
    public class CombatManager : MonoBehaviour
    {
        // --------------------------------------------------------------------
        static CombatManager _Singleton = null;
        private Ability ability;
        private EffectManager _EffectManager;
        public PlayerProfile Player;

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

        
        /// <summary>
        /// Player Damages Target Collider using the given Ability 
        /// </summary>
        /// <param name="collider">Target Collider</param>
        /// <param name="ability">Damaging Ability</param>
        public void DamageEnemy(Collider2D collider, Ability ability)
        {
            // ARKTODO: Think about the implications and performance of this call
            // ARKNOTE: To improve the performance, we can replace this call with enemycollider.GetComponent<Health>().TakeDamage(damage) 
            // ARKTODO: Create a generic health class which enemy and player can inherit, so the above call can be generic

            if (ability.effect.effectkey != eEffectType.undefined)
            {
                _EffectManager.CastTargetEffect(collider, ability);
            }
            else
            {
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage : " + ability.statistics.damage * Player.EffectDamageMultipler);
                // when buff effect is not active, BuffMultiplier returns 1
                collider.GetComponent<Health>().TakeDamage(ability.statistics.damage * Player.EffectDamageMultipler);
            }
        }


        /// <summary>
        /// Enemy Damages Player Collider using given integer damage
        /// </summary>
        /// <param name="collider">Players Collider </param>
        /// <param name="dmg"> Integer Damage</param>
        public void DamageEnemy(Collider2D collider, int dmg)
        {
            // ARKTODO: Think about the implications and performance of this call
            // ARKNOTE: To improve the performance, we can replace this call with enemycollider.GetComponent<Health>().TakeDamage(damage) 
            // ARKTODO: Create a generic health class which enemy and player can inherit, so the above call can be generic
            collider.GetComponent<Health>().TakeDamage(dmg);
        }
        
        //Casts players ability
        /// <summary>
        /// Executes all functionality to do with Player Casting an ability which includes
        /// - Checking Cooldown
        /// - Playing Animation
        /// - Enabling Skill Trigger Colliders
        /// - Casting Appropraite Effects (Self Casting Effects)
        /// </summary>
        /// <param name="ability"> Ability Being Casted</param>
        public void CastAbility(Ability ability)
        {
            //Check to see ability is off cooldown
            if (ability.offCooldown && (Player.playerHUD.energySlider.value - ability.statistics.energy) > 0)
            {
                //play the correct animation
                Player.PlayerAnimator.Play(ability.information.animationKey, 0);

                switch (ability.type)
                {
                    case eAbilityType.Melee:
                        //set the correct trigger
                        Player.AbilityColliderTrigger.enabled = true;
                        //update the triggers damage with abilities damage
                        Player.AbilityColliderTrigger.GetComponent<SkillTrigger>().CastedAbility = ability;
                        break;
                    case eAbilityType.Ranged:
                        //set the correct trigger
                        Player.AbilityColliderTrigger.enabled = true;
                        //update the triggers damage with abilities damage
                        Player.AbilityColliderTrigger.GetComponent<SkillTrigger>().CastedAbility = ability;
                        break;
                    case eAbilityType.Buff:
                        _EffectManager.CastSelfEffect(ability);
                        break;
                    case eAbilityType.Mobility:
                        break;
                    case eAbilityType.Transform:
                        break;
                    default:
                        break;

                }
                Player.playerHUD.PlayerCastedAbility(ability);
                StartCoroutine(CooldownHandler(ability));
            }
            else
            {
                string message = ability.information.animationKey + " not off ability cooldown yet!";
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.Info, message);
            }
        }

        //Coroutine used for ability cooldown
        /// <summary>
        /// Coroutine for handling Ability Cooldown
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
        IEnumerator CooldownHandler(Ability ability)
        {
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.Info, ability.information.animationKey + " on " + ability.statistics.cooldown.ToString() + " second cooldown");
            ability.offCooldown = false;
            yield return new WaitForSeconds(ability.statistics.cooldown);
            ability.offCooldown = true;
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.Info, ability.information.animationKey + " is off cooldown");
        }
    }
}
