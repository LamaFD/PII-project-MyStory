using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;


[CreateAssetMenu(fileName = "New PNJ", menuName = "PNJ")] // makes is possible to create objects throught the asset menu
public class SO_PNJ : ScriptableObject
{
    // Scriptible object : contains all informations of a PNJ

    public bool isInRange; // true : player is in range of the pnj, false : player is not in range of the pnj
    public Sprite pnjSprite; // Sprite of the PNJ shown when he/she talks in the scene Interaction
    public string pnjName; 
    public int pnjId; // Id allowing the identification of the pnj in MainGame and show the right pnj during the interaction 

}
