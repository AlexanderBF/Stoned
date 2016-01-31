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
    [Tooltip("Spirits required to activate the powers")]
    public AnimationCurve requiredSpirits = AnimationCurve.Linear(0, 5, 60 * 10, 5);

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
            ClickHomeBase.spiritsPerPower = requiredSpirits.Evaluate(dTime);

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

    void FixedUpdate()
    {
    	// this value is being fed to the music
    	// to play an intensity layer as the game
    	// gets more tense.

    	// It should be replaced with a value between 0 to 1.0f!
    	float theIntensity = 0.5f;
    
/*		Fabric.EventManager.Instance.SetParameter("MX/Main_Loop", 
		                                          "Intensity", 
		                                          theIntensity, 
		                                          null);
*/
    }

}
