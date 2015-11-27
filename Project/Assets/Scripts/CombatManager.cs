using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DamageEnemy(Collider2D enemyCollider, int damage)
    {
        if (enemyCollider.isTrigger != true && enemyCollider.tag == "Enemy")
        {
            enemyCollider.SendMessageUpwards("TakeDamage", damage);
        }
    }
}
