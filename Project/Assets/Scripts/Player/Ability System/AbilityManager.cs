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
        AbilityBuilder _builder = null;
        //Construct Ability 
        //<summary>
        //Constructs the specified ability using the associated ability builder function
        //</summary>
        public void ConstructAbility(string abilityID)
        {
            //open up necessary XML file and find the node associate with the abilityID
            //load the necessary stats, information, effect, etc into the necessary fields and construct the ability

        }
    }

}
