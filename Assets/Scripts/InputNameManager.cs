using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNameManager : MonoBehaviour
{
    // Used in Character Personalization
    // Used by the GameManager 

    public InputField playerNameInput; // The Input field of the Name
    public string inputName; 
    [SerializeField] public SO_CharacterBody player;

    // Start is called before the first frame update
    void Start()
    {
        // Get player name from manager to show in the input field and be able to change it
        inputName = GameManager.manager.GetPlayerName();
        // Show name in input
        playerNameInput.text = inputName;
    }

    // Allows to get the Name in the InputField
    public string GetInputName()
    {
        inputName = playerNameInput.text;
        return inputName;
    }

    // Changes the name of the player, used in GameManager
    public void ChangePlayerName()
    {
        player.UpdateName(playerNameInput.text);
    }
}
