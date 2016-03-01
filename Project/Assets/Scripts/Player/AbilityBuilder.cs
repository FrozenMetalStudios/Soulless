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

    //Damage Over Time Ability Builder
    //<summary>
    //Concrete Builder class for constructing a basic ability that applies a
    //damage over time effect to a selected number of targets target
    //</summary>
    class DamageOverTimeAbility : AbilityBuilder
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
