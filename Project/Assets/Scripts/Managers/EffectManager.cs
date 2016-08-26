using UnityEngine;
using ARK.Player.Ability;
using ARK.Player.Ability.Effects;
using Assets.Scripts.Utility;
using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using System.Collections;


namespace Assets.Scripts.Managers
{
    public class EffectManager : MonoBehaviour
    {
        // --------------------------------------------------------------------
        static EffectManager _Singleton = null;
        private Ability ability;
        public PlayerProfile Player;

        // --------------------------------------------------------------------
        public static EffectManager Singleton
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
                                     "EffectManager: Multiple EffectManager violate Singleton pattern.");
            }
            _Singleton = this;
        }

        
        /// <summary>
        /// Determines which Target Effect to cast (Damaging)
        /// </summary>
        /// <param name="collider"> Enemy Collider</param>
        /// <param name="ability">Ability to Cast</param>
        public void CastTargetEffect(Collider2D collider, Ability ability)
        {
            switch(ability.type)
            {
                case eAbilityType.Melee:
                    CastDamageEffect(collider, ability);
                    break;
                case eAbilityType.Ranged:
                    CastDamageEffect(collider, ability);
                    break;
                default:
                    break;
            }
        }

        //NOTE: This CastEffect implements the function call that does not reguire a collider
        // This function deals with Self-Casted Effects
        /// <summary>
        /// Determines which Self Casting Effects to call
        /// - Damage Buff
        /// - Movement Buff
        /// </summary>
        /// <param name="ability">Ability with self buff effect</param>
        public void CastSelfEffect(Ability ability)
        {
            switch (ability.effect.effectkey)
            {
                case eEffectType.DamageBuff:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Amplifier!");
                    StartCoroutine(PerformDamageBuff(ability.effect.statistics));
                    break;
                case eEffectType.Movement:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Movement!");
                    StartCoroutine(PerformMovementBuff(ability.effect.statistics));
                    break;
                case eEffectType.undefined:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Effect is undefined!");
                    break;
                default:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Not a Self Casting Effect Ability!");
                    break;
            }
        }

        /// <summary>
        /// Cast Damaging Effect such as
        /// - Damage over time
        /// - Life steal
        /// </summary>
        /// <param name="collider">Target</param>
        /// <param name="ability">Ability with damaging effect</param>
        private void CastDamageEffect(Collider2D collider, Ability ability)
        {
            switch (ability.effect.effectkey)
            {
                //Damage Effects
                case eEffectType.DamageOverTime:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Over Time!");
                    StartCoroutine(PerformDoT(collider, ability));
                    break;
                case eEffectType.LifeSteal:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "LifeSteal!");
                    StartCoroutine(PerformLifeSteal(collider, ability));
                    break;
                case eEffectType.undefined:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Effect is undefined!");
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Casts the Buff Effect
        /// </summary>
        /// <param name="ability">Ability with Buff Effect</param>
        private void CastBuffEffect(Ability ability)
        {
            switch (ability.effect.effectkey)
            {
                case eEffectType.DamageBuff:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Amplifier!");
                    StartCoroutine(PerformDamageBuff(ability.effect.statistics));
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
        /// <summary>
        /// Casts an ability with Mobility Effect
        /// </summary>
        /// <param name="collider">Target</param>
        /// <param name="ability">Ability with mobility effect</param>
        private void CastMobilityEffect(Collider2D collider, Ability ability)
        {
            switch (ability.effect.effectkey)
            {
                case eEffectType.Dodge:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Dodge!");
                    break;
                case eEffectType.Teleport:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Teleport!");
                    break;
                case eEffectType.Stun:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Stun");
                    StartCoroutine(PerformStun(collider, ability.effect.statistics));
                    break;
                case eEffectType.undefined:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Effect is undefined!");
                    break;
                default:
                    break;
            }
        }

        #region Damage Effects
        
        /// <summary>
        /// Coroutine for Damage over Time
        /// </summary>
        /// <param name="target">Target collider</param>
        /// <param name="stats">Effect statistics</param>
        /// <returns></returns>
        private IEnumerator PerformDoT(Collider2D target, Ability ability)
        {
            Health targetHealth = target.GetComponent<Health>();
            float amountDamaged = 0;
            float damagePerLoop = (ability.effect.statistics.damage.damage / ability.effect.statistics.duration) * Player.EffectDamageMultipler;

            while (amountDamaged < ability.effect.statistics.damage.damage)
            {
                targetHealth.TakeDamage(damagePerLoop);
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Target damaged: " + damagePerLoop);
                amountDamaged += damagePerLoop;
                yield return new WaitForSeconds(ability.effect.statistics.damage.rate);
            }

        }
        /// <summary>
        /// Coroutine for Life Steal
        /// </summary>
        /// <param name="target">Target Collider</param>
        /// <param name="stats">Effect statistics</param>
        /// <returns></returns>
        private IEnumerator PerformLifeSteal(Collider2D target, Ability ability)
        {
            Health targetHealth = target.GetComponent<Health>();
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            float amountDamaged = 0;
            float damagePerLoop = (ability.effect.statistics.damage.damage / ability.effect.statistics.duration) * Player.EffectDamageMultipler;

            while (amountDamaged < ability.effect.statistics.damage.damage)
            {
                //Play Animation
                // ARKTODO: Need to add effect animation key so you can play animation effects
                //LoadEffectAnimation(ability);
                //Player.PlayerAnimator.Play(ability.effect.animationkey, 0);
                //Damage Target
                targetHealth.TakeDamage(damagePerLoop);
                //Add Health to Player
                playerHealth.AddHealth(damagePerLoop);
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Life Stolen: " + damagePerLoop);
                amountDamaged += damagePerLoop;
                yield return new WaitForSeconds(ability.effect.statistics.damage.rate);
            }

            //reset the stat
        }
        #endregion

        #region Buff Effects
        /// <summary>
        /// Coroutine for Self Damage Buff
        /// </summary>
        /// <param name="stats">Effect statistics</param>
        private IEnumerator PerformDamageBuff(EffectStatistics stats)
        {
            //create temp variable that holds original damage

            //apply modifier
            Player.EffectDamageMultipler = stats.buff.percentage;
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage buff applied! " + stats.duration + "secs with percentage buff: " + stats.buff.percentage);
            yield return new WaitForSeconds(stats.duration);
            Player.EffectDamageMultipler = 1;
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Buff Off!");

            //reset the stat
        }

        /// <summary>
        /// Coroutine for Self Movement Buff
        /// </summary>
        /// <param name="stats">Effect statistics</param>
        private IEnumerator PerformMovementBuff(EffectStatistics stats)
        {
            float movespeed = 0;
            movespeed = this.GetComponent<PlayerController>().maxSpeed;
            this.GetComponent<PlayerController>().maxSpeed = movespeed * stats.buff.percentage;
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Player is slowed for " + stats.duration + "secs with percentage slow: " + stats.buff.percentage);
            yield return new WaitForSeconds(stats.duration);
            this.GetComponent<PlayerController>().maxSpeed = movespeed;
        }
        #endregion
        #region Mobility Effects
        /// <summary>
        /// Coroutine for Mobility
        /// </summary>
        /// /// <param name="target">Target Collider</param>
        /// <param name="stats">Effect statistics</param>
        private IEnumerator PerfomDodge(Collider2D target, EffectStatistics stats)
        {
            //create temp variable that holds original damage

            //apply modifier

            throw new System.Exception("Dodge not implemented!");
            //yield return new WaitForSeconds(stats.duration);

            //reset the stat
        }
        /// <summary>
        /// Coroutine for Teleport
        /// </summary>
        /// /// <param name="target">Target Collider</param>
        /// <param name="stats">Effect statistics</param>
        private IEnumerator PerformTeleport(Collider2D target, EffectStatistics stats)
        {
            //create temp variable that holds original damage

            //apply modifier

            throw new System.Exception("Teleport not implemented!");
            //yield return new WaitForSeconds(stats.duration);

            //reset the stat
        }
        /// <summary>
        /// Coroutine for Stun
        /// </summary>
        /// /// <param name="target">Target Collider</param>
        /// <param name="stats">Effect statistics</param>
        private IEnumerator PerformStun(Collider2D target, EffectStatistics stats)
        {

            //disable movement
            if (target.tag == Tags.Enemy)
            {
                target.GetComponent<EnemyController>().enabled = false;
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Enemy is Stunned for " + stats.duration + "secs");
                yield return new WaitForSeconds(stats.duration);
                target.GetComponent<EnemyController>().enabled = true;
            }
            else
            {
                target.GetComponent<PlayerController>().enabled = false;
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Player is Stunned for " + stats.duration + "secs");
                yield return new WaitForSeconds(stats.duration);
                target.GetComponent<PlayerController>().enabled = true;
            }

        }
        #endregion

        /// <summary>
        /// Loads the animation clips into effect animation state by overriding the existing controller with a new one
        /// </summary>
        /// <param name="ability">abilitiy</param>
        private void LoadEffectAnimation(Ability ability)
        {
            RuntimeAnimatorController currentController = Player.PlayerAnimator.runtimeAnimatorController;
            AnimatorOverrideController overrideController = new AnimatorOverrideController();

            overrideController.runtimeAnimatorController = currentController;
            ARKLogger.LogMessage(eLogCategory.Animation, eLogLevel.Info, "Loading effect : " + ability.effect.animationpath);

            AnimationClip newAnim = Resources.Load<AnimationClip>(ability.effect.animationpath);
            overrideController[ability.effect.AnimationKey] = newAnim;

            Player.PlayerAnimator.runtimeAnimatorController = overrideController;
        }
    }
}

