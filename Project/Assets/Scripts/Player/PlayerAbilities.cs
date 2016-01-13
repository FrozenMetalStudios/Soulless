using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace PlayerAbilities
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


    public class Ability : MonoBehaviour
    {
        public eEquippedSlot equippedSlot;         //Type of ability (Attack1, Attack2, Spell1, Spell2, Spell3, Ultimate) used for identification
        private eAbilityCast abilityType;           //Ability type (light dark) used for corruption management

        public bool isEquipped;                    //Equipped Flag
        public bool isUnlocked;                    //Unlocked Flag
        public bool offCooldown;                   //Cooldown Flag

        public int costToUnlock;                   //Cost to unlock the ability if it is not unlocked
        public int damage;                         //Damage the ability outputs
        public int range;                          //Range of the ability (used to scale the trigger
        public float cooldown;                     //Abilities cooldown

        public string animationTag;                    //Input Tag which is used for animator 
        public Sprite abilityImage;                //Equipped abilities sprite

        //Default constructor
        public Ability()
        {

        }

        //Main constructor
        public Ability(eEquippedSlot slot, eAbilityCast aCast, int dmg, float cd, Sprite img)
        {
            damage = dmg;
            cooldown = cd;
            equippedSlot = slot;
            offCooldown = true;
            abilityImage = img;
            equippedSlot = slot;
            abilityType = aCast;
            animationTag = DetermineAnimationTag(slot);
        }

        public string DetermineAnimationTag(eEquippedSlot slot)
        {
            switch (slot)
            {
                case eEquippedSlot.Attack1:
                    return "BasicAttack1";
                case eEquippedSlot.Attack2:
                    return "BasicAttack2";
                case eEquippedSlot.Spell1:
                    return "Ability1";
                case eEquippedSlot.Spell2:
                    return "Ability2";
                case eEquippedSlot.Spell3:
                    return "Ability3";
                case eEquippedSlot.Ultimate:
                    return "Ultimate";
                default:
                    return null;

            }

        }

    }
    
}
