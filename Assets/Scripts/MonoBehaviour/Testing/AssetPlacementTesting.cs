using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AssetPlacementTesting : MonoBehaviour
{
    public AssetPlacement AssetPlacer;
    public float TestUpperHeightLimit;
    public float TestLowerHeightLimit;
    public int TestColumns = 10;
    public int TestRows = 10;

    [SerializeField]
    private int _obstacleProbability;

    public Mesh HexagonMesh;
    public GameObject HexAsset;
    public GameObject HexObstacleAsset;
    public SelectedCellText CellTextScript;
    public GameObject CellContainer;

    // Variables for distance calculation
    private Bounds _hexagonBounds;
    private float _x;
    private float _y;
    private const int Scale = 100; // scale of hexagon

    private void Start()
    {
        AssetPlacer = gameObject.AddComponent<AssetPlacement>(); // adds and references AssetPlacement script
        HexagonMesh = GetComponent<MeshFilter>().mesh;
        _hexagonBounds = HexagonMesh.bounds;
        _x = (_hexagonBounds.max.x * 2) * Scale;     // Coordinate offset
        _y = (_hexagonBounds.max.y * 3 / 2) * Scale; // calculation

        PlaceTest(TestColumns, TestRows);
    }
    private void PlaceTest(int columns, int rows)
    {
        for (var i = 0; i < columns; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                var height = Random.Range(TestLowerHeightLimit, TestUpperHeightLimit);
                var placedCell = AssetPlacer.PlaceGameObject(
                    HexAsset,
                    i % 2 != 0
                        ? new Vector3(j * _x + _x / 2, height, i * _y)
                        : new Vector3(j * _x, height, i * _y),
                    new Vector3(),
                    CellContainer.transform);
                

                if (Random.Range(0, 100) < _obstacleProbability)
                {
                    placedCell.GetComponent<GameObjectSelect>().enabled = false;
                    AssetPlacer.PlaceGameObject(
                        HexObstacleAsset, 
                        placedCell.transform.GetChild(4).position,
                        new Vector3(), 
                        placedCell.transform);
                }
                else
                {
                    placedCell.GetComponent<GameObjectSelect>().Label = $"X = {i} Y = {j}";
                    placedCell.GetComponent<GameObjectSelect>().LabelScript = CellTextScript;
                }
            }
        }
    }
}
