using UnityEngine;
using System;
using UnityEngine.UI;
using ARK.Player.Ability;
using System.Collections;
using Assets.Scripts.Utility;

// Skill Bar Element
//<summary>
// Contains all the necessar UI elements to do with players skills
//  - Image placeholder
//  - Text placeholder (for cooldown timers)
//</summary>
public class SkillBarElement
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
public class SkillBar
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
    public void LoadEquippedSkillImages(PlayerProfile player)
    {
        Attack1.image.sprite = Resources.Load<Sprite>(player.EquippedAbilities[Convert.ToInt32(eEquippedSlot.AttackSlot1)].information.spritepath);
        Attack2.image.sprite = Resources.Load<Sprite>(player.EquippedAbilities[Convert.ToInt32(eEquippedSlot.AttackSlot2)].information.spritepath); ;
        Spell1.image.sprite = Resources.Load<Sprite>(player.EquippedAbilities[Convert.ToInt32(eEquippedSlot.SpellSlot1)].information.spritepath); ;
        Spell2.image.sprite = Resources.Load<Sprite>(player.EquippedAbilities[Convert.ToInt32(eEquippedSlot.SpellSlot2)].information.spritepath); ;
        Spell3.image.sprite = Resources.Load<Sprite>(player.EquippedAbilities[Convert.ToInt32(eEquippedSlot.SpellSlot3)].information.spritepath); ;
        Ultimate.image.sprite = Resources.Load<Sprite>(player.EquippedAbilities[Convert.ToInt32(eEquippedSlot.UltimateSlot)].information.spritepath); ;
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
    public Slider energySlider;                     //Energy slider for players energy
    public Slider corruptionSlider;                 //Corruption slider for players in-game corruption

    public PlayerProfile playerProfile;             //PlayerProfile which holds all information about the player
    public Image[] hudImages = new Image[6];        //Image holders for abilities
    public Text[] coolDownText = new Text[6];       //Text boxes for ability cooldowns
    public CorruptionManager corruptManager;
    private SkillBar playerSkillBar;


    #region Unity callbacks
    // Use this for initialization
    void Start()
    {
        //Load the saved players ability images into the skill bar
        playerSkillBar = new SkillBar(hudImages, coolDownText);
        corruptManager = new CorruptionManager();

        playerSkillBar.LoadEquippedSkillImages(playerProfile);
        energySlider.maxValue = playerProfile.maxEnergy;
        corruptionSlider.maxValue = playerProfile.maxEnergy;

        energySlider.value = playerProfile.maxEnergy;
        corruptionSlider.value = 0;
        corruptManager = new CorruptionManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (energySlider.value < playerProfile.maxEnergy)
        {
            energySlider.value += playerProfile.energyRegen * Time.deltaTime;
            if(energySlider.value > playerProfile.maxEnergy)
            {
                energySlider.value = playerProfile.maxEnergy;
            } 
        }

        if(corruptionSlider.value > 0)
        {
            corruptionSlider.value -= playerProfile.corruptionDegen * Time.deltaTime;
            if(corruptionSlider.value < 0)
            {
                corruptionSlider.value = 0;
            }
        }

    }
    #endregion

    public void PlayerCastedAbility(Ability castedAbility)
    {
        if (castedAbility.offCooldown)
        {
            HandleCooldown(castedAbility);
            HandleEnergy(castedAbility.statistics.energy);

            corruptManager.ModifyMeter(castedAbility);
            HandleCorruption(castedAbility.cast);
        }

    }

    //Handles energy costs for players energy slider
    private void HandleEnergy(int cost)
    {
        if (energySlider.value != energySlider.minValue)
        {
            energySlider.value -= cost;
        }
        else
        {
            ARKLogger.LogMessage(eLogCategory.Control,
                                 eLogLevel.Info,
                                 "Player does not have enough energy to cast ability");
        }
    }

    //Handles corruption costs for players corruption slider
    private void HandleCorruption(eAbilityCast cast)
    {
        corruptionSlider.value = corruptManager.corruptionMeter;
    }
    // Handles cooldown values for players skill bar
    private void HandleCooldown(Ability ability)
    {
        SkillBarElement hudElement = DetermineHudElement(ability.slot);
        StartCoroutine(CooldownHandler(ability, hudElement));

    }

    // Coroutine used for ability cooldown
    IEnumerator CooldownHandler(Ability ability, SkillBarElement hudElement)
    {
        hudElement.image.color = Color.black;
        yield return new WaitForSeconds(ability.statistics.cooldown);
        hudElement.image.color = Color.white;
    }

    // Internal function for determing which ability is associated with which UI element
    private SkillBarElement DetermineHudElement(eEquippedSlot ability)
    {
        switch (ability)
        {
            case eEquippedSlot.AttackSlot1:
                return playerSkillBar.Attack1;
            case eEquippedSlot.AttackSlot2:
                return playerSkillBar.Attack2;
            case eEquippedSlot.SpellSlot1:
                return playerSkillBar.Spell1;
            case eEquippedSlot.SpellSlot2:
                return playerSkillBar.Spell2;
            case eEquippedSlot.SpellSlot3:
                return playerSkillBar.Spell3;
            case eEquippedSlot.UltimateSlot:
                return playerSkillBar.Ultimate;
            default:
                return null;
        }
    }
}
