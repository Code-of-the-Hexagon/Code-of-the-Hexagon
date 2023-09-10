using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    //Constants
    private readonly float dragSpeed = GameConstants.CameraConstants.FreeDragSpeed;
    private readonly float rotateSpeed = GameConstants.CameraConstants.FreeRotateSpeed;
    private readonly float cameraSpeed = GameConstants.CameraConstants.FreeCameraSpeed;
    private readonly float zoomScale = GameConstants.CameraConstants.FreeZoomScale;
    private readonly float cameraMovementSpeedMultiplier = GameConstants.CameraConstants.FreeCameraBoostMultiplier;
    private readonly float cameraRotationSpeedMultiplier = GameConstants.CameraConstants.FreeRotateBoostMultiplier;
    public float cameraAngle = 44.713f;//add to constants later (idk how)
    public Vector3 cameraOffset;//also add to constants (0, 4.15f, -5) 

    private Vector3 cameraUpperLimit = GameConstants.CameraConstants.FreeCameraUpperLimit;
    private Vector3 cameraLowerLimit = GameConstants.CameraConstants.FreeCameraLowerLimit;

    //Keyboard settings
    private readonly KeyCode moveForwardsKey = KeyboardSettings.MoveCameraForwards;
    private readonly KeyCode moveBackwardsKey = KeyboardSettings.MoveCameraBackwards;
    private readonly KeyCode moveLeftKey = KeyboardSettings.MoveCameraLeft;
    private readonly KeyCode moveRightKey = KeyboardSettings.MoveCameraRight;
    private readonly KeyCode rotateLeftKey = KeyboardSettings.RotateCameraLeft;
    private readonly KeyCode rotateRightKey = KeyboardSettings.RotateCameraRight;
    private readonly KeyCode speedBoostKey = KeyboardSettings.IncreaseCameraSpeed;

    private Vector3 dragOrigin;
    Vector3 move;
    Vector3 rotate;
    public float rotationSpeedMultiplier = 1.0f;
    public Vector3 rotationPointAbs;
    private void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.position += cameraOffset;
    }

    void LateUpdate()
    {
        move = new Vector3();
        rotate = new Vector3();

        if (Input.GetKey(speedBoostKey))
        {
            rotationSpeedMultiplier = cameraRotationSpeedMultiplier;
        }
        else
        {
            rotationSpeedMultiplier = 1.0f;
        }


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

        //camera speed dependant on zoom (0.8x zoomed in, cameraMovementSpeedMultiplierX zommed out)
        float speed = transform.position.y / ((cameraUpperLimit.y - cameraLowerLimit.y) / cameraMovementSpeedMultiplier);
        if (speed < 0.8) speed = 0.8f;
        move *= speed;

        //calculating the move with real pos
        Vector3 temp1 = transform.position;
        transform.Translate(move, Space.Self);
        Vector3 temp = transform.position;
        Vector3 realMove = temp - temp1, temp2 = rotationPointAbs, temp3;

        //moving and limiting the rotation point
        rotationPointAbs += realMove;
        limitPointMovement(ref rotationPointAbs);

        //moving the camera relatively to rotation point 
        temp3 = rotationPointAbs - temp2;
        transform.position = temp1 + temp3;



        //calc Scroll at an angle
        Vector3 scrollMovement = CalculateScrollMovement(-Input.mouseScrollDelta.y);
        move = scrollMovement;

        //aplying scroll and rotation movement
        transform.Translate(move, Space.Self);
        transform.RotateAround(rotationPointAbs, rotate, rotateSpeed * Time.deltaTime * rotationSpeedMultiplier);
    }
    //Check if point istn't out of bounds
    void limitPointMovement(ref Vector3 cordinates)
    {
        if (cordinates.x > cameraUpperLimit.x) cordinates.x = cameraUpperLimit.x;
        if (cordinates.x < cameraLowerLimit.x) cordinates.x = cameraLowerLimit.x;
        if (cordinates.z > cameraUpperLimit.z) cordinates.z = cameraUpperLimit.z;
        if (cordinates.z < cameraLowerLimit.z) cordinates.z = cameraLowerLimit.z;
    }
    //Check if zoom istn't out of bounds
    void LimitCameraZoomMovement(ref Vector3 ZoomMove)
    {
        Vector3 currentPos = transform.position;
        if (currentPos.y + ZoomMove.y > cameraUpperLimit.y || currentPos.y + ZoomMove.y < cameraLowerLimit.y)
        {
            ZoomMove.y = 0;
            ZoomMove.z = 0;
        }
    }
    Vector3 CalculateScrollMovement(float scrollDelta)
    {
        Vector3 Move = new Vector3();
        Move.y = scrollDelta * zoomScale * (float)Math.Sin((double)cameraAngle);
        Move.z = -scrollDelta * zoomScale * (float)Math.Cos((double)cameraAngle);
        LimitCameraZoomMovement(ref Move);
        return Move;
    }
}
