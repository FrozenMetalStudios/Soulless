using System;
using UnityEngine;
using System.Collections;
namespace ARK.Player.Ability.Effects
{
    //Enumeration of all the different types of effects an ability can apply
    public enum eEffectType
    {
        DamageOverTime,
        Stun,
        Slow,
        DamageAmp,
        Debuff,
        undefined
    }

    #region Data Structures for Different Effect Types
    public class EffectStatistics
    {
        public class Base
        {
            public eEffectType type;
            public float duration;
        }
        public class Slow : Base { public float percentage; }
        public class Stun : Base { }
        public class DamageOverTime : Base
        {
            public int damage;
            public float rate;
        }
        public class DamageAmp : Base { public float multiplier; }
        public class Debuff : DamageAmp { }
    }
    #endregion

    //Base Ability Effect Class
    //<summary>
    //Lays out the basic ability effect structure 
    //</summary>
    public abstract class BaseEffect : MonoBehaviour
    {
        public eEffectType effectkey;
        public string effectpath;

        public abstract void Cast(Collider2D target);
    }
    public class Effect : BaseEffect
    {
        public Effect()
        {
            effectkey = eEffectType.undefined;
            this.effectpath = null;
        }
        public override void Cast(Collider2D target)
        {
            throw new NotImplementedException();   
        }
    }

    #region Derived Classes For Different Effects
    public class DamageOverTime : Effect
    {
        EffectStatistics.DamageOverTime statistics;
        public DamageOverTime()
        {
            statistics = new EffectStatistics.DamageOverTime();
            effectkey = eEffectType.DamageOverTime;
            effectpath = null;
        }

        public DamageOverTime(float duration, int damage, float rate)
        {
            statistics = new EffectStatistics.DamageOverTime();
            effectkey = eEffectType.DamageOverTime;
            statistics.duration = duration;
            statistics.damage = damage;
            statistics.rate = rate;
        }

        //object will be called by the combat manager
        public override void Cast(Collider2D target)
        {
            //spawns independent thread(or coroutine?) that will be alive for the duration of the effect
            //perform damage over time
            //terminate thread
            StartCoroutine(PerformDoT(target));
        }

        private IEnumerator PerformDoT(Collider2D target)
        {
            Health targetHealth = target.GetComponent<Health>(); ;
            for (float i = 0f; i <= statistics.duration; i += Time.deltaTime)
            {
                targetHealth.TakeDamage(statistics.damage);
                new WaitForSeconds(statistics.rate);
            }
            yield return true;
        }
    }
    public class Stun : Effect
    {
        EffectStatistics.Stun statistics;
        public Stun(float duration)
        {
            statistics = new EffectStatistics.Stun();
            statistics.type = eEffectType.Stun;
            statistics.duration = duration;
        }
        public override void Cast(Collider2D target)
        {
            throw new NotImplementedException();
        }
    }
    public class Slow : Effect
    {
        EffectStatistics.Slow statistics;

        public Slow(float duration, float percentage)
        {
            statistics = new EffectStatistics.Slow();
            statistics.duration = duration;
            statistics.percentage = percentage;
        }

        public override void Cast(Collider2D target)
        {
            throw new NotImplementedException();
        }
    }
    public class DeBuff : Effect
    {
        EffectStatistics.Debuff statistics;

        public DeBuff(float duration, float multiplier)
        {
            statistics = new EffectStatistics.Debuff();
            statistics.duration = duration;
            statistics.multiplier = multiplier;
        }
        public override void Cast(Collider2D target)
        {
            throw new NotImplementedException();
        }
    }
    public class DamageAmp : Effect
    {
        EffectStatistics.DamageAmp statistics;

        public DamageAmp(float duration, float multiplier)
        {
            statistics = new EffectStatistics.DamageAmp();
            effectkey = eEffectType.DamageAmp;
            statistics.duration = duration;
            statistics.multiplier = multiplier;
        }

        public override void Cast(Collider2D target)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
