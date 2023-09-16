using System.Collections.Generic;

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
