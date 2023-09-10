using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assetPlacementTesting : MonoBehaviour
{
    public assetPlacement assetPlacer;
    public float testUpperHeightLimit = 0.4f;
    public float testLowerHeightLimit = 0f;
    public int testCollumns = 10;
    public int testRows = 10;
    public Vector3 tileRotation = new Vector3();
    public Mesh hexagonMesh;

    private string floorModelDirectory = "Hexes/";
    private string hexName = "defaultHex";

    // Variables for distance calculation
    private Bounds hexagonBounds;
    private float x;
    private float y;
    private int scale = 100; // scale of hexagon

    private void Start()
    {
        assetPlacer = gameObject.AddComponent<assetPlacement>(); // prideda assetComponent scripto instance prie Floor GameObject, ir j refrence'ina su assetPlacer
        assetPlacer.floorChildren = new List<GameObject>();
        hexagonMesh = GetComponent<MeshFilter>().mesh;
        hexagonBounds = hexagonMesh.bounds;
        x = (hexagonBounds.max.x * 2) * scale;     // Coordinate offset
        y = (hexagonBounds.max.y * 3 / 2) * scale; // calculations

        tileRotation.x = 90f;

        placeTest(testCollumns, testRows);
    }
    void placeTest(int collumns, int rows)
    {
        for (int i = 0; i < collumns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (i % 2 != 0)
                {
                    assetPlacer.placeGameObject(new Vector3(j * x + x / 2, UnityEngine.Random.Range(testUpperHeightLimit, testLowerHeightLimit), i * y), tileRotation, (floorModelDirectory + hexName));
                }
                else
                    assetPlacer.placeGameObject(new Vector3(j * x, UnityEngine.Random.Range(testUpperHeightLimit, testLowerHeightLimit), i * y), tileRotation, (floorModelDirectory + hexName));
            }
        }
    }
}
