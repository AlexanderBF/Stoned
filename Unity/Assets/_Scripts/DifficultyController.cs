using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DifficultyController : MonoBehaviourCo {

    public SpiritSpawner spawner;
    public List<SpiritBase> spiritPrefabs;
    public List<DetectTouchSlide> rings;

    [Tooltip("How fast do the spirits travel")]
    public AnimationCurve travelSpeed = AnimationCurve.Linear(0, 1, 60 * 10, 3);

    [Space(10,order =0)]
    [Tooltip("How fast are the spirits spawned", order = 1)]
    public AnimationCurve spawnDelay = AnimationCurve.Linear(0, 3, 60 * 10, 1);
    [Tooltip("How fast are the spirits spawned random modifier")]
    public AnimationCurve spawnRandomDelay = AnimationCurve.Linear(0, 0.5f, 60 * 10, 0.25f);

    [Tooltip("How many spirits of each type needs to be captured")]
    public AnimationCurve spiritsNeededToFill = AnimationCurve.Linear(0, 5, 60*10, 10);

    [Tooltip("Multiplier for rotation speed as the game progresses")]
    public AnimationCurve controlRotationSpeed = AnimationCurve.Linear(0, 1, 1, 1);

    [Tooltip("How often do we update the difficulty settings")]
    public float intervalForProgressionChanges = 5;

    float startTime;
    float startRotationSpeed;
    void Start()
    {
        startRotationSpeed = rings.Count > 0 ? rings[0].maxRotationSpeed : 240.0f;
        startTime = Time.time;
        StartCoroutine(Control());
    }

    IEnumerator Control()
    {
        while(true)
        {
            float dTime = Time.time - startTime;

            spawner.delayBetweenSpawn = spawnDelay.Evaluate(dTime);
            spawner.delayBetweenSpawnRandomizer = spawnRandomDelay.Evaluate(dTime);

            foreach(SpiritBase sb in spiritPrefabs)
            {
                sb.speed = travelSpeed.Evaluate(dTime);
            }

            foreach(DetectTouchSlide dts in rings)
            {
                dts.maxRotationSpeed = controlRotationSpeed.Evaluate(dTime) * startRotationSpeed;
            }

            yield return new WaitForSeconds(intervalForProgressionChanges);
        }
    }
}
