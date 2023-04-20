using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    // Allows a PNJ to be draged by the player with the mouse

    private bool dragging = false;
    private Vector3 offset;

    private void Update()
    {
        if (dragging)
        {
            // Change the position of the object if the mouse is pressed on it and dragged
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)+offset;
        }
    }

    private void OnMouseDown()
    {
        // Record the difference between the objects center, and the clicked point on the camera plane
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Start dragging
        dragging = true;
        
    }

    private void OnMouseUp()
    {
        // Stop dragging
        dragging = false;
    }

}
