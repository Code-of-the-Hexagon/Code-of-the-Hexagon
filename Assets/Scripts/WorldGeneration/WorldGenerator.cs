using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldGenerator
{
    // generates world from WorldGenerationOptions
    public static World GenerateWorld(WorldGenerationOptions options) =>
        new World()
            .AddCellsAndGenerateShape(options.ShapeOptions)
            .AddObstacles(options)
            .AddHeights(options);

    // creates world shape defined in WorldGenerationOptions.WorldShape
    // performs a bfs algorithm in all area defined by world limits and
    // includes all cells contained in HexGrid shape to the world
    public static World AddCellsAndGenerateShape(
        this World world, 
        WorldGenerationOptions.WorldShape options)
    {
        var startingCell = new HexCellData(options.StartingPoint);

        var queue = new Queue<HexCellData>();
        queue.Enqueue(startingCell);
        var usedPositions = new HashSet<string>
        {
            startingCell.Coordinates.ToString()
        };
        while (queue.Count > 0)
        {
            var currentCell = queue.Dequeue();
            if (options.HexGridShape(currentCell.Coordinates))
            {
                world.AddNewCell(currentCell);
            }

            foreach (var vector in UnitVectors.HexGridVectors)
            {
                var newCell = new HexCellData(currentCell.Coordinates + vector);
                if (usedPositions.Contains(newCell.Coordinates.ToString()) ||
                    !options.WorldLimits(newCell.Coordinates))
                {
                    continue;
                }
                usedPositions.Add(newCell.Coordinates.ToString());
                queue.Enqueue(newCell);


            }
        }

        return world;
    }

    // Adds obstacles to the world
    public static World AddObstacles(
        this World world,
        WorldGenerationOptions options)
    {
        foreach (var cell in world.HexCells.Values)
        {
            cell.Type = Rng.IsTrue(options.ObstacleSpawnChance) ? HexType.Obstacle : HexType.Grass;
        }

        return world;
    }

    // Changes cell heights
    public static World AddHeights(
        this World world,
        WorldGenerationOptions options)
    {
        foreach (var cell in world.HexCells.Values)
        {
            cell.Height = Rng.RandomFloat(0f, 1f);
        }

        return world;
    }
}
