using UnityEngine;
using System.Collections;

//Melee Attack Trigger
//<summary>
//Players melee trigger that collides with enemy or object to create a trigger event
//</summary>
public class MeleeAttackTrigger : MonoBehaviour
{
    public int dmg;                         //damage to be dealt to collided objects
    public CombatManager _CombatMngr;       //Combat manager that deals with combat related situations

    //Global function for player to update the triggers damage
    public void UpdateDamage(int damage)
    {
        dmg = damage;
    }

    //Trigger event when melee trigger collides with 2D object
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //check to see if trigger collides with an enemy
        if(collider.isTrigger != true && collider.tag == "Enemy")
        {
            _CombatMngr.DamageEnemy(collider, dmg);
        }
    }

}
