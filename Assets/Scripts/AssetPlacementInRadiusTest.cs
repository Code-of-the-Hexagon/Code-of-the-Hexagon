using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class assetPlacementInRadius : MonoBehaviour
{
    public Mesh hexagonMesh;
    private Bounds hexagonBounds;
    public assetPlacement assetPlacer;
    public Vector3 tileRotation = new Vector3();
    private int scale = 100; // scale of hexagon
    private HexCoordinateSystem hexCoordinateSystem = new HexCoordinateSystem();
    private string floorModelDirectory = "Hexes/";
    private string hexName = "defaultHex";

    public float testUpperHeightLimit = 0.4f;
    public float testLowerHeightLimit = 0f;

    private void Start()
    {
        assetPlacer = gameObject.AddComponent<assetPlacement>(); // prideda assetPlacement scripto instance prie Floor GameObject, ir j refrence'ina su assetPlacer
        assetPlacer.floorChildren = new List<GameObject>();
        tileRotation.x = 90f;
        hexagonMesh = GetComponent<MeshFilter>().mesh;
        hexagonBounds = hexagonMesh.bounds;
        SpawnHexagonsInRadius(20);
    }

    public void SpawnHexagonsInRadius(int radius)
    {
        // Central hexagon coordinates
        int centerQ = 0;
        int centerR = 0;
        int centerS = 0;

        // Loop through coordinates in the specified radius
        for (int q = -radius; q <= radius; q++)
        {
            for (int r = Math.Max(-radius, -q - radius); r <= Math.Min(radius, -q + radius); r++)
            { 
                int s = -q - r;

                // Calculate distance from central hexagon
                int distance = hexCoordinateSystem.GetDistance((q, r, s), (centerQ, centerR, centerS));

                // Check if the coordinate is within the desired radius
                if (distance <= radius)
                {
                    // Spawn a hexagon at the current coordinate
                    (float x, float y) coordinates = hexCoordinateSystem.GetXYCoordinates((q, r, s));

                    assetPlacer.placeGameObject(new Vector3(
                            coordinates.x * scale * hexagonBounds.max.y,
                            UnityEngine.Random.Range(testUpperHeightLimit, testLowerHeightLimit), 
                            coordinates.y * scale * hexagonBounds.max.y),
                            tileRotation,
                            (floorModelDirectory + hexName));
                    Debug.Log($"{hexagonBounds.max.y}");
                }
            }
        }
    }
}