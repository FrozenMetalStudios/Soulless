using UnityEngine;
using System.Collections;

namespace ARK.Player.Ability.Effects
{
    //Enumeration of all the different types of effects an ability can apply
    public enum eEffectType
    {
        DamageOverTime,
        Stun,
        Slow,
        Damage,
        undefined
    }

    //Base Ability Effect Interface
    //<summary>
    //Lays out the basic ability effect structure 
    //</summary>
    public interface Effect
    {
        void performAbility();
    }

    //Base Ability Effect class
    //<summary>
    //Lays out the basic ability effect structure 
    //</summary>
    public class AbilityEffect : Effect
    {

        public eEffectType effectkey;
        public AbilityEffect()
        {
        }
        public void performAbility()
        {

        }
    }


}
