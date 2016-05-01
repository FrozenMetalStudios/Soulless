using UnityEngine;
using System.Collections;
using ARK.Player.Ability;
using ARK.Player.Ability.Effects;

namespace ARK.Player.Ability.Builders
{
    //Builder class interface
    public interface AbilityBuilder
    {
        //Prototype function calls for building different parts of Ability
        void BuildStatistics(AbilityStats stats);
        void BuildDevInformation(AbilityInformation info);
        void BuildEffect(eEffectType type);

        Ability _Ability { get; }
    }

    //Concrete Builder classes

    //Damage Ability Builder
    //<summary>
    //Concrete Builder class for constructing a basic damaging ability
    //</summary>
    class MeleeBuilder : AbilityBuilder
    {
        public Ability ability;

        public MeleeBuilder()
        {
            ability = new Ability();
            ability.type = eAbilityType.Melee;
        }
        public MeleeBuilder(eEquippedSlot slot, eAbilityType type, eAbilityCast cast)
        {
            ability = new Ability(slot, type, cast);
            ability.type = eAbilityType.Melee;

        }

        public void BuildStatistics(AbilityStats stats)
        {
            ability.Statistics = stats;
        }
        public void BuildDevInformation(AbilityInformation info)
        {
            ability.DevInformation = info;
        }
        public void BuildEffect(eEffectType type)
        {
            ability.Effect.effectkey = type;

            switch(type)
            {
                case eEffectType.Damage:
                    break;
                case eEffectType.DamageOverTime:
                    break;
                case eEffectType.Slow:
                    break;
                case eEffectType.Stun:
                    break;
                case eEffectType.undefined:
                    break;
            }


        }

        public Ability _Ability
        {
            get { return ability; }
        }
    }

    //Self Buff Ability Builder
    //<summary>
    //Concrete Builder class for constructing a basic ability that applies a
    //damage increase buff
    //</summary>
    class RangedBuilder : AbilityBuilder
    {
        Ability ability;

        public RangedBuilder()
        {
            ability = new Ability();
            ability.type = eAbilityType.Ranged;
        }

        public void BuildStatistics(AbilityStats stats) { }
        public void BuildDevInformation(AbilityInformation info) { }
        public void BuildEffect(eEffectType type) { }

        public Ability _Ability
        {
            get { return ability; }
        }
    }

}
