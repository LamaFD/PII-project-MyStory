using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BodyPartsManager : MonoBehaviour
{
    // Associated to the GameObject player in all the scenes the object is present
    // Updates All Animations to Match Player Selections

    // Animation Naming Must Be: "[Type]_[Index]_[state]_[direction]" except when the state is idle it is : "[Type]_[Index]_[state]"
    // All animations must be in Assets/Resources/Animation and in the appropriate folders

    [SerializeField] private SO_CharacterBody characterBody; // Body of the player

    // String Arrays
    [SerializeField] private string[] bodyPartTypes; // Hair, Body, Outfit
    [SerializeField] private string[] characterStates; // idle, walk
    [SerializeField] private string[] characterDirections; // up, down, left, right

    // Animation
    private Animator animator;
    private AnimationClip animationClip;
    private AnimatorOverrideController animatorOverrideController;
    private AnimationClipOverrides defaultAnimationClips;


    private void Start()
    {
        // Calibrate the position of the player's hair (the start position being idle)
        GameManager.manager.ChangeHairYPosition(false);
        // Update position of the player on the map after the transition from another scene
        if(SceneManager.GetActiveScene().name=="MainGame")
        {
            GameManager.manager.UpdatePlayerPositionBeforeInteraction();
        }
        // Set animator
        animator = GetComponent<Animator>(); // get animator associated to the object
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
        defaultAnimationClips = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        animatorOverrideController.GetOverrides(defaultAnimationClips);
        // Update Body Parts at the begining of the scene
        UpdateBodyParts();
    }

    private void Update()
    {
        // Update BodyParts regularly to show the right Body Part chosen by the player
        UpdateBodyParts();
    }

    public void UpdateBodyParts()
    {
        // Override default animation clips with character body parts
        for (int partIndex = 0; partIndex < bodyPartTypes.Length; partIndex++)
        {
            // Get current body part
            string partType = bodyPartTypes[partIndex];
            // Get current body part ID
            string partID = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartAnimationID.ToString();
            // Go through all the stats possible : idle and walk
            for (int stateIndex = 0; stateIndex < characterStates.Length; stateIndex++)
            {
                // Get the state's name
                string state = characterStates[stateIndex];
                // Get through all the direction possible : up, down, left and right 
                for (int directionIndex = 0; directionIndex < characterDirections.Length; directionIndex++)
                {
                    // Get the name of the direction
                    string direction = characterDirections[directionIndex];
                    // There are 2 possibilites because of the difference in naming in case the position is idle or not 
                    if (state=="idle")
                    {
                        // Get the appropriate animation clip
                        animationClip = Resources.Load<AnimationClip>("Animation/" + partType + "_Animation/" + partType + "_" + partID + "_" + state);
                        // Override default animation
                        defaultAnimationClips[partType + "_" + 1 + "_" + state] = animationClip;
                    }
                    else
                    {
                        // Get the appropriate animation clip
                        animationClip = Resources.Load<AnimationClip>("Animation/" + partType + "_Animation/" + partType + "_" + partID + "_" + state + "_" + direction);
                        // Override default animation
                        defaultAnimationClips[partType + "_" + 1 + "_" + state + "_" + direction] = animationClip;
                    }
                }
            }
        }

        // Apply updated animations
        animatorOverrideController.ApplyOverrides(defaultAnimationClips);
    }

    // Allows to Override the animation
    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }
}