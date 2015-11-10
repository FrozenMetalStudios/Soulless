using UnityEngine;
using System.Collections;

namespace PlayerAbilities
{
    public enum AbilityType
    {
        Light,
        Dark
    }
    public class Abilities : MonoBehaviour
    {
        public AbilityType type;
        public bool isUnlocked;
        public int costToUse;
        public int costToUnlock;
        public int damage;
        public int range;
        public int cooldown;
        public string description;
        
    }

    public class PlayerUltimate : Abilities
    {

    }

}
