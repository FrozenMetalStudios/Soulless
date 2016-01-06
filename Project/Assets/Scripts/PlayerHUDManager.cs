using UnityEngine;
using UnityEngine.UI;
using PlayerAbilities;
using System.Collections;

// Skill Bar Element
//<summary>
// Contains all the necessar UI elements to do with players skills
//  - Image placeholder
//  - Text placeholder (for cooldown timers)
//</summary>
public class SkillBarElement : MonoBehaviour
{
    public Image image;     //UI image
    public Text text;       //UI Text

    //constructor for creating skill bar element class
    public SkillBarElement(Image img, Text txt)
    {
        image = img;
        text = txt;
    }
}

// Skill Bar 
//<summary>
// Contains all the UI elements for the players skill bar
//</summary>
public class SkillBar : MonoBehaviour
{
    public SkillBarElement Attack1;         //contains UI elements for players Attack 1
    public SkillBarElement Attack2;         //contains UI elements for players Attack 2
    public SkillBarElement Spell1;          //contains UI elements for players Spell 1
    public SkillBarElement Spell2;          //contains UI elements for players Spell 2
    public SkillBarElement Spell3;          //contains UI elements for players Spell 3
    public SkillBarElement Ultimate;        //contains UI elements for players Ultimate

    //Constructor for adding the defined UI image placeholders and UI Text placeholders to SkillBarElements
    public SkillBar(Image[] hudImgs, Text[] cdTxt)
    {
        Attack1 = new SkillBarElement(hudImgs[0], cdTxt[0]);
        Attack2 = new SkillBarElement(hudImgs[1], cdTxt[1]);
        Spell1 = new SkillBarElement(hudImgs[2], cdTxt[2]);
        Spell2 = new SkillBarElement(hudImgs[3], cdTxt[3]);
        Spell3 = new SkillBarElement(hudImgs[4], cdTxt[4]);
        Ultimate = new SkillBarElement(hudImgs[5], cdTxt[5]);
    }

    //Loads the saved images that are in the players profile
    public void loadEquippedSkillImages(PlayerProfile player)
    {
        Attack1.image.sprite = player.Attack1.AbilityImage;
        Attack2.image.sprite = player.Attack2.AbilityImage;
        Spell1.image.sprite = player.Spell1.AbilityImage;
        Spell2.image.sprite = player.Spell2.AbilityImage;
        Spell3.image.sprite = player.Spell3.AbilityImage;
        Ultimate.image.sprite = player.Ultimate.AbilityImage;
    }

}

// Player HUD Manager
//<summary>
// Manages the entire Player Heads-Up-Display which includes the following
// - Players Skill Bar : Images, cooldown timer values display 
// - Players Health Bar 
//</summary>
public class PlayerHUDManager : MonoBehaviour
{

    public Slider healthSlider;                     //Health slider for players health
    public PlayerProfile playerProfile;             //PlayerProfile which holds all information about the player
    public Image[] hudImages = new Image[6];        //Image holders for abilities
    public Text[] coolDownText = new Text[6];       //Text boxes for ability cooldowns
    private SkillBar playerSkillBar;


    #region Unity callbacks
    // Use this for initialization
    void Start()
    {
        //Load the saved players ability images into the skill bar
        playerSkillBar = new SkillBar(hudImages, coolDownText);
        playerSkillBar.loadEquippedSkillImages(playerProfile);

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    // Handles cooldown values for players skill bar
    public void HandleCooldown(Abilities ability)
    {
        SkillBarElement hudElement = determineHudElement(ability.attackType);
        StartCoroutine(CooldownHandler(ability, hudElement));

    }

    // Coroutine used for ability cooldown
    IEnumerator CooldownHandler(Abilities ability, SkillBarElement hudElement)
    {
        print(ability.isOffCooldown.ToString());
        hudElement.image.color = Color.black;
        yield return new WaitForSeconds(ability.CoolDown);
        hudElement.image.color = Color.white;
        print(ability.isOffCooldown.ToString());


    }

    // Internal function for determing which ability is associated with which UI element
    private SkillBarElement determineHudElement(ePlayerAbilities ability)
    {
        switch (ability)
        {
            case ePlayerAbilities.Attack1:
                return playerSkillBar.Attack1;
            case ePlayerAbilities.Attack2:
                return playerSkillBar.Attack2;
            case ePlayerAbilities.Spell1:
                return playerSkillBar.Spell1;
            case ePlayerAbilities.Spell2:
                return playerSkillBar.Spell2;
            case ePlayerAbilities.Spell3:
                return playerSkillBar.Spell3;
            case ePlayerAbilities.Ultimate:
                return playerSkillBar.Ultimate;
            default:
                return null;
        }
    }
}
