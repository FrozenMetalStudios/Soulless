using UnityEngine;
using System.Collections;
using PlayerAbilities;

public class BasePlayer : MonoBehaviour
{
    public int playerHealth;
    public int playerExp;
    public int playerLevel;
    public int lightPoints;
    public int darkPoints;

    public Abilities attack1;
    public Abilities attack2;
    public Abilities spell1;
    public Abilities spell2;
    public Abilities spell3;
    public Abilities ultimate;

}

public class PlayerMain: BasePlayer
{

}

