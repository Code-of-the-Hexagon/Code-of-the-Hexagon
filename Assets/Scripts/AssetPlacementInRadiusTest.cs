using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class AssetPlacementInRadius : MonoBehaviour
{
    // Scripts
    public AssetPlacement AssetPlacer;
    private HexCoordinateSystem _coordinateSystem = new HexCoordinateSystem();
    
    // GameObject stuff
    public Mesh HexagonMesh;
    public GameObject HexAsset;
    public GameObject HexObstacleAsset;
    public SelectedCellText CellTextScript;
    public GameObject CellContainer;

    // Calculation stuff
    private const int Scale = 100; // scale of hexagon
    private Bounds _hexagonBounds;
    private float _x;
    private float _z;

    public float TestUpperHeightLimit = 1;
    public float TestLowerHeightLimit = 0;

    private void Start()
    {
        AssetPlacer = gameObject.AddComponent<AssetPlacement>();
        HexagonMesh = GetComponent<MeshFilter>().mesh;
        _hexagonBounds = HexagonMesh.bounds;
        _x = (_hexagonBounds.max.x) * Scale;     // Coordinate offset
        _z = (_hexagonBounds.max.y) * Scale; // calculation
        SpawnHexagonsInRadius(5);
    }

    public void SpawnHexagonsInRadius(int radius)
    {
        const int centerQ = 0;
        const int centerR = 0;
        const int centerS = 0;

        for (var q = -radius; q <= radius; q++)
        {
            for (var r = Math.Max(-radius, -q - radius); r <= Math.Min(radius, -q + radius); r++)
            { 
                var s = -q - r;
                var distance = _coordinateSystem.GetDistance(
                    new CubeCoordinates(q, r, s),
                    new CubeCoordinates(centerQ, centerR, centerS));

                if (distance <= radius)
                {
                    var position = _coordinateSystem.GetXYCoordinates(new CubeCoordinates(q,r,s));
                    position.y = Random.Range(TestLowerHeightLimit, TestUpperHeightLimit);
                    position.x = position.x * _x; 
                    position.z = position.z * _z; 
                    
                    var placedCell = AssetPlacer.PlaceGameObject(
                        HexAsset,
                        position,
                        new Vector3(),
                        CellContainer.transform);

                }
            }
        }
    }
}