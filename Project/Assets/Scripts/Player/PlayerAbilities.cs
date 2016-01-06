using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayerAbilities
{
    //Abilities can be either associated with Demon(dark), or Spirit(light)
    //Helps to determine corruption level
    public enum eAbilityType
    {
        Light,
        Dark
    }

    //Players attack types
    public enum ePlayerAbilities
    {
        Attack1,
        Attack2,
        Spell1,
        Spell2,
        Spell3,
        Ultimate
    }

    public class Abilities : MonoBehaviour
    {
        public ePlayerAbilities attackType;         //Type of ability (Attack1, Attack2, Spell1, Spell2, Spell3, Ultimate) used for identification
        private eAbilityType abilityType;           //Ability type (light dark) used for corruption management

        private bool isEquipped;                    //Equipped Flag
        private bool isUnlocked;                    //Unlocked Flag
        private bool offCooldown;                   //Cooldown Flag
            
        private int costToUnlock;                   //Cost to unlock the ability if it is not unlocked
        private int damage;                         //Damage the ability outputs
        private int range;                          //Range of the ability (used to scale the trigger
        private float cooldown;                     //Abilities cooldown
                    
        private string inputTag;                    //Input Tag which is used for animator 
        private Sprite abilityImage;                //Equipped abilities sprite

        #region Getters and Setters
        public string InputTag
        {
            get { return inputTag; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public int Range
        {
            get { return range; }
            set { range = value; }
        }
        public float CoolDown
        {
            get { return cooldown; }
            set { cooldown = value; }
        }

        public bool isOffCooldown
        {
            get { return offCooldown; }
            set { offCooldown = value; }
        }
        public Sprite AbilityImage
        {
            get { return abilityImage; }
            set { abilityImage = value; }
        }
        #endregion

        //Default constructor
        public Abilities()
        {

        }

        //Main constructor
        public Abilities(ePlayerAbilities type, eAbilityType aType, int dmg, float cd, string tag, Sprite img)
        {
            damage = dmg;
            cooldown = cd;
            inputTag = tag;
            offCooldown = true;
            abilityImage = img;
            attackType = type;
            abilityType = aType;
        }

    }

}
