using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// makes is possible to create objects throught the asset menu
[CreateAssetMenu(fileName = "New Body Part", menuName = "Body Part")] 
public class SO_BodyPart : ScriptableObject
{
    // Scriptable object representing a Body Part that may be contained in the SO_CharacterBody

    // Body Part Details
    public string bodyPartName;
    public int bodyPartAnimationID; // Allows to find the animation associated to this Body Part in BodyPartsManager

    // List Containing All Body Part Animations
    public List<AnimationClip> allBodyPartAnimations = new List<AnimationClip>();
}
