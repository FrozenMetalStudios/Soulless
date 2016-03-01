using UnityEngine;
using System.Collections;


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
        Attack1,
        Attack2,
        Spell1,
        Spell2,
        Spell3,
        Ultimate
    }

    //Ability Hitbox object
    //<summary>
    // Holds collider length and height modifications
    //</summary>
    public class AbilityHitBox
    {
        public float colliderLength;
        public float colliderHeight; 

    }

    //Ability Data
    //<summary>
    //Holds all information pertaining to a abilities data
    //</summary>
    public class AbilityData
    {
        public string name;
        public string animationKey;
        public string effectKey;
        public int damage;
        public int cooldown;
        public int energy;
        public int corruption;

        AbilityHitBox hitbox;
    }

    //Ability 
    //<summary>
    //Holds all information pertaining to a ability
    //</summary>
    public class PlayerAbility : MonoBehaviour
    {


    }



}
