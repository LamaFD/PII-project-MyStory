using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Used in MainGame
    // Makes the camera follow the player in the scene 

    private Vector3 offset = new Vector3(0f, 0f, -10f); // Defines the offset from our camera to the player, the offset.z=-10f because otherwise the camera wont see anything at 0
    private float smoothTime = 0.25f; // Time it takes the camera to reach the target 
    private Vector3 velocity = Vector3.zero; // Defines the velocity 

    [SerializeField] private Transform target; // The target the camera will follow (the player)


    // Update is called once per frame
    void Update()
    {
        // Set the offset to the position of the target :
        Vector3 targetPosition = target.position + offset; 

        // Gradualy move the vector to the desired position over time giving smooth transition :
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); 
    }
}
