using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public delegate bool CoordinatesArray(HexGridCoordinates coordinates);

public class WorldGenerationOptions
{
    
    public HexGridCoordinates StartingPoint { get; set; } = new(0, 0);

    public CoordinatesArray HexGridShape { get; set; } = _ => false;
    public CoordinatesArray WorldLimits { get; set; } = _ => false;

    public WorldGenerationOptions()
    {
        WorldLimits = WorldLimits.AddShape(Shape.Circle, GameConstants.WorldGeneration.MaxWorldRadius, ArrayJoinTypes.Union);
    }

}
