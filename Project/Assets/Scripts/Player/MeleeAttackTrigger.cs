using UnityEngine;
using System.Collections;

public class MeleeAttackTrigger : MonoBehaviour {

    public int dmg;
    public CombatManager _CombatMngr;

    public void updateDamage(int damage)
    {
        dmg = damage;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.isTrigger != true && collider.tag == "Enemy")
        {
            _CombatMngr.DamageEnemy(collider, dmg);
        }
    }

}
