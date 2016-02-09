using UnityEngine;
using System.Collections;
using Assets.Scripts.Utility;


//Enemy Ranged Attack
//<summary>
//Enemy attacking script for melee type enemies
//</summary>
public class EnemyRangedAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    private Animator anim;                              // Reference to the animator component.
    private GameObject player;                          // Reference to the player GameObject.
    private PlayerHealth playerHealth;                  // Reference to the player's health.
    private EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    private bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    private float timer;                                // Timer for counting up to the next attack.

    #region Unity callbacks
    // Use this for initialization
    void Start () {
        Logger.LogMessage(eLogCategory.General, eLogLevel.System, "initalizing ranged enemy");
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Logger.LogMessage(eLogCategory.General, eLogLevel.System, "enemy attacking player");
            //attack.
            Shoot();
        }

        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            //tell the animator the player is dead.
            anim.SetTrigger("PlayerDead");
        }

    }
    #endregion

    void OnTriggerEnter2D(Collider2D other)
    {
        Logger.LogMessage(eLogCategory.General, eLogLevel.System, "player in range of enemy collider");
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Shoot()
    {

    }
}
