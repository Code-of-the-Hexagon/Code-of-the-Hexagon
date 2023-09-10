using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class assetPlacement: MonoBehaviour
{
    public List<GameObject> floorChildren { get; set; } // <- scary public set

    void Start()
    {
        floorChildren = new List<GameObject>();
    }

    public void placeGameObject(Vector3 position, Vector3 rotation, string objectPath)
    {
        
        GameObject objectToPlace = Resources.Load<GameObject>(objectPath);
        if (objectToPlace != null)
        {
            floorChildren.Add(Instantiate<GameObject>(objectToPlace, position, Quaternion.Euler(rotation.x, rotation.y, rotation.z), transform));
        }
        else
        {
            Debug.LogError($"Hex: {objectPath} is null");
        }
    }
}