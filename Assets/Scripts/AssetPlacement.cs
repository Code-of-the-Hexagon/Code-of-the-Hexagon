using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asset_placement : MonoBehaviour
{
    public List<GameObject> floorChildren;
    private string floorModelDirectory = "";

    #region test_variables
    public float testUpperHeightLimit = 0.4f;
    public float testLowerHeightLimit = 0f;
    public int testCollumns = 10;
    public int testRows = 10;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
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
                    placeTile(new Vector3(j * 1.732f + 0.866f, UnityEngine.Random.Range(testUpperHeightLimit, testLowerHeightLimit), i * 1.5f), "singleHex");
                else
                    placeTile(new Vector3(j * 1.732f, UnityEngine.Random.Range(testUpperHeightLimit, testLowerHeightLimit), i * 1.5f), "singleHex");
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