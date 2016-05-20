using System;
using ARK.Player.Ability;
using ARK.Player.Ability.Effects;


namespace ARK.Utility.Ability
{
    [Serializable]
    public class JsonStatistics
    {
        public int damage;
        public int modifier;
        public int cooldown;
        public int energy;
        public int corruption;
        public string nextabilityID;

        public JsonStatistics()
        {

        }
    }
    [Serializable]
    public class JsonHitBox
    {
        public int length;
        public int height;

        public JsonHitBox()
        {

        }
    }
    [Serializable]
    public class JsonEffect
    {
        public string effectKey;
        public string effectScriptPath;
        public JsonEffect()
        {

        }
    }
    [Serializable]
    public class JsonInformation
    {
        public string animationKey;
        public string animationPath;
        public string description;
        public string spritePath;
        public JsonHitBox hitbox;
        public JsonInformation()
        {
            hitbox = new JsonHitBox();
        }
    }
    [Serializable]
    public class JsonAbilityObject
    {
        public string id;
        public string name;
        public string slot;
        public string type;
        public string cast;

        public JsonStatistics statistics;
        public JsonInformation information;
        public JsonEffect effect;

        public JsonAbilityObject()
        {
            statistics = new JsonStatistics();
            information = new JsonInformation();
            effect = new JsonEffect();
        }
    }
    public class Conversion
    {
        // <summary>
        // These functions convert extracted strings of special types to their respective Enumerations
        // </summary>
        #region String to Enumeration Conversion Functions
        public static eEffectType DetermineEffect(string type)
        {
            if (String.Equals(type, "Damage", StringComparison.OrdinalIgnoreCase)) return eEffectType.Damage;
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

        public static AbilityStats JSONtoStats(JsonStatistics jsonobj)
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

        public static AbilityInformation JSONtoInfomartion(JsonInformation jsonobj)
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

        public static AbilityEffect JSONtoEffect(JsonEffect jsonobj)
        {
            AbilityEffect effect = new AbilityEffect();

            effect.effectkey = DetermineEffect(jsonobj.effectKey);
            effect.effectpath = jsonobj.effectScriptPath;

            return effect;
        }
    }
}

