using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetPlacementTesting : MonoBehaviour
{
    public AssetPlacement AssetPlacer;
    public float TestUpperHeightLimit;
    public float TestLowerHeightLimit;
    public int TestCollumns = 10;
    public int TestRows = 10;
    public Mesh HexagonMesh;
    public GameObject HexAsset;
    public SelectedCellText CellTextScript;

    // Variables for distance calculation
    private Bounds _hexagonBounds;
    private float _x;
    private float _y;
    private const int Scale = 100; // scale of hexagon

    private void Start()
    {
        AssetPlacer = gameObject.AddComponent<AssetPlacement>(); // prideda assetComponent scripto instance prie Floor GameObject, ir j refrence'ina su assetPlacer
        AssetPlacer.FloorChildren = new List<GameObject>();
        HexagonMesh = GetComponent<MeshFilter>().mesh;
        _hexagonBounds = HexagonMesh.bounds;
        _x = (_hexagonBounds.max.x * 2) * Scale;     // Coordinate offset
        _y = (_hexagonBounds.max.y * 3 / 2) * Scale; // calculation

        PlaceTest(TestCollumns, TestRows);
    }
    private void PlaceTest(int collumns, int rows)
    {
        for (int i = 0; i < collumns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var height = UnityEngine.Random.Range(TestLowerHeightLimit, TestUpperHeightLimit);
                var placedCell = AssetPlacer.PlaceGameObject(HexAsset,
                    i % 2 != 0
                        ? new Vector3(j * _x + _x / 2, height, i * _y)
                        : new Vector3(j * _x, height, i * _y),
                    new Vector3());
                placedCell.GetComponent<GameObjectSelect>().SetLabel($"X = {i} Y = {j}");
                placedCell.GetComponent<GameObjectSelect>().LabelScript = CellTextScript;
            }
        }
    }
}
