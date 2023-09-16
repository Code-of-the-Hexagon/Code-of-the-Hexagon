using System.Collections.Generic;

public static class UnitVectors
{
    public static readonly List<HexGridCoordinates> HexGridVectors = new()
    {
        new HexGridCoordinates(0, 1, -1),
        new HexGridCoordinates(1, 0, -1),
        new HexGridCoordinates(1, -1, 0),
        new HexGridCoordinates(0, -1, 1),
        new HexGridCoordinates(-1, 0, 1),
        new HexGridCoordinates(-1, 1, 0),
    };
}