using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour {

    public int dmg;                         //damage to be dealt to collided objects
    private CombatManager _CombatMngr;       //Combat manager that deals with combat related situations
    private bool inRange = false;
    private Collider2D detectedPlayer;

    void Awake()
    {
        this._CombatMngr = CombatManager.Singleton;
    }

    //Global function for player to update the triggers damage
    public void updateDamage(int damage)
    {
        dmg = damage;
    }

    //Trigger event when melee trigger collides with 2D object
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //check to see if trigger collides with an enemy
        inRange = true;
        if(collider.tag == "Player")
        {
            detectedPlayer = collider;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
        detectedPlayer = null;
    }

    public void DamageObject()
    {
        if (inRange && detectedPlayer != null)
        {

            _CombatMngr.DamageEnemy(detectedPlayer, dmg);
        }
    }
}
