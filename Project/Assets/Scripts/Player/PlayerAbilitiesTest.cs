using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace PlayerAbilityTest
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


    public class AbilityTest : MonoBehaviour{
        public eEquippedSlot equippedSlot;         //Type of ability (Attack1, Attack2, Spell1, Spell2, Spell3, Ultimate) used for identification
        public eAbilityCast cast;           //Ability type (light dark) used for corruption management
        public bool offCooldown;                   //Cooldown Flag

        public int damage;                         //Damage the ability outputs
        public float cooldown;                     //Abilities cooldown
        public int energy;                         //Energy resource cost
        public int corruption;                      //Corruption cost

        public string animationTag;                    //Input Tag which is used for animator 
        public Sprite abilityImage;                //Equipped abilities sprite

        //Default constructor
        public AbilityTest()
        {

        }

        //Main constructor
        public AbilityTest(eEquippedSlot slot, eAbilityCast aCast, int dmg, float cd, int eCost, int cCost,Sprite img)
        {
            damage = dmg;
            cooldown = cd;
            equippedSlot = slot;
            offCooldown = true;
            energy = eCost;
            corruption = cCost;
            abilityImage = img;
            equippedSlot = slot;
            cast = aCast;
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
