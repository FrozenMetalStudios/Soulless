using System;
using ARK.Player.Ability;
using ARK.Player.Ability.Effects;

namespace ARK.Utility.Ability
{
    /// <summary>
    /// All constants related to ability system sit here
    /// </summary>
    public class Constants
    {
        public static int MAX_EQUIPPABLE_ABILITIES = 6;
    }

    /// <summary>
    /// JSON objects
    /// </summary>
    public class JSONUtility
    {
        [Serializable]
        public class StatsObj
        {
            public int damage;
            public int modifier;
            public int cooldown;
            public int energy;
            public int corruption;
            public string nextabilityID;

            public StatsObj()
            {

            }
        }
        [Serializable]
        public class HitboxObj
        {
            public int length;
            public int height;

            public HitboxObj()
            {

            }
        }
        [Serializable]
        public class EffectObj
        {
            public string effectKey;
            public int damage;
            public float duration;
            public float rate;
            public EffectObj()
            {

            }
        }
        [Serializable]
        public class InformationObj
        {
            public string animationKey;
            public string animationPath;
            public string description;
            public string spritePath;
            public HitboxObj hitbox;
            public InformationObj()
            {
                hitbox = new HitboxObj();
            }
        }
        [Serializable]
        public class AbilityObj
        {
            public string id;
            public string name;
            public string slot;
            public string type;
            public string cast;

            public StatsObj statistics;
            public InformationObj information;
            public EffectObj effect;

            public AbilityObj()
            {
                statistics = new StatsObj();
                information = new InformationObj();
                effect = new EffectObj();
            }
        }
    }
    
    /// <summary>
    /// Functions related to conversion
    /// </summary>
    public class Conversion
    {
        // <summary>
        // These functions convert extracted strings of special types to their respective Enumerations
        // </summary>
        #region String to Enumeration Conversion Functions
        public static eEffectType DetermineEffect(string type)
        {
            if (String.Equals(type, "DamageAmp", StringComparison.OrdinalIgnoreCase)) return eEffectType.DamageAmp;
            if (String.Equals(type, "Debuff", StringComparison.OrdinalIgnoreCase)) return eEffectType.Debuff;
            if (String.Equals(type, "Stun", StringComparison.OrdinalIgnoreCase)) return eEffectType.Stun;
            if (String.Equals(type, "Slow", StringComparison.OrdinalIgnoreCase)) return eEffectType.Slow;
            if (String.Equals(type, "DamageOverTime", StringComparison.OrdinalIgnoreCase)) return eEffectType.DamageOverTime;
            else return eEffectType.undefined;
        }
        public static eEquippedSlot DetermineEquippedSlot(string type)
        {
            if (String.Equals(type, "AttackSlot1", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.AttackSlot1;
            if (String.Equals(type, "AttackSlot2", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.AttackSlot2;
            if (String.Equals(type, "SpellSlot1", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.SpellSlot1;
            if (String.Equals(type, "SpellSlot2", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.SpellSlot2;
            if (String.Equals(type, "SpellSlot3", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.SpellSlot3;
            if (String.Equals(type, "Ultimate", StringComparison.OrdinalIgnoreCase)) return eEquippedSlot.UltimateSlot;
            else return eEquippedSlot.undefined;
        }
        public static eAbilityCast DetermineAbilityCast(string type)
        {
            if (String.Equals(type, "light", StringComparison.OrdinalIgnoreCase)) return eAbilityCast.Light;
            if (String.Equals(type, "dark", StringComparison.OrdinalIgnoreCase)) return eAbilityCast.Dark;
            else return eAbilityCast.undefined;
        }
        public static eAbilityType DetermineAbilityType(string type)
        {
            if (String.Equals(type, "Melee", StringComparison.OrdinalIgnoreCase)) return eAbilityType.Melee;
            if (String.Equals(type, "Ranged", StringComparison.OrdinalIgnoreCase)) return eAbilityType.Ranged;
            if (String.Equals(type, "SelfBuff", StringComparison.OrdinalIgnoreCase)) return eAbilityType.SelfBuff;
            if (String.Equals(type, "Mobility", StringComparison.OrdinalIgnoreCase)) return eAbilityType.Mobility;
            if (String.Equals(type, "Transform", StringComparison.OrdinalIgnoreCase)) return eAbilityType.Transform;
            else return eAbilityType.undefined;
        }
        #endregion

        public static AbilityStats JSONtoStats(JSONUtility.StatsObj jsonobj)
        {
            AbilityStats stats = new AbilityStats();

            stats.damage = jsonobj.damage;
            stats.modifier = jsonobj.modifier;
            stats.cooldown = jsonobj.cooldown;
            stats.energy = jsonobj.energy;
            stats.corruption = jsonobj.corruption;
            stats.nextabilityID = jsonobj.nextabilityID;

            return stats;

        }

        public static AbilityInformation JSONtoInfomartion(JSONUtility.InformationObj jsonobj)
        {
            AbilityInformation info = new AbilityInformation();

            info.animationKey = jsonobj.animationKey;
            info.animationpath = jsonobj.animationPath;
            info.description = jsonobj.description;
            info.spritepath = jsonobj.spritePath;
            info.hitbox.length = jsonobj.hitbox.length;
            info.hitbox.height = jsonobj.hitbox.height;

            return info;
        }

        public static Effect JSONtoEffect(JSONUtility.EffectObj jsonobj)
        {
            Effect effect;

            switch(DetermineEffect(jsonobj.effectKey))
            {
                case eEffectType.DamageAmp:
                    effect = new DamageAmp(jsonobj.duration, jsonobj.rate);
                    break;
                case eEffectType.DamageOverTime:
                    effect = new DamageOverTime(jsonobj.duration, jsonobj.damage, jsonobj.rate);
                    break;
                case eEffectType.Debuff:
                    effect = new DeBuff(jsonobj.duration, jsonobj.rate);
                        break;
                case eEffectType.Slow:
                    effect = new Slow(jsonobj.duration, jsonobj.rate);
                    break;
                case eEffectType.Stun:
                    effect = new Stun(jsonobj.duration);
                    break;
                case eEffectType.undefined:
                    effect = new Effect();
                        break;
                default:
                    effect = new Effect();
                    break;
            }
            return effect;
        }
    }
}

