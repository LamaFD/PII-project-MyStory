using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJSpriteManager : MonoBehaviour
{
    // Used in Interaction
    // Manages the PNJ sprite shown in the interaction : sprite depends on who the player started the interaction with

    public SpriteRenderer spriteRenderer;

    // Difines the sprite at the begining of the scene
    private void Start()
    {
        ChangePNJSprite();
    }

    // Changes the sprite
    void ChangePNJSprite()
    {
        // Get the id of the pnj from the GameManager
        spriteRenderer.sprite = GameManager.manager.pnjs[GameManager.manager.pnjId - 1].pnjSprite; // -1 because the Id in manager starts at 1
    }
}
