using UnityEngine;
using System.Collections.Generic;
using ARK.Player.Ability.Builders;
using ARK.Utility.Ability;

namespace ARK.Player.Ability.Manager
{
    //Ability System Manager
    //<summary>
    //Manages all the aspects of the ability system 
    //</summary>
    public class AbilityManager : MonoBehaviour
    {
        private AbilityBuilder _builder = null;
        JsonManager _jsonmanager;
        private string database_path = "/Resources/AbilityDatabase/database_test.json";
        private List<JsonAbilityObject> Database;

        public AbilityManager()
        {
            //Deseralize the ability database into the Database list
            _jsonmanager = new JsonManager();
            Database = _jsonmanager.LoadAbilityDatabase(Application.dataPath + database_path);
        }

        /// <summary>
        /// Builds the specified ability using its builder
        /// </summary>
        /// <param name="builder">builder used to contruct each part</param>
        /// <param name="template">dummy ability which contains information needed to contruct each part</param>
        /// <returns>contructed ability</returns>
        private Ability BuildAbility(AbilityBuilder builder, JsonAbilityObject template)
        {
            // Build each component of the ability
            builder.BuildData(template);
            builder.BuildStatistics(template);
            builder.BuildDevInformation(template);
            builder.BuildEffect(template);

            return builder._Ability;
        }

        /// <summary>
        /// Constructs the specified ability using the associated ability builder function
        /// </summary>
        /// <param name="abilityID">JSON id of ability</param>
        /// <returns>Contructed ability </returns>
        public Ability ConstructAbility(string abilityID)
        {
            JsonAbilityObject temp_ability = new JsonAbilityObject();
            Ability ability = new Ability();

            //load the necessary stats, information, effect, etc into the necessary fields and construct the ability
            //temp_ability = _jsonmanager.AbilityParser.Load(abilityID);
            temp_ability = FindAbility(abilityID);
            //Determine which builder to utilize for constructing the ability
            switch(Conversion.DetermineAbilityType(temp_ability.type))
            {
                case eAbilityType.Melee:
                    _builder = new MeleeBuilder();
                    break;
                case eAbilityType.Mobility:
                    break;
                case eAbilityType.Ranged:
                    _builder = new RangedBuilder();
                    break;
                case eAbilityType.SelfBuff:
                    break;
                case eAbilityType.Transform:
                    break;
                case eAbilityType.undefined:
                    return null;
            }
            //build ability
            ability  = BuildAbility(_builder, temp_ability);
            return ability;
        }

        private JsonAbilityObject FindAbility(string id)
        {
            return Database.Find(a => a.id == id);
        }
    }

}
