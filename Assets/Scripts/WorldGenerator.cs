using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldGenerator
{
    public static World GenerateWorld(WorldGenerationOptions options)
    {
        var world = new World();
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
}
