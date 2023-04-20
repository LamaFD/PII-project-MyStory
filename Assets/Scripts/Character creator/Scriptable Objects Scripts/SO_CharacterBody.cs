using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allowes the creation of an object
[CreateAssetMenu(fileName = "New Character Body", menuName = "Character Body")]  
public class SO_CharacterBody : ScriptableObject
{
    // Scriptable object representing the body of the player : holds details about the full character body

    public string playerName; // Initiated value in Unity
    public BodyPart[] characterBodyParts; // Has to hold in order : Hair, Body, Outfit

    // Allows the name to be updated in the scene ; character personalization
    public void UpdateName(string name) 
    {
        playerName=name;
    }

    // Allows name to be accessed
    public string GetName() 
    {
        return playerName;
    }
}

[System.Serializable]
public class BodyPart // defines the bodyparts contained by the CharacterBody
{
    public string bodyPartName; // Name of the body part : Hair, Outfit or Body
    public SO_BodyPart bodyPart; // The bodyPart scriptable object that makes part of the body
}
