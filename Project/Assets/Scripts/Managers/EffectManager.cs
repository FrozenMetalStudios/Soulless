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


        //Damages Collided enemy object
        public void CastEffect(Collider2D collider, Ability ability)
        {
            // ARKTODO: Think about the implications and performance of this call
            // ARKNOTE: To improve the performance, we can replace this call with enemycollider.GetComponent<Health>().TakeDamage(damage) 
            // ARKTODO: Create a generic health class which enemy and player can inherit, so the above call can be generic

            switch(ability.type)
            {
                case eAbilityType.Melee:
                    CastDamageEffect(collider, ability);
                    break;
                case eAbilityType.Ranged:
                    CastDamageEffect(collider, ability);
                    break;
                case eAbilityType.Buff:
                    //CastBuffEffect(collider, ability);
                    break;
                case eAbilityType.Mobility:
                    //CastMobilityEffect(collider, ability);
                    break;
                case eAbilityType.Transform:
                    throw new System.Exception("not yet implemented!");
                default:
                    break;
            }
        }

        private void CastDamageEffect(Collider2D collider, Ability ability)
        {
            switch (ability.effect.effectkey)
            {
                //Damage Effects
                case eEffectType.DamageOverTime:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Over Time!");
                    StartCoroutine(PerformDoT(collider, ability.effect.statistics));
                    break;
                case eEffectType.LifeSteal:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "LifeSteal!");
                    break;
                case eEffectType.undefined:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Effect is undefined!");
                    break;
                default:
                    break;
            }
        }
        private void CastBuffEffect(Collider2D collider, Ability ability)
        {
            switch (ability.effect.effectkey)
            {
                case eEffectType.DamageBuff:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage Amplifier!");
                    //StartCoroutine(PerformDamageBuff(collider, ability.effect.statistics));
                    break;
                case eEffectType.Movement:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Movement!");
                    //StartCoroutine(PerformMovementBuff(collider, ability.effect.statistics));
                    break;
                case eEffectType.undefined:
                    ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Effect is undefined!");
                    break;
                default:
                    break;
            }
        }
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
        public IEnumerator PerformDoT(Collider2D target, EffectStatistics stats)
        {
            Health targetHealth = target.GetComponent<Health>();
            float amountDamaged = 0;
            float damagePerLoop = (stats.damage.damage / stats.duration) ;

            while (amountDamaged < stats.damage.damage)
            {
                targetHealth.TakeDamage(damagePerLoop);
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Target damaged: " + damagePerLoop);
                amountDamaged += damagePerLoop;
                yield return new WaitForSeconds(stats.damage.rate);
            }

        }
        public IEnumerator PerformLifeSteal(Collider2D target, EffectStatistics stats)
        {
            //create temp variable that holds original damage

            //apply modifier

            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Life steal multiplier: " + stats.damage.percentage + "with duration: " + stats.duration + "secs");
            yield return new WaitForSeconds(stats.duration);

            //reset the stat
        }
        #endregion

        /*
        #region Buff Effects
        public IEnumerator PerformDamageBuff(Collider2D target, EffectStatistics stats)
        {
            //create temp variable that holds original damage

            //apply modifier

            buff_multiplier = stats.buff.percentage;
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Damage buff applied! " + stats.duration + "secs with percentage buff: " + stats.buff.percentage);
            yield return new WaitForSeconds(stats.duration);
            buff_multiplier = 1;


            //reset the stat
        }
        public IEnumerator PerformMovementBuff(Collider2D target, EffectStatistics stats)
        {
            float movespeed = 0;

            if (target.tag == Tags.Enemy)
            {
                movespeed = target.GetComponent<EnemyController>().Movespeed;
                target.GetComponent<EnemyController>().Movespeed = movespeed * stats.buff.percentage;
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Enemy is slowed for " + stats.duration + "secs with percentage slow: " + stats.buff.percentage);
                yield return new WaitForSeconds(stats.duration);
                target.GetComponent<EnemyController>().Movespeed = movespeed;
            }
            else
            {
                movespeed = target.GetComponent<PlayerController>().maxSpeed;
                target.GetComponent<PlayerController>().maxSpeed = movespeed * stats.buff.percentage;
                ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "Player is slowed for " + stats.duration + "secs with percentage slow: " + stats.buff.percentage);
                yield return new WaitForSeconds(stats.duration);
                target.GetComponent<PlayerController>().maxSpeed = movespeed;
            }
        }
        #endregion
    */
        #region Mobility Effects
        public IEnumerator PerfomDodge(Collider2D target, EffectStatistics stats)
        {
            //create temp variable that holds original damage

            //apply modifier

            throw new System.Exception("Dodge not implemented!");
            //yield return new WaitForSeconds(stats.duration);

            //reset the stat
        }
        public IEnumerator PerformTeleport(Collider2D target, EffectStatistics stats)
        {
            //create temp variable that holds original damage

            //apply modifier

            throw new System.Exception("Teleport not implemented!");
            //yield return new WaitForSeconds(stats.duration);

            //reset the stat
        }
        public IEnumerator PerformStun(Collider2D target, EffectStatistics stats)
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

    }
}

