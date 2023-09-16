// HexCellData contains information about single hex cell
public class HexCellData
{
    public HexType Type { get; set; }
    public HexGridCoordinates Coordinates { get; private set; }
    public float Height { get; set; }

    public HexCellData(HexGridCoordinates coordinates, HexType type = HexType.Unknown, float height = 0)
    {
        Type = type;
        Coordinates = coordinates;
        Height = height;
    }
}
