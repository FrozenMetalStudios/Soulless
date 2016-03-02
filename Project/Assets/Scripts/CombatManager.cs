using UnityEngine;
using Assets.Scripts.Utility;


//Combat Manager
//<summary>
//Manages all game combat with the different types of enemies( trash, bosses, etc) and global combat
public class CombatManager : MonoBehaviour
{
    // --------------------------------------------------------------------
    static CombatManager _Singleton = null;

    // --------------------------------------------------------------------
    public static CombatManager Singleton
    {
        get { return _Singleton; }
    }

    void Awake()
    {
        // Ensure only 1 singleton
        if (null != _Singleton)
        {
            ARKLogger.LogMessage(eLogCategory.General,
                                 eLogLevel.System,
                                 "CombatManager: Multiple CombatManager violate Singleton pattern.");
        }
        _Singleton = this;
    }

    #region Unimplemented Unity Callbacks
    // Use this for initialization
    void Start () {}
    
    // Update is called once per frame
    void Update () {}
    #endregion


    //Damages Collided enemy object
    public void DamageEnemy(Collider2D enemyCollider, int damage)
    {
        // ARKTODO: Think about the implications and performance of this call
        enemyCollider.SendMessageUpwards(CombatActions.TakeDamage, damage);
    }
}
