using UnityEngine;
using System.Collections;
using ARK.Player.Ability.Effects;
using System;


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
        SelfBuff,               //all abilities that apply a buff/debuff to player (sheild, dmg increase/decrease etc)
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
        public string name;
        public int damage;
        public int modifier;
        public int cooldown;
        public int energy;
        public int corruption;
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
    }

    //Base Ability 
    //<summary>
    //Holds all information pertaining to a ability
    //</summary>
    [Serializable]
    public class Ability : MonoBehaviour
    {
        public eEquippedSlot slot;
        public eAbilityType type;
        public eAbilityCast cast;
        public Sprite spriteimg;
        public bool offCooldown;                   //Cooldown Flag

        private AbilityStats statistics;
        private AbilityInformation devInformation;
        private AbilityEffect effect;

        //Default Constructor
        public Ability()
        {
            slot = eEquippedSlot.undefined;
            type = eAbilityType.undefined;
            cast = eAbilityCast.undefined;
            offCooldown = true;

            statistics = new AbilityStats();
            devInformation = new AbilityInformation();
            effect = new AbilityEffect();


        }
        public Ability(eEquippedSlot slot, eAbilityType type, eAbilityCast cast)
        {
            this.slot = slot;
            this.type = type;
            this.cast = cast;
            offCooldown = true;

            statistics = new AbilityStats();
            devInformation = new AbilityInformation();
            effect = new AbilityEffect();
           

        }

        //The Constructor the builder uses
        public Ability(AbilityStats stats, AbilityInformation devInfo, AbilityEffect effect)
        {
            statistics = stats;
            devInformation = devInfo;
            this.effect = effect;
        }

        public AbilityStats Statistics
        {
            get { return statistics; }
            set { statistics = value; }
        }

        public AbilityInformation DevInformation
        {
            get { return devInformation; }
            set { devInformation = value; }
        }

        public AbilityEffect Effect
        {
            get { return effect; }
            set { effect = value; }
        }
    }

}
