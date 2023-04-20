using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartsSelector : MonoBehaviour
{
    // Used in the scene Character Personalization
    // Associated to the panel : ModificationPanel
    // Updates All Animations to Match Player Selections

    [SerializeField] private SO_CharacterBody characterBody; // body of the player
    [SerializeField] private BodyPartSelection[] bodyPartSelections; // different Body Parts available

    private void Start()
    {
        // Get all current body parts
        for(int i = 0; i < bodyPartSelections.Length; i++)
        {
            GetCurrentBodyParts(i);
        }
    }

    // Change to a previous Body Part in the list
    public void PreviousBodyPart(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            // If there is a Body Part before the current one
            if (bodyPartSelections[partIndex].bodyPartCurrentIndex > 0)
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex--;
            }
            // If the current Body Part is the first in the list 
            else
            {
                // Go to the last Body Part in the list
                bodyPartSelections[partIndex].bodyPartCurrentIndex = bodyPartSelections[partIndex].bodyPartOptions.Length - 1;
            }
            // Update the Body Part on the player
            UpdateCurrentPart(partIndex);
        }
    }

    // Change to a the next Body Part in the list
    public void NextBodyPart(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            // If there is a Body Part after the current one
            if (bodyPartSelections[partIndex].bodyPartCurrentIndex < bodyPartSelections[partIndex].bodyPartOptions.Length - 1)
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex++;
            }
            // If the current Body Part is the last in the list 
            else
            {
                // Go to the first Body Part in the list
                bodyPartSelections[partIndex].bodyPartCurrentIndex = 0;
            }
            // Update the Body Part on the player
            UpdateCurrentPart(partIndex);
        }
    }

    // Verify if the Index Value given is in range of the Body Part list
    private bool ValidateIndexValue(int partIndex)
    {
        // Testing if the index is between 0 and the nombers of parts that exists 
        if (partIndex > bodyPartSelections.Length || partIndex < 0)
        {
            Debug.Log("Index value does not match any body parts!");
            return false;
        }
        else
        {
            return true;
        }
    }

    // Get the Body Parts currentily associated to the player's body using the index of the part
    public void GetCurrentBodyParts(int partIndex)
    {
        //Get the name of the bodypart
        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartName;
        //Get the animationID of the bodypart
        bodyPartSelections[partIndex].bodyPartCurrentIndex = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartAnimationID;
    }

    // Update the body part associated to the player's body following the actions of the player in the customization menue
    public void UpdateCurrentPart(int partIndex)
    {
        // Update Selection Name Text
        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex].bodyPartName;
        // Update Character Body Part
        characterBody.characterBodyParts[partIndex].bodyPart = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex];
        // Adapting the y position of the bodypart containing the hair
        GameManager.manager.ChangeHairYPosition(false); // false because the player is in the idle position
    }
}



[System.Serializable]
public class BodyPartSelection // Groups all the Body Parts available seperated by type (Hair, Body, Outfit)
{
    public string bodyPartName; // type of Body Parts : Hair, Body or Outfit
    public SO_BodyPart[] bodyPartOptions; // All Body Part associated to this type 
    public Text bodyPartNameTextComponent; // Text componant showing the name of the body part displayed on the player 

    // Make the variable p not show up in the inspector
    // but be serialized.
    [HideInInspector] public int bodyPartCurrentIndex; // index allowing the identification of each Body Part
}
