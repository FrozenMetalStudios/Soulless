using ARK.Utility.Ability;
using ARK.Player.Ability.Effects;

namespace ARK.Player.Ability.Builders
{
    //Builder class interface
    public interface AbilityBuilder
    {
        //Prototype function calls for building different parts of Ability
        void BuildData(JSONUtility.AbilityObj temp);
        void BuildStatistics(JSONUtility.AbilityObj temp);
        void BuildDevInformation(JSONUtility.AbilityObj temp);
        void BuildEffect(JSONUtility.AbilityObj temp);

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
        public void BuildData(JSONUtility.AbilityObj temp)
        {
            ability.id = temp.id;
            ability.name = temp.name;
            ability.slot = Conversion.DetermineEquippedSlot(temp.slot);
            ability.type = Conversion.DetermineAbilityType(temp.type);
            ability.cast = Conversion.DetermineAbilityCast(temp.cast);
        }

        public void BuildStatistics(JSONUtility.AbilityObj temp)
        {
            ability.Statistics = Conversion.JSONtoStats(temp.statistics);
        }
        public void BuildDevInformation(JSONUtility.AbilityObj temp)
        {
            ability.DevInformation = Conversion.JSONtoInfomartion(temp.information);
        }
        public void BuildEffect(JSONUtility.AbilityObj temp)
        {
            eEffectType type = Conversion.DetermineEffect(temp.effect.effectKey);
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
        public void BuildData(JSONUtility.AbilityObj temp) { }
        public void BuildStatistics(JSONUtility.AbilityObj temp) { }
        public void BuildDevInformation(JSONUtility.AbilityObj temp) { }
        public void BuildEffect(JSONUtility.AbilityObj temp) { }

        public Ability _Ability
        {
            get { return ability; }
        }
    }

}
