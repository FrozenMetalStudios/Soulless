using UnityEngine;
using System.Collections;

namespace PlayerAbilities
{
    public enum AbilityType
    {
        Light,
        Dark
    }

    public enum ePlayerAbilities
    {
        BasicAttack1,
        BasicAttack2,
        Spell1,
        Spell2,
        Spell3,
        Ultimate
    }
    public class Abilities : MonoBehaviour
    {
        private AbilityType type;
        private bool isEquipped;
        private bool isUnlocked;
        private int costToUnlock;
        private int damage;
        private int range;
        private float cooldown;
        private string inputTag;
        private bool offCooldown;
        

        public string InputTag
        {
            get { return inputTag; }
            set { inputTag = value; }
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
        public Abilities()
        {

        }
        public Abilities(int dmg, float cd, string tag)
        {
            damage = dmg;
            cooldown = cd;
            inputTag = tag;
            offCooldown = true;
        }

    }

}
