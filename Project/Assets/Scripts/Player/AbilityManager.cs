using UnityEngine;
using System.Collections;
using ARK.Player.Ability.Builders;

namespace ARK.Player.Ability.Manager
{
    //Ability System Manager
    //<summary>
    //Manages all the aspects of the ability system 
    //</summary>
    public class AbilityManager : MonoBehaviour
    {
        //Construct Ability 
        //<summary>
        //Constructs the specified ability using the associated ability builder function
        //</summary>
        public void ConstructAbility(AbilityBuilder abilityBuilder)
        {
            abilityBuilder.BuildData();
            abilityBuilder.BuildEffect();
            abilityBuilder.BuildHitBox();
        }
    }

}
