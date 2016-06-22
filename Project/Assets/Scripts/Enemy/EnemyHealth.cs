using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Enemy Health
//<summary>
// Deals with anything associated with Enemies Health functionality
//</summary>
public class EnemyHealth : Health {

    public Slider healthSlider;                     //Reference to the slider object
    public int startingHealth = 100;                //Enemy health on start of game
    public int currentHealth;                       //Enemies current health
    public int scoreValue = 10;                     //The amount added to the players score when the enemy dies
    //public AudioClip dealthClip;                    //Enemies death audio clip

    //private Animator anim;                          //Reference to enemy characters animator
   // private AudioSource enemyAudio;                 //Reference to enemy characters audio source
    private ParticleSystem hitParticles;            //Reference to the particle system that plays when enemy is damaged
    private CapsuleCollider capsuleCollider;        //Reference to the enemies capsule collider
    private bool isDead;                            //Check to see if the enemy has died

    // Use this for initialization
    void Awake () {
        //Setting up the references
        //anim = GetComponent<Animator>();
        //enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponent<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        //setting the current health when the enemy first spawns
        currentHealth = startingHealth;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    override public void TakeDamage(int amount)
    {
        // If the enemy is dead...
        if (isDead)
        {
            // ... no need to take damage so exit the function.
            return;
        }


        // Play the hurt sound effect.
        //enemyAudio.Play();
        
        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        //Update the health slider with the value of the current health
        healthSlider.value = currentHealth;

        // And play the particles.
        //hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }

    override public void Death()
    {
        // The enemy is dead.
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        //capsuleCollider.isTrigger = true;

        // Tell the animator that the enemy is dead.
        //anim.SetTrigger("Dead");
        Destroy(gameObject);
        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        //enemyAudio.clip = deathClip;
       // enemyAudio.Play();
    }
}
