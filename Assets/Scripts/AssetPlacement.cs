using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asset_placement : MonoBehaviour
{
    public TextMesh textMesh;
    public Mesh hexagonMesh;
    public List<GameObject> floorChildren;
    private string floorModelDirectory = "Hexes/";

    #region test_variables
    public float testUpperHeightLimit = 0.4f;
    public float testLowerHeightLimit = 0f;
    public int testCollumns = 10;
    public int testRows = 10;
    #endregion

    // Variables for distance calculation
    private Bounds hexagonBounds;
    private float x;
    private float y;
    private int scale = 100; // scale of hexagon
    // Start is called before the first frame update
    void Start()
    {
        hexagonMesh = GetComponent<MeshFilter>().mesh;
        hexagonBounds = hexagonMesh.bounds;
        x = (hexagonBounds.max.x * 2) * scale;     // Coordinate offset
        y = (hexagonBounds.max.y * 3 / 2) * scale; // calculations

        floorChildren = new List<GameObject>();
        placeTest(testCollumns, testRows);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void placeTest(int collumns, int rows)
    {
        for (int i = 0; i < collumns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (i % 2 != 0)
                    placeTile(new Vector3(j * x + x / 2, UnityEngine.Random.Range(testUpperHeightLimit, testLowerHeightLimit), i * y), "defaultHex");
                else
                    placeTile(new Vector3(j * x, UnityEngine.Random.Range(testUpperHeightLimit, testLowerHeightLimit), i * y), "defaultHex");
            }
        }
    }
    void placeTile(Vector3 position, string hexBlockName)
    {
        hexBlockName = floorModelDirectory + hexBlockName;
        GameObject hex = Resources.Load<GameObject>(hexBlockName);
        if (hex != null)
        {
            floorChildren.Add(Instantiate<GameObject>(hex, position, Quaternion.LookRotation(Vector3.up, Vector3.forward), transform));
        }
        else
        {
            //add error message
        }
    }
}
