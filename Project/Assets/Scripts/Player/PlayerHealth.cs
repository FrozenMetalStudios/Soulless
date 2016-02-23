using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Utility;

//Player Health
//<summary>
//Deals with anything associated with players health functionality
//</summary>
public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;                                       //Players current health 
    public Slider healthSlider;                                     //Reference to the slider object
    public Image damageImage;                                       //Reference to the image that will be flashed whne player takes damage
    //public AudioClip dealthClip;                                    //The audio clip that plays when player dies
    public float flashSpeed = 5f;                                   //The speed the death image will flash
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);         //The color of the death image

    private Animator anim;                                          //Reference to the players animator
    //private AudioSource playerAudio;
    private PlayerMovement playerMovement;                        //Reference to the player movement controller so we can disable movement when player dies
    public PlayerProfile playerProfile;                               //Reference to the characters stats, abilites etc

    private bool isDead;                                            //Players death flag
    private bool isDamaged;                                         //Player is damaged flag

	// Use this for initialization
	void Awake () {
        //Get necessary references
        anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        //playerProfile = GetComponent<PlayerProfile>();

        //Set the players health to the startingHealth when player spawns
        currentHealth = playerProfile.playerHealth;
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
        //if character is damaged...
        if (isDamaged)
        {
            //... flash the damage images
            //damageImage.color = flashColour;
        }
        else
        {
            //... transition the color back to a clear background
            //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        isDamaged = false;
	}

    public void TakeDamage(int amount)
    {
        ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.System, "player taking damage: " + amount.ToString());
        //set the damage flag so the damage image will flash
        isDamaged = true;

        //Reduce the current health of the player
        currentHealth -= amount;

        //Update the health slider with the value of the current health
        healthSlider.value = currentHealth;

        //Play Hurt audio clip
        //playerAudio.Play();

        //Check to see if the players health is less than 0 and the dealth flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            //... player should die
            ARKLogger.LogMessage(eLogCategory.Combat, eLogLevel.Warning, "Player has died!");
            Death();
        }
    }

    private void Death()
    {
        //Set the death flag so this function wont go into a loop
        isDead = true;

        // Tell the animator that the player is dead
        anim.SetTrigger("Die");

        //Set the player audio clip to the death clip and play it
       //playerAudio.clip = dealthClip;
        //playerAudio.Play();

        //disable the players movement
        playerMovement.enabled = false;
        Destroy(gameObject);
    }
}
