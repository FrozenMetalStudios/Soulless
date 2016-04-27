using UnityEngine;
using System.Collections;
using System.IO;
using ARK.Player.Ability.Builders;
using ARK.Player.Ability.Effects;

namespace ARK.Player.Ability.Manager
{
    //Ability System Manager
    //<summary>
    //Manages all the aspects of the ability system 
    //</summary>
    public class AbilityManager : MonoBehaviour
    {
        AbilityBuilder _builder = null;


        /// <summary>
        /// Builds the specified ability using its builder
        /// </summary>
        /// <param name="builder">builder used to contruct each part</param>
        /// <param name="template">dummy ability which contains information needed to contruct each part</param>
        /// <returns>contructed ability</returns>
        private Ability BuildAbility(AbilityBuilder builder, Ability template)
        {
            Ability ability = new Ability();

            // Build each component of the ability
            builder.BuildStatistics(template.Statistics);
            builder.BuildDevInformation(template.DevInformation);
            builder.BuildEffect(template.Effect.effectkey);

            return builder._Ability;
        }

        /// <summary>
        /// Constructs the specified ability using the associated ability builder function
        /// </summary>
        /// <param name="abilityID">JSON id of ability</param>
        /// <returns>Contructed ability </returns>
        public Ability ConstructAbility(string abilityID)
        {
            string filepath = "/Resources/AbilityDatabase/test.json";
            JsonManager _jsonmanager = new JsonManager();
            Ability temp_ability = new Ability();
            Ability ability = new Ability();

            //obtain full file path of ability json file
            _jsonmanager.LoadFile(Application.dataPath + filepath);

            //load the necessary stats, information, effect, etc into the necessary fields and construct the ability
            temp_ability = _jsonmanager.AbilityParser.Load("0x0001");
            
            //Determine which builder to utilize for constructing the ability
            switch(temp_ability.type)
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
    }

}
