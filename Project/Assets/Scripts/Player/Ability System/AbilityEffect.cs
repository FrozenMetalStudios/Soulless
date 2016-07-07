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
        public eEffectType type;
        public float duration;

        public float percentage;
        public int damage;
        public float rate;
        public float multiplier;
    }

    #endregion

    public class Effect : MonoBehaviour
    {
        public EffectStatistics statistics;
        public eEffectType effectkey;
        public string animationpath;

        public Effect()
        {
            effectkey = eEffectType.undefined;
            animationpath = null;
        }
    }

    #region Derived Classes For Different Effects
    public class DamageOverTime : Effect
    {
        public DamageOverTime()
        {
            statistics = new EffectStatistics();
            effectkey = eEffectType.DamageOverTime;
            animationpath = null;
        }

        public DamageOverTime(float duration, int damage, float rate, string path)
        {
            statistics = new EffectStatistics();
            effectkey = eEffectType.DamageOverTime;
            statistics.duration = duration;
            statistics.damage = damage;
            statistics.rate = rate;
            animationpath = path;
        }
    }
    public class Stun : Effect
    {
        public Stun(float duration, string path)
        {
            statistics = new EffectStatistics();
            statistics.type = eEffectType.Stun;
            statistics.duration = duration;
            animationpath = path;
        }
    }
    public class Slow : Effect
    {
        public Slow(float duration, float percentage, string path)
        {
            statistics = new EffectStatistics();
            statistics.duration = duration;
            statistics.percentage = percentage;
            animationpath = path;
        }
    }
    public class DeBuff : Effect
    {
        public DeBuff(float duration, float multiplier, string path)
        {
            statistics = new EffectStatistics();
            statistics.duration = duration;
            statistics.multiplier = multiplier;
            animationpath = path;
        }
    }
    public class DamageAmp : Effect
    {
        public DamageAmp(float duration, float multiplier, string path)
        {
            statistics = new EffectStatistics();
            effectkey = eEffectType.DamageAmp;
            statistics.duration = duration;
            statistics.multiplier = multiplier;
            animationpath = path;
        }
    }
    #endregion
}
