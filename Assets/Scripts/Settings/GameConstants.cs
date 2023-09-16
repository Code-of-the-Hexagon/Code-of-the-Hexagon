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

        public static readonly Vector3 FreeCameraUpperLimit = 
            new (100f, 15f, 100f);
        public static readonly Vector3 FreeCameraLowerLimit =
            new (-100f, 0f, -100f);
    }

    public static class WorldGeneration
    {
        public const int MaxWorldRadius = 70;
    }
}
