using UnityEngine;
using ARK.Player.Ability;
using Assets.Scripts.Utility;

//Player Attack
//<summary>
// Manage players combat and attacking
//</summary>
public class PlayerAttack : MonoBehaviour
{
    
    public PlayerProfile Player;                //Players profile

    #region Unity callbacks
    // Use this for initialization
    void Start () {
        //player = GetComponent<PlayerProfile>();
        Player.AbilityColliderTrigger.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Handles the different inputs the player can use for combat
        Ability abilityToCast;
         if (Input.GetButtonDown(ButtonNames.BasicAttack1))
        {
            abilityToCast = Player.DetermineAbility(eEquippedSlot.AttackSlot1);
            Player.combatManager.CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown(ButtonNames.BasicAttack2))
        {
            abilityToCast = Player.DetermineAbility(eEquippedSlot.AttackSlot2);
            Player.combatManager.CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown(ButtonNames.Ability1))
        {
            abilityToCast = Player.DetermineAbility(eEquippedSlot.SpellSlot1);
            Player.combatManager.CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown(ButtonNames.Ability2))
        {
            abilityToCast = Player.DetermineAbility(eEquippedSlot.SpellSlot2);

            Player.combatManager.CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown(ButtonNames.Ability3))
        {
            abilityToCast = Player.DetermineAbility(eEquippedSlot.SpellSlot3);
            Player.combatManager.CastAbility(abilityToCast);
        }
        else if (Input.GetButtonDown(ButtonNames.Ultimate))
        {
        }
        else
        {
            Player.AbilityColliderTrigger.enabled = false;
        }
    }
    #endregion
}
