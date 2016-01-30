using UnityEngine;
using System.Collections;

public class SpiritSpawner : MonoBehaviourCo {

    [Tooltip("The point at which spirits spawn")]
    public Transform spawnPoint;

    [Space(10)]
    public SpiritBase fireSpirit;
    public SpiritBase earthSpirit;
    public SpiritBase waterSpirit;
    public SpiritBase airSpirit;

    public SpiritBase nextToSpawn
    {
        get
        {
            int r = Random.Range(0, 4);
            switch (r)
            {
                case 0: return fireSpirit;
                case 1: return earthSpirit;
                case 2: return waterSpirit;
            }
            return airSpirit;
        }
    }

    [Space(10, order = 0)]
    [Tooltip("The delay between spawning is delay + randomizer * randomValue", order = 1)]
    public float delayBetweenSpawn = 3.0f;
    public float delayBetweenSpawnRandomizer = 0.5f;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(delayBetweenSpawn + delayBetweenSpawnRandomizer * Random.value);

            // Rotate a random amount
            transform.Rotate(Vector3.up, Random.value * 360.0f, Space.Self);

            // Spawn a thing at the location of the spawn point
            SpiritBase toSpawn = nextToSpawn;
            //GameObject spawned = 
                Instantiate(toSpawn.gameObject, spawnPoint.position, spawnPoint.rotation)
                    //as GameObject
                    ;
        }
    }
}