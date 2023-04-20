using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_Position_Manager : MonoBehaviour
{
    // Used in MainGame

    // Serves in containing the PNJ objects and be able to :
        // save their positions on the map before the interaction
        // update their positions on the map after the interaction

    public GameObject[] PNJ_Objects; // Contains list of all the PNJ in the map
    public void Start()
    {
        // Set the positions of the PNJs according to the information in the GameManager
        List<Vector3> pnjsPositionsBeforeInteraction = GameManager.manager.pnjs_positions;
        UpdatePNJPosition(pnjsPositionsBeforeInteraction);
    }

    // Allows to get the position of all the PNJs present on the map, used right before the interaction is initiated in GameManager
    public List<Vector3> GetPNJsPositions()
    {
        List<Vector3> pnjsPositionsBeforeInteraction = new List<Vector3>();
        // Get the position of each PNJ
        foreach (GameObject pnj in PNJ_Objects)
        {
            pnjsPositionsBeforeInteraction.Add(pnj.GetComponent<PNJ_Manager>().GetPositionBeforeInteraction());
        }
        return pnjsPositionsBeforeInteraction;
    }

    // Allows to update the position of all the PNJs present on the map according to the information in the GameManager
    public void UpdatePNJPosition(List<Vector3> pnjsPositionsBeforeInteraction)
    {
        for(int i=0;i< pnjsPositionsBeforeInteraction.Count; i++)
        {
            PNJ_Objects[i].GetComponent<PNJ_Manager>().UpdatePositionAfterInteraction(pnjsPositionsBeforeInteraction[i]);
        }

    }
}
