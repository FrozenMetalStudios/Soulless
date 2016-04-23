using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EntityManager : MonoBehaviour {

    private List<PlayerController> players = new List<PlayerController>();

    // --------------------------------------------------------------------
    static EntityManager _Singleton = null;

    // --------------------------------------------------------------------
    public static EntityManager Singleton
    {
        get { return _Singleton; }
    }

    // Use this for initialization
    void Awake()
    {
        // Ensure only 1 singleton
        if (null != _Singleton)
        {
            UnityEngine.Debug.LogError("EntityManager: Multiple EntityManager violate Singleton pattern.");
        }
        _Singleton = this;
    }

    public static void RegisterPlayer(
        PlayerController player
        )
    {
        EntityManager.Singleton.players.Add(player);
    }

    public static PlayerController GetRandomPlayer()
    {
        return EntityManager.Singleton.players[0];
    }
}
