using UnityEngine;
using System.Collections;

namespace ARK.Player.Ability.Builders
{
    //Builder class interface
    public interface AbilityBuilder
    {
        //Prototype function calls for building different parts of Ability
        void BuildData();
        void BuildEffect();
        void BuildHitBox();

    }

    //Concrete Builder classes

    //Damage Ability Builder
    //<summary>
    //Concrete Builder class for constructing a basic damaging ability
    //</summary>
    class DamageAbilityBuilder : AbilityBuilder
    {
        public void BuildData()
        {

        }
        public void BuildEffect()
        {

        }
        public void BuildHitBox()
        {

        }
    }

    //Self Buff Ability Builder
    //<summary>
    //Concrete Builder class for constructing a basic ability that applies a
    //damage increase buff
    //</summary>
    class SelfBuffAbilityBuilder : AbilityBuilder
    {
        public void BuildData()
        {

        }
        public void BuildEffect()
        {

        }
        public void BuildHitBox()
        {

        }
    }

}
