using UnityEngine;
using ARK.Player.Ability;
using ARK.Player.Ability.Effects;
using Assets.Scripts.Utility;
using System.Collections;

//Melee Attack Trigger
//<summary>
//Players melee trigger that collides with enemy or object to create a trigger event
//</summary>
public class SkillTrigger : MonoBehaviour
{
    private Ability ability;                         //damage to be dealt to collided objects
    public CombatManager _CombatMngr;       //Combat manager that deals with combat related situations
    public Collider2D triggerCollider;

    public Ability CastedAbility
    {
        get { return ability; }
        set { ability = value; }
    }

    //Trigger event when melee trigger collides with 2D object
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //check to see if trigger collides with an enemy
        if(collider.isTrigger != true && collider.tag == "Enemy")
        {
            //move this to combat manager

            _CombatMngr.DamageEnemy(collider, ability);
        }
    }

}
