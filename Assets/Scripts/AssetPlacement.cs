using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetPlacement: MonoBehaviour
{
    public List<GameObject> FloorChildren { get; set; } // <- scary public set

    void Start()
    {
        FloorChildren = new List<GameObject>();
    }

    public GameObject PlaceGameObject(GameObject objectToPlace, Vector3 position, Vector3 rotation)
    {
        var spawnedGameObject = Instantiate(objectToPlace, position,
            Quaternion.Euler(rotation.x, rotation.y, rotation.z), transform);
        FloorChildren.Add(spawnedGameObject);
        return spawnedGameObject;
    }
}