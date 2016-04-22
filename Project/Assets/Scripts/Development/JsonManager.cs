using UnityEngine;
using System.Collections;
using ARK.Player.Ability;
using SimpleJSON;
using System;


abstract public class  Json<T>
{
    abstract public T Load(string path, string id);
    abstract public void Save(T item, string path);
    abstract public void Add(T item, string path);
    abstract public void Remove(T item, string path);
}

public class PlayerAbility : Json<AbilityStats>
{
    public PlayerAbility()
    {

    }

    public override void Save(AbilityStats stats, string path)
    {

    }

    public override AbilityStats Load(string path, string id)
    {
        AbilityStats stats = new AbilityStats();
        JSONNode file = JSON.Parse(path);
        stats.damage = int.Parse(file[id]["data"]["combat"]["damage"]);
        stats.cooldown = int.Parse(file[id]["data"]["combat"]["cooldown"]);
        stats.energy = int.Parse(file[id]["data"]["combat"]["energy"]);
        stats.corruption = int.Parse(file[id]["data"]["combat"]["corruption"]);
        stats.modifier = int.Parse(file[id]["data"]["combat"]["modifier"]);

        return stats;
    }

    public override void Add(AbilityStats item, string path)
    {
        throw new NotImplementedException();
    }
    public override void Remove(AbilityStats item, string path)
    {
        throw new NotImplementedException();
    }

}
public class PlayerStats : Json<string>
{
    public PlayerStats()
    {

    }
    public override void Save(string stats, string path)
    {

    }
    public override string Load(string path, string id)
    {
        throw new NotImplementedException();
    }
    public override void Add(string item, string path)
    {
        throw new NotImplementedException();
    }
    public override void Remove(string item, string path)
    {
        throw new NotImplementedException();
    }
}

//JSON Manager
//<summary>
// Wrapper class for SimpleJSON Interface
// This wrapper will parse our game specific formats
//</summary>
public class JsonManager 
{
    public PlayerAbility AbilityParser;
    public PlayerStats StatsParsers;
    public JsonManager()
    {
        AbilityParser = new PlayerAbility();
        StatsParsers = new PlayerStats();

    }
}
