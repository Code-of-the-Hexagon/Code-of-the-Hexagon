using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class AssetPlacementInRadius : MonoBehaviour
{
    public Mesh HexagonMesh;
    private Bounds _hexagonBounds;
    public AssetPlacement AssetPlacer;
    public Vector3 TileRotation = new Vector3();
    private int _scale = 100; // scale of hex
    private HexCoordinateSystem _hexCoordinateSystem = new HexCoordinateSystem();
    private string _floorModelDirectory = "Hexes/";
    private string _hexName = "defaultHex";

    public float TestUpperHeightLimit = 0.4f;
    public float TestLowerHeightLimit = 0f;

    private void Start()
    {
        AssetPlacer = gameObject.AddComponent<AssetPlacement>();
        AssetPlacer.floorChildren = new List<GameObject>();
        TileRotation.x = 90f;
        HexagonMesh = GetComponent<MeshFilter>().mesh;
        _hexagonBounds = HexagonMesh.bounds;
        SpawnHexagonsInRadius(20);
    }

    public void SpawnHexagonsInRadius(int radius)
    {
        var centerQ = 0;
        var centerR = 0;
        var centerS = 0;

        // Loop through coordinates in the specified radius
        for (var q = -radius; q <= radius; q++)
        {
            for (var r = Math.Max(-radius, -q - radius); r <= Math.Min(radius, -q + radius); r++)
            { 
                var s = -q - r;
                var distance = _hexCoordinateSystem.GetDistance((q, r, s), (centerQ, centerR, centerS));

                if (distance <= radius)
                {
                    var coordinates = _hexCoordinateSystem.GetXYCoordinates(new InClassName(new InClassName((q, r, s))));

                    
                }
            }
        }
    }
}