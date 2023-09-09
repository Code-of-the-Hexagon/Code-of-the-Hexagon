using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private float dragSpeed = GameConstants.CameraConstants.FreeDragSpeed;
    private float rotateSpeed = GameConstants.CameraConstants.FreeRotateSpeed;
    private Vector3 dragOrigin;

    void LateUpdate()
    {
        // Use middle-click to drag the camera
        if (Input.GetMouseButtonDown(2)) 
        {
            dragOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            // Difference between current mouse position and mouse position when button was clicked
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            // Apply drag speed and reverse the movement
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
            transform.Translate(move, Space.World);
            //dragOrigin = Input.mousePosition;
        }
    }
}
