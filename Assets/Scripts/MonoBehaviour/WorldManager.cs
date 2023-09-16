using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public World HexWorld { get; private set; }

    [SerializeField]
    private DrawWorld _worldDrawer;
    void Start()
    {
        HexWorld = InitializeWorld();
        _worldDrawer.Draw(HexWorld);
    }

    private static World InitializeWorld()
    {
        // Create the world out of different shapes
        var options = new WorldGenerationOptions();
        options.HexGridShape = options.HexGridShape.AddShape(Shape.Circle, 15, ArrayJoinTypes.Union);
        options.HexGridShape = options.HexGridShape.AddShape(Shape.Star, 5, ArrayJoinTypes.Intersection, null, true);
        options.HexGridShape = options.HexGridShape.AddShape(Shape.Circle, 7, ArrayJoinTypes.Union);
        options.HexGridShape = options.HexGridShape.AddShape(Shape.Star, 2, ArrayJoinTypes.Intersection, null, true);
        return WorldGenerator.GenerateWorld(options);
    }
}
