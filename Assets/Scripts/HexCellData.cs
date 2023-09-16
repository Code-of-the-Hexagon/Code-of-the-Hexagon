public class HexCellData
{
    public HexType Type { get; private set; }
    public HexGridCoordinates Coordinates { get; private set; }
    public float Height { get; private set; }

    public HexCellData(HexGridCoordinates coordinates, HexType type = HexType.Grass, float height = 0)
    {
        Type = type;
        Coordinates = coordinates;
        Height = height;
    }
}
