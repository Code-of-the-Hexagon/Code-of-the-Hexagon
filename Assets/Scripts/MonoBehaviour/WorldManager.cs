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
        var options = new WorldGenerationOptions
        {
            ObstacleSpawnChance = 0.2f
        };

        options.AddShapeToHexGrid(Shape.Star, 6, ArrayJoinTypes.Union, new HexGridCoordinates(0, 0));
        options.AddShapeToHexGrid(Shape.Star, 3, ArrayJoinTypes.Intersection, new HexGridCoordinates(0, 0), null, true);

        options.AddShapeToHexGrid(Shape.Circle, 10, ArrayJoinTypes.Union, new HexGridCoordinates(15, 15));
        options.AddShapeToHexGrid(Shape.Circle, 4, ArrayJoinTypes.Intersection, new HexGridCoordinates(15, 15), null, true);

        options.AddShapeToHexGrid(Shape.Triangle, 10, ArrayJoinTypes.Union, new HexGridCoordinates(15, -15));
        options.AddShapeToHexGrid(Shape.Triangle, 4, ArrayJoinTypes.Intersection, new HexGridCoordinates(15, -15), null, true);

        options.AddShapeToHexGrid(Shape.Hexagon, 8, ArrayJoinTypes.Union, new HexGridCoordinates(-15, 15));
        options.AddShapeToHexGrid(Shape.Hexagon, 3, ArrayJoinTypes.Intersection, new HexGridCoordinates(-15, 15), null, true);

        options.AddShapeToHexGrid(Shape.Rectangle, 10, ArrayJoinTypes.Union, new HexGridCoordinates(-20, -18));

        return WorldGenerator.GenerateWorld(options);
    }
}
