using UnityEngine;

public class AssetPlacement: MonoBehaviour
{
    // Instantiates GameObject to Unity scene
    public GameObject PlaceGameObject(GameObject objectToPlace, Vector3 position, Vector3 rotation, Transform parentTransform = null)
    {
        var spawnedGameObject = Instantiate(objectToPlace, position,
            Quaternion.Euler(rotation.x, rotation.y, rotation.z), transform);
        if (parentTransform != null)
        {
            spawnedGameObject.transform.SetParent(parentTransform);
        }
        
        return spawnedGameObject;
    }
}