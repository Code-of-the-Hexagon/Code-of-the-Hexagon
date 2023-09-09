using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    //Constants
    private float dragSpeed = GameConstants.CameraConstants.FreeDragSpeed;
    private float rotateSpeed = GameConstants.CameraConstants.FreeRotateSpeed;
    private float cameraSpeed = GameConstants.CameraConstants.FreeCameraSpeed;
    private float zoomScale = GameConstants.CameraConstants.FreeZoomScale;

    private Vector3 cameraUpperLimit = GameConstants.CameraConstants.FreeCameraUpperLimit;
    private Vector3 cameraLowerLimit = GameConstants.CameraConstants.FreeCameraLowerLimit;

    //Keyboard settings
    private string moveForwardsKey = KeyboardSettings.MoveCameraForwards;
    private string moveBackwardsKey = KeyboardSettings.MoveCameraBackwards;
    private string moveLeftKey = KeyboardSettings.MoveCameraLeft;
    private string moveRightKey = KeyboardSettings.MoveCameraRight;
    private string rotateLeftKey = KeyboardSettings.RotateCameraLeft;
    private string rotateRightKey = KeyboardSettings.RotateCameraRight;

    private Vector3 dragOrigin;
    Vector3 move;
    Vector3 rotate;

    private void Start()
    {
        Application.targetFrameRate = 100;
    }

    void LateUpdate()
    {
        move = new Vector3();
        rotate = new Vector3();

        // Use middle-click to drag the camera
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            // Difference between current mouse position and mouse position when button was clicked
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            move += new Vector3(pos.x * dragSpeed * Time.deltaTime, 0, pos.y * dragSpeed * Time.deltaTime);
        }
        else
        {
            if (Input.GetKey(moveForwardsKey))
            {
                move += new Vector3(0, 0, cameraSpeed * Time.deltaTime);
            }
            if (Input.GetKey(moveBackwardsKey))
            {
                move += new Vector3(0, 0, -cameraSpeed * Time.deltaTime);
            }
            if (Input.GetKey(moveLeftKey))
            {
                move += new Vector3(-cameraSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(moveRightKey))
            {
                move += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
            }
        }

        if (Input.GetKey(rotateRightKey))
        {
            rotate += new Vector3(0, -1, 0);
        }
        else if (Input.GetKey(rotateLeftKey))
        {
            rotate += new Vector3(0, 1, 0);
        }

        // Scroll camera up and down
        move += new Vector3(0, -Input.mouseScrollDelta.y * zoomScale, 0);

        LimitCameraMovementToBounds(ref move);

        //Apply all transformations to camera object
        transform.Translate(move, Space.Self);
        Vector3 relativePivot = new Vector3(0, 0, 0);
        transform.RotateAround(relativePivot, rotate, rotateSpeed * Time.deltaTime);
    }

    //Check if the camera won't be moved to out of bounds
    void LimitCameraMovementToBounds(ref Vector3 move)
    {
        Vector3 currentPos = transform.position;
        LimitMovementAxisToBounds(currentPos.x, ref move.x, cameraUpperLimit.x, cameraLowerLimit.x);
        LimitMovementAxisToBounds(currentPos.y, ref move.y, cameraUpperLimit.y, cameraLowerLimit.y);
        LimitMovementAxisToBounds(currentPos.z, ref move.z, cameraUpperLimit.z, cameraLowerLimit.z);
    }
    void LimitMovementAxisToBounds(float currentPos, ref float move, float upperLimit, float lowerlimit)
    {
        if (currentPos + move > upperLimit) move = upperLimit - currentPos;
        if (currentPos + move < lowerlimit) move = lowerlimit - currentPos;
    }
}
