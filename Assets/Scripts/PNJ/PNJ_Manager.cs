using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PNJ_Manager : MonoBehaviour
{
    // Used in MainGame

    // Detects when player is in range and allows the needed action 
    // Allow access to the position of the PNJ before the interaction and to update it after by the PNJ_Position_Manager

    [SerializeField] private SO_PNJ pnj; // Contains the pnj managed by this script

    // Update is called once per frame
    void Update()
    {
        if (pnj.isInRange && Input.GetKeyDown(KeyCode.Return))
        {
            // Send the id of the PNJ to the manager to have the right PNJ in the next scene : Interaction
            GameManager.manager.UpdatePNJId(pnj.pnjId);
            // Load Interaction Scene
            GameManager.manager.ChangeScene("Interaction");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // Verify that the object in range is the player 
        if (collision.CompareTag("Player"))
        {
            pnj.isInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verify that the object going out of range is the player 
        if (collision.CompareTag("Player"))
        {
            pnj.isInRange = false;
        }

    }

    // Get the position of the PNJ before the interaction 
    public Vector3 GetPositionBeforeInteraction()
    {
        return transform.position;
    }

    // Update the position of the PNJ after the interaction 
    public void UpdatePositionAfterInteraction(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
