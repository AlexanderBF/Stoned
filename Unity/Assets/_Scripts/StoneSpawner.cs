using UnityEngine;
using System.Collections;

public class StoneSpawner : MonoBehaviour {

    public GameObject firstStone;
    public int totalCount;

    void Awake()
    {
        // How many degrees of separation
        float degrees = 360.0f / totalCount;

        Quaternion originalRotation = transform.rotation;
        Vector3 position = firstStone.transform.position;
        Quaternion rotation = firstStone.transform.rotation;

        for(int i=1; i< totalCount; i++)
        {
            transform.Rotate(Vector3.forward, degrees);

            GameObject spawn = Instantiate(firstStone, position, rotation) as GameObject;
            spawn.transform.parent = firstStone.transform.parent;
        }

        transform.rotation = originalRotation;
    }
}
