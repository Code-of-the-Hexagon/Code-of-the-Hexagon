using System.Collections.Generic;

// world class contains information about everything in the world
public class World
{
    public World()
    {
        HexCells = new();
    }

    public Dictionary<HexGridCoordinates, HexCellData> HexCells;

    public HexCellData GetCell(HexGridCoordinates coordinates)
    {
        return !HexCells.ContainsKey(coordinates) ? 
            null : 
            HexCells[coordinates];
    }

    public void AddNewCell(HexCellData cell)
    {
        HexCells[cell.Coordinates] = cell;
    }
}
