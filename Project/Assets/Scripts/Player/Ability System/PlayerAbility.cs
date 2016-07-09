using System;
using UnityEngine;
using ARK.Player.Ability.Effects;

namespace ARK.Player.Ability
{
    #region Ability Enumeration Types
    //Abilities can be either associated with Demon(dark), or Spirit(light)
    //Helps to determine corruption level
    public enum eAbilityCast
    {
        Light,
        Dark,
        undefined
    }

    //The different types of abilities players can have
    public enum eAbilityType
    {
        Melee,                  //melee abilities hit harder, but have much shorter range
        Ranged,                 //ranged abilities do less dmg, but have a longer range
        Buff,               //all abilities that apply a buff/debuff to player (sheild, dmg increase/decrease etc)
        Mobility,               //abilities that increase the player movement or traversal of the map (dodges, dashes, etc)
        Transform,               //abilities that change the players character model and other abilities
        undefined
    }

    //Players attack types
    public enum eEquippedSlot
    {
        AttackSlot1,
        AttackSlot2,
        SpellSlot1,
        SpellSlot2,
        SpellSlot3,
        UltimateSlot,
        undefined
    }
    #endregion

    #region Ability Supporting Types
    //Ability Hitbox object
    //<summary>
    // Holds collider length and height modifications
    //</summary>
    [Serializable]
    public class AbilityHitBox
    {
        public float length;
        public float height; 
    }

    //Ability Data
    //<summary>
    //Holds all information pertaining to a abilities statistics
    //</summary>
    [Serializable]
    public class AbilityStats
    {
        public int damage;
        public int modifier;
        public int cooldown;
        public int energy;
        public int corruption;
        public string nextabilityID;
    }

    //Ability Information
    //<summary>
    //Holds all information pertaining to a abilities game object information
    //</summary>
    [Serializable]
    public class AbilityInformation
    {
        public string animationKey;
        public string animationpath;
        public string description;
        public string spritepath;
        public AbilityHitBox hitbox;

        public AbilityInformation()
        {
            hitbox = new AbilityHitBox();
        }
    }
    #endregion

    //Base Ability 
    //<summary>
    //Holds all information pertaining to a ability
    //</summary>
    [Serializable]
    public class Ability : MonoBehaviour
    {
        public string id;
        public new string name;
        public eEquippedSlot slot;
        public eAbilityType type;
        public eAbilityCast cast;
        public Sprite spriteimg;
        public bool offCooldown;                   //Cooldown Flag

        public AbilityStats statistics;
        public AbilityInformation information;
        public Effect effect;

        //Default Constructor
        public Ability()
        {
            name = "";
            slot = eEquippedSlot.undefined;
            type = eAbilityType.undefined;
            cast = eAbilityCast.undefined;
            offCooldown = true;

            statistics = new AbilityStats();
            information = new AbilityInformation();
            effect = new Effect();
        }
        public Ability(eEquippedSlot slot, eAbilityType type, eAbilityCast cast)
        {
            this.slot = slot;
            this.type = type;
            this.cast = cast;
            offCooldown = true;

            statistics = new AbilityStats();
            information = new AbilityInformation();
            effect = new Effect();
        }

        //The Constructor the builder uses
        public Ability(AbilityStats stats, AbilityInformation devInfo, Effect effect)
        {
            statistics = stats;
            information = devInfo;
            this.effect = effect;
        }
    }
}
