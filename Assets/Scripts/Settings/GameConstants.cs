using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    public static class CameraConstants
    {
        public const float FreeDragSpeed = 15f;
        public const float FreeCameraSpeed = 10f;
        public const float FreeCameraBoostMultiplier = 3f;
        public const float FreeRotateSpeed = 120f;
        public const float FreeRotateBoostMultiplier = 2f;
        public const float FreeZoomScale = 1f;

        public readonly static Vector3 FreeCameraUpperLimit = 
            new Vector3(100f, 15f, 100f);
        public readonly static Vector3 FreeCameraLowerLimit =
            new Vector3(-100f, 0f, -100f);
    }
}
