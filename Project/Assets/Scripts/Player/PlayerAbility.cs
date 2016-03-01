using UnityEngine;
using System.Collections;
using ARK.Player.Ability.Effects;


namespace ARK.Player.Ability
{
    //Abilities can be either associated with Demon(dark), or Spirit(light)
    //Helps to determine corruption level
    public enum eAbilityCast
    {
        Light,
        Dark
    }

    //The different types of abilities players can have
    public enum eAbilityType
    {
        Melee,                  //melee abilities hit harder, but have much shorter range
        Ranged,                 //ranged abilities do less dmg, but have a longer range
        SelfBuff,               //all abilities that apply a buff/debuff to player (sheild, dmg increase/decrease etc)
        Mobility,               //abilities that increase the player movement or traversal of the map (dodges, dashes, etc)
        Transform               //abilities that change the players character model and other abilities
    }

    //Players attack types
    public enum eEquippedSlot
    {
        AttackSlot1,
        AttackSlot2,
        SpellSlot1,
        SpellSlot2,
        SpellSlot3,
        UltimateSlot
    }

    //Ability Hitbox object
    //<summary>
    // Holds collider length and height modifications
    //</summary>
    public class AbilityHitBox
    {
        private float colliderLength;
        private float colliderHeight; 
    }

    //Ability Data
    //<summary>
    //Holds all information pertaining to a abilities statistics
    //</summary>
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
    public class AbilityInformation
    {
        public string animationKey;
        public string effectKey;
        public AbilityHitBox hitbox;
    }

    //Base Ability 
    //<summary>
    //Holds all information pertaining to a ability
    //</summary>
    public class BaseAbility : MonoBehaviour
    {
        public AbilityStats statistics;
        public AbilityInformation information;
        public AbilityEffect effect;
    }

}
