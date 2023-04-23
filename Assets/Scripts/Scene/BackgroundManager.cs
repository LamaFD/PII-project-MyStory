using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    // Used in Interaction
    // Manages the background in the interaction : shows the emotions chosen by the player

    public Sprite[] backgroundImages; // contains the different sprites of the background in this order : dialogue , neutral, sad, angry, happy
    public SpriteRenderer backgroundSpriteRenderer;

    void Start()
    {
        // initiate the background to neutral
        backgroundSpriteRenderer.sprite = backgroundImages[0];
    }

    // Changes background of the interaction depending on the emotion chosen
    // Used in GameManager
    public void ChangeBackground(int emotionID)
    {
        backgroundSpriteRenderer.sprite = backgroundImages[emotionID];
    }
}
