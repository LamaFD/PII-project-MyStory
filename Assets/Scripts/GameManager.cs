using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Used in all the scenes
    // Helps manage the game
    // Contains properties and fonctions needed in multiple scenes

    public static GameManager manager;

    public List<Vector3> pnjs_positions; // vectors containing the positions of all the pnj on the map
    private Vector3 playerPositionBeforeInteraction; // contains the position of the player on the map before the interaction begins
    public int pnjId; // Id of the PNJ with whom the player is interacting

    // Needs to be filled at the Start Screen assets menue
    [SerializeField] public SO_PNJ[] pnjs; // all pnjs present in the game
    [SerializeField] public SO_CharacterBody player; // player's body
    public Vector3 initialPositionOfPlayer; // Position of the player at the begining of the game
    public double hairTressesIdlePosition; // Idle position of the object containing the hair if the hair is : "Tresses longues"
    public double hairTressesMovingPosition; // Moving position of the object containing the hair if the hair is : "Tresses longues"
    public double hairBobPosition; // Position of the object containing the hair if the hair is : "Bob bleu"

    void Awake()
    {
        // if the manager does not already exist in the scene then this object is the manager and should not be destroyed
        if(manager==null)
        {
            manager = this;
            DontDestroyOnLoad(this);
        }
        // if the manager already exist then destroy this object
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // Reset the bool inRange of the pnjs to false
        foreach (SO_PNJ pnj in pnjs)
        {
            pnj.isInRange = false;
        }
        // Set the initial position of the player at the begining of the game
        playerPositionBeforeInteraction = initialPositionOfPlayer;
    }

    // Allows the change in scenes, associated to buttons in all the scenes
    public void ChangeScene(string name)
    {
        if(name=="Interaction")
        {
            // Save player position
            SavePlayerPositionBeforeInteraction();
            // Save position of all the PNJ using the PNJ Position Manager present in the scene
            GameObject PNJ_Position_Manager = GameObject.Find("PNJ_Position_Manager");
            pnjs_positions = PNJ_Position_Manager.GetComponent<PNJ_Position_Manager>().GetPNJsPositions();
        }
        if (name=="Start Screen" && SceneManager.GetActiveScene().name=="MainGame")
        {
            // Reset the initial position of the player at the begining of the game
            playerPositionBeforeInteraction = initialPositionOfPlayer;
        }
        if (name == "Start Screen" && SceneManager.GetActiveScene().name == "Character Personalization")
        {
            // Change player name before loading next scene
            UpdatePlayerName();
        }
        SceneManager.LoadScene(name);
    }

    // Update the Id of the PNJ when the interaction is activated by the player
    // Used in PNJ_Manager
    public void UpdatePNJId(int Id)
    {
        pnjId = Id;
    }

    // Get the name of the PNJ to show on the button in the Interaction scene
    public string GetPNJName()
    {
        return pnjs[pnjId-1].pnjName;
    }

    // Update the player's name using the InputNameManager which controles the InputFiled in Characteer Personalization
    public void UpdatePlayerName()
    {
        InputNameManager inputNameManager = GameObject.Find("InputNameManager").GetComponent<InputNameManager>();
        inputNameManager.ChangePlayerName();
    }

    // Allows access to the player's name in other scripts : InputNameManager
    public string GetPlayerName()
    {
        return player.playerName;
    }

    // Changes player hair depending on its type and the direction taked by the player
    // Used in multiple scripts : BodyPartsManager, BodyPartsSelector, PlayerMouvement
    public void ChangeHairYPosition(bool isWlakingRightOrLeftOrUp)
    {
        // Get the name of the hair used in the player's body
        string hairName = player.characterBodyParts[0].bodyPart.bodyPartName;
        // Get hair object in the scene
        GameObject hair = GameObject.Find("Hair");

        // If the hair is "Tresses longues"
        if (hairName == "Tresses longues" && isWlakingRightOrLeftOrUp)
            hair.transform.localPosition = new Vector3(hair.transform.localPosition.x, (float)hairTressesMovingPosition, hair.transform.localPosition.z);
        if (hairName == "Tresses longues" && isWlakingRightOrLeftOrUp == false)
            hair.transform.localPosition = new Vector3(hair.transform.localPosition.x, (float)hairTressesIdlePosition, hair.transform.localPosition.z);

        // If the hair is "Bob bleu"
        else if (hairName == "Bob bleu")
            hair.transform.localPosition = new Vector3(hair.transform.localPosition.x, (float)hairBobPosition, hair.transform.localPosition.z);

    }

    // Saves the player position before the interaction to be able to replace the player in this position after the interaction is finished
    public void SavePlayerPositionBeforeInteraction()
    {
        // search for the player and save their position
        GameObject player = GameObject.Find("Player");
        playerPositionBeforeInteraction = player.transform.position;
    }

    // Used in the BodyPartManager when the MainGame scene start so as to know the position of the player
    public void UpdatePlayerPositionBeforeInteraction()
    {
        // search for the player and update their position
        GameObject player = GameObject.Find("Player");
        player.transform.position = playerPositionBeforeInteraction;
    }
}
