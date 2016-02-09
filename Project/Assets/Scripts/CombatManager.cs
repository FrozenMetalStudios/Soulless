using UnityEngine;
using System.Collections;
using Assets.Scripts.Utility;

//Combat Manager
//<summary>
//Manages all game combat with the different types of enemies( trash, bosses, etc) and global combat
public class CombatManager : MonoBehaviour
{

    #region Unity Callbacks
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    #endregion

    //Damages Collided enemy object
    public void DamageEnemy(Collider2D enemyCollider, int damage)
    {
      
        enemyCollider.SendMessageUpwards(CombatActions.TakeDamage, damage);
    }
}
