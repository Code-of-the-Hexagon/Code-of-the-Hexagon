using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWorld : MonoBehaviour
{
    [SerializeField] private AssetPlacement _assetPlacer;

    [SerializeField] private MeshFilter _hexagonMeshFilter;

    [SerializeField] private GameObject _hexAsset;

    [SerializeField] private GameObject _hexObstacleAsset;

    [SerializeField] private SelectedCellText _cellTextScript;

    [SerializeField] private GameObject _cellContainer;


    // Variables for distance calculation
    private Mesh _hexagonMesh;
    private Bounds _hexagonBounds;
    private float _x;
    private float _y;

    // scale of hexagon
    private const int Scale = 100; 

    public void Draw(World world)
    {
        _hexagonMesh = _hexagonMeshFilter.mesh;
        _hexagonBounds = _hexagonMesh.bounds;
        _x = (_hexagonBounds.max.x * 2) * Scale;     
        _y = (_hexagonBounds.max.y * 3 / 2) * Scale;
        foreach (var hexCell in world.HexCells.Values)
        {
            var placedCell = _assetPlacer.PlaceGameObject(
                _hexAsset,
                hexCell.Coordinates.Y % 2 != 0
                    ? new Vector3(hexCell.Coordinates.X * _x + _x / 2, hexCell.Height, hexCell.Coordinates.Y * _y)
                    : new Vector3(hexCell.Coordinates.X * _x, hexCell.Height, hexCell.Coordinates.Y * _y),
                new Vector3(),
                _cellContainer.transform);
            placedCell.GetComponent<GameObjectSelect>().Label = hexCell.Coordinates.ToString();//$"X = {hexCell.Coordinates.X} Y = {hexCell.Coordinates.Y}";
            placedCell.GetComponent<GameObjectSelect>().LabelScript = _cellTextScript;
        }
    }
}
