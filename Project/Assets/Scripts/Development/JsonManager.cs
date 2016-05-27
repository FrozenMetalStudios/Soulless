using System.Collections.Generic;
using System;
using System.IO;
using ARK.Utility.Ability;
using Newtonsoft.Json;
/// <summary>
/// Base Class for json parsers
/// </summary>
/// <typeparam name="T"></typeparam>
abstract public class  Json<T>
{
    /// <summary>
    /// Load the specified ability
    /// </summary>
    /// <param name="id"> JSON id for given ability </param>
    /// <returns> The specified object </returns>
    abstract public T Load(string id);

    /// <summary>
    /// Save the given type object to the specified path
    /// </summary>
    /// <param name="item">Object to be saved to JSON file </param>
    /// <param name="path"> location to be saved </param>
    abstract public void Save(T item, string path);
}

/// <summary>
/// This class extracts information related to Player Statisitics from JSON files
/// </summary>
public class PlayerInformation : Json<string>
{
    public PlayerInformation()
    {

    }
    public override void Save(string stats, string path)
    {

    }
    public override string Load(string id)
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
    public PlayerInformation StatsParsers;

    public JsonManager()
    {
        StatsParsers = new PlayerInformation();

    }

    public List<JSONUtility.AbilityObj> LoadAbilityDatabase(string path)
    {
        string jsonText = File.ReadAllText(path);
        List<JSONUtility.AbilityObj> database = new List<JSONUtility.AbilityObj>();
        database = JsonConvert.DeserializeObject< List<JSONUtility.AbilityObj> >(jsonText);
        return database;
    }
}

