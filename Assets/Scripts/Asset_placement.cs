using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asset_placement : MonoBehaviour
{
    public List<GameObject> floor_children;
    private string floor_model_directory = "";

    #region test_variables
    public float test_upper_height_limit = 0.4f;
    public float test_lower_height_limit = 0f;
    public int test_collumns = 10;
    public int test_rows = 10;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        floor_children = new List<GameObject>();
        place_test(test_collumns, test_rows);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void place_test(int collumns, int rows)
    {
        for (int i = 0; i < collumns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (i % 2 != 0)
                    place_tile(new Vector3(j * 1.7f + 0.85f, UnityEngine.Random.Range(test_upper_height_limit, test_lower_height_limit), i * 1.5f), "singleHex");
                else
                    place_tile(new Vector3(j * 1.7f, UnityEngine.Random.Range(test_upper_height_limit, test_lower_height_limit), i * 1.5f), "singleHex");
            }
        }
    }
    void place_tile(Vector3 position, string hex_block_name)
    {
        hex_block_name = floor_model_directory + hex_block_name;
        GameObject hex = Resources.Load<GameObject>(hex_block_name);
        if (hex != null)
        {
            floor_children.Add(Instantiate<GameObject>(hex, position, Quaternion.LookRotation(Vector3.up, Vector3.forward), transform));
        }
        else
        {
            //add error message
        }
    }
}
