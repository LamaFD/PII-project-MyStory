using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    // Manages UI objects in MainGame
    // Allows to show the indication regarding to starting an interaction with a pnj

    public GameObject indicationUI; // the indication box in the canvas
    public bool showIndication; 
    public SO_PNJ[] pnjs; // list of pnjs present on the map
    
    void Start()
    {
        // the indication is not shown at the begining of the scene
        showIndication = false;
    }

    void Update()
    {
        // go over all pnjs
        foreach(SO_PNJ pnj in pnjs)
        {
            // if the player is in range of one of the pnjs
            if(pnj.isInRange) 
            {
                showIndication = true;
            }
        }
        // show or not the indication box
        indicationUI.SetActive(showIndication);
        // reset showIndication value
        showIndication=false;
    }
}
