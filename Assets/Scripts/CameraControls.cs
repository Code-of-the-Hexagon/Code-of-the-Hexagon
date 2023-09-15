using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    //Constants
    private readonly float _dragSpeed = GameConstants.CameraConstants.FreeDragSpeed;
    private readonly float _rotateSpeed = GameConstants.CameraConstants.FreeRotateSpeed;
    private readonly float _cameraSpeed = GameConstants.CameraConstants.FreeCameraSpeed;
    private readonly float _zoomScale = GameConstants.CameraConstants.FreeZoomScale;
    private readonly float _cameraMovementSpeedMultiplier = GameConstants.CameraConstants.FreeCameraBoostMultiplier;
    private readonly float _cameraRotationSpeedMultiplier = GameConstants.CameraConstants.FreeRotateBoostMultiplier;

    private readonly Vector3 _cameraUpperLimit = GameConstants.CameraConstants.FreeCameraUpperLimit;
    private readonly Vector3 _cameraLowerLimit = GameConstants.CameraConstants.FreeCameraLowerLimit;

    //Keyboard settings
    private readonly KeyCode _moveForwardsKey = KeyboardSettings.MoveCameraForwards;
    private readonly KeyCode _moveBackwardsKey = KeyboardSettings.MoveCameraBackwards;
    private readonly KeyCode _moveLeftKey = KeyboardSettings.MoveCameraLeft;
    private readonly KeyCode _moveRightKey = KeyboardSettings.MoveCameraRight;
    private readonly KeyCode _rotateLeftKey = KeyboardSettings.RotateCameraLeft;
    private readonly KeyCode _rotateRightKey = KeyboardSettings.RotateCameraRight;
    private readonly KeyCode _speedBoostKey = KeyboardSettings.IncreaseCameraSpeed;

    private Vector3 _dragOrigin;
    private Vector3 _move;
    private Vector3 _rotate;
    private float _moveSpeedMultiplier = 1.0f;
    private float _rotationSpeedMultiplier = 1.0f;

    void LateUpdate()
    {
        _move = new Vector3();
        _rotate = new Vector3();

        if (Input.GetKey(_speedBoostKey))
        {
            _moveSpeedMultiplier = _cameraMovementSpeedMultiplier;
            _rotationSpeedMultiplier = _cameraRotationSpeedMultiplier;
        }
        else
        {
            _moveSpeedMultiplier = 1.0f;
            _rotationSpeedMultiplier = 1.0f;
        }


        // Use middle-click to drag the camera
        if (Input.GetMouseButtonDown(2))
        {
            _dragOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            // Difference between current mouse position and mouse position when button was clicked
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
            _move += new Vector3(pos.x * _dragSpeed * Time.deltaTime, 0, pos.y * _dragSpeed * Time.deltaTime);
        }
        else
        {

            if (Input.GetKey(_moveForwardsKey))
            {
                _move += new Vector3(0, 0, _cameraSpeed * Time.deltaTime * _moveSpeedMultiplier);
            }
            if (Input.GetKey(_moveBackwardsKey))
            {
                _move += new Vector3(0, 0, -_cameraSpeed * Time.deltaTime * _moveSpeedMultiplier);
            }
            if (Input.GetKey(_moveLeftKey))
            {
                _move += new Vector3(-_cameraSpeed * Time.deltaTime * _moveSpeedMultiplier, 0, 0);
            }
            if (Input.GetKey(_moveRightKey))
            {
                _move += new Vector3(_cameraSpeed * Time.deltaTime * _moveSpeedMultiplier, 0, 0);
            }
        }

        if (Input.GetKey(_rotateRightKey))
        {
            _rotate += new Vector3(0, -1, 0);
        }
        else if (Input.GetKey(_rotateLeftKey))
        {
            _rotate += new Vector3(0, 1, 0);
        }

        // Scroll camera up and down
        _move += new Vector3(0, -Input.mouseScrollDelta.y * _zoomScale, 0);

        LimitCameraMovementToBounds(ref _move);

        //Apply all transformations to camera object
        transform.Translate(_move, Space.Self);
        transform.RotateAround(transform.position, _rotate, _rotateSpeed * Time.deltaTime * _rotationSpeedMultiplier);
    }

    //Check if the camera won't be moved to out of bounds
    void LimitCameraMovementToBounds(ref Vector3 move)
    {
        Vector3 currentPos = transform.position;
        LimitMovementAxisToBounds(currentPos.x, ref move.x, _cameraUpperLimit.x, _cameraLowerLimit.x);
        LimitMovementAxisToBounds(currentPos.y, ref move.y, _cameraUpperLimit.y, _cameraLowerLimit.y);
        LimitMovementAxisToBounds(currentPos.z, ref move.z, _cameraUpperLimit.z, _cameraLowerLimit.z);
    }
    void LimitMovementAxisToBounds(float currentPos, ref float move, float upperLimit, float lowerlimit)
    {
        if (currentPos + move > upperLimit) move = upperLimit - currentPos;
        if (currentPos + move < lowerlimit) move = lowerlimit - currentPos;
    }
}
