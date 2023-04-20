using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    // Manages the interaction scene as a whole

    [SerializeField] public SO_CharacterBody player; // the player

    public bool pnjSpeaking; // Indicates if the pnj should be speaking or not ( = player is speaking)
    public int emotionID; // Identifies the emotion chosen by the player. Signification : 0: Neutre, 1:Tristesse, 2: Colere, 3:Joie
    private int stepNumber; // Indicates the step being played : 1: Character choice, 2: Writing Dialogue, 3:Emotion choice, 4: Showing dialaogue

    //// UI elements in the scene
    // Boxes for each step
    public GameObject boxSpeakerChoice;
    public GameObject boxDialogueInput;
    public GameObject boxEmotionChoice;
    public GameObject boxShowDialogue;
    private BackgroundManager bgManager;
    // Input field
    public InputField inputDialogue;
    // Text that needs to be modified
    public Text buttonPNJName;
    public Text buttonPlayerName;
    public Text speakerName;
    public Text ShownDialogue;
    // Visuel elements
    public GameObject dialogueBubblePNJ;
    public GameObject dialogueBubblePlayer;
    public GameObject background;


    private void Start()
    {
        // Set the text on the button according to the names of the player and PNJ
        SetCharactersNameOnButtons();
        // get the background manager from the scene
        bgManager=background.GetComponent<BackgroundManager>();
        // Sel all element for the begining 
        SetElementsForStart();
    }

    // Set all element to start an interaction cycle
    public void SetElementsForStart()
    {
        // Hide all boxes except box of speaker choice
        dialogueBubblePlayer.SetActive(false);
        dialogueBubblePNJ.SetActive(false);
        boxDialogueInput.SetActive(false);
        boxEmotionChoice.SetActive(false);
        boxShowDialogue.SetActive(false);
        boxSpeakerChoice.SetActive(true);
        // Reset the step nomber
        stepNumber = 1;
        // Resert dialogue text
        inputDialogue.text = " ";
        // Reset background
        bgManager.ChangeBackground(0);
    }
    
    // Allows activation of the next step in the interaction 
    private void ActivateNextBox()
    {
        // Next step activeted depending of the actual one
        if (stepNumber == 1)
        {
            boxSpeakerChoice.SetActive(true);
        }
        if (stepNumber == 2)
        {
            boxSpeakerChoice.SetActive(false);
            boxDialogueInput.SetActive(true);
        }
        if (stepNumber == 3)
        {
            boxDialogueInput.SetActive(false);
            boxEmotionChoice.SetActive(true);
        }
        if (stepNumber == 4)
        {
            boxEmotionChoice.SetActive(false);
            boxShowDialogue.SetActive(true);
            //show the visuel elements according to the player's choices
            if(pnjSpeaking)
                dialogueBubblePNJ.SetActive(true);
            else
                dialogueBubblePlayer.SetActive(true);
            //set background depending on the emotion
            bgManager.ChangeBackground(emotionID);
        }
    }

    // Used on the button associated to the PNJ
    public void PNJIsSpeaking()
    {
        pnjSpeaking = true;
        ChangeName();
    }
    // Used on the button associated to the PNJ
    public void PNJIsNotSpeaking()
    {
        pnjSpeaking = false;
        ChangeName();
    }

    // Changes the name of the speaker depending on the choice of the player
    private void ChangeName()
    {
        //define the name of the speaker
        if (pnjSpeaking) 
        {
            speakerName.text = GameManager.manager.GetPNJName();
        }
        else
        {
            speakerName.text = player.playerName;
        }

        // start next step
        stepNumber++;
        ActivateNextBox();
    }

    public void ContinueButton()
    {
        // Go to next step
        stepNumber++;
        ActivateNextBox();
    }

    public void ConfirmeDialogueButton()
    {
        // Assigne the text input by the player to the test that is going to be shown in the dialogue box
        ShownDialogue.text = inputDialogue.text;
        // Go to next step
        stepNumber++;
        ActivateNextBox();
    }

    // Changing emotion according to the button pressed
    // emotionId depends on the button
    public void ChangeEmotion(int emotionId)
    {
        emotionID = emotionId;
        // Go to next step
        stepNumber++;
        ActivateNextBox();
    }

    // Sets the name of the player and pnj on the buttons to chose the speaker
    public void SetCharactersNameOnButtons()
    {
        buttonPNJName.text = GameManager.manager.GetPNJName();
        buttonPlayerName.text = player.playerName;
    }
}
