using System;
using UnityEngine;
using System.Collections;
namespace ARK.Player.Ability.Effects
{
    //Enumeration of all the different types of effects an ability can apply
    public enum eEffectType
    {
        DamageOverTime,
        LifeSteal,
        Stun,
        Slow,
        DamageBuff,
        Movement,
        Dodge,
        Teleport,
        undefined
    }
    #region Data Structures for Different Effect Types

    public struct DamageEffectStats
    {
        public float percentage;
        public int damage;
        public float rate;
    }
    public struct BuffEffectStats
    {
        public float percentage;
    }

    public struct MobilityEffectStats
    {
        public int distance;
    }
    public struct EffectStatistics
    {
        public eEffectType type;
        public float duration;

        public DamageEffectStats damage;
        public BuffEffectStats buff;
        public MobilityEffectStats mobility;
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
            statistics.damage.damage = damage;
            statistics.damage.rate = rate;
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
            statistics.buff.percentage = percentage;
            animationpath = path;
        }
    }
    public class LifeSteal : Effect
    {
        public LifeSteal(float duration, float percentage, string path)
        {
            statistics = new EffectStatistics();
            statistics.duration = duration;
            statistics.damage.percentage = percentage;
            animationpath = path;
        }
    }
    public class DamageBuff : Effect
    {
        public DamageBuff(float duration, float percentage, string path)
        {
            statistics = new EffectStatistics();
            effectkey = eEffectType.DamageBuff;
            statistics.duration = duration;
            statistics.buff.percentage = percentage;
            animationpath = path;
        }
    }
    public class Dodge : Effect
    {
        public Dodge(float duration, int distance, string path)
        {
            statistics = new EffectStatistics();
            statistics.duration = duration;
            statistics.mobility.distance = distance;
            animationpath = path;
        }
    }
    public class Teleport : Effect
    {
        public Teleport(float duration, int distance, string path)
        {
            statistics = new EffectStatistics();
            statistics.duration = duration;
            statistics.mobility.distance = distance;
            animationpath = path;
        }
    }
    #endregion
}
