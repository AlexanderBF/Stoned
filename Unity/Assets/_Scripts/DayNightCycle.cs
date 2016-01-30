using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

    public Light sun;
    public float dayLengthInSeconds = 20;
    public AnimationCurve lightIntensity;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, Time.deltaTime * 360.0f / dayLengthInSeconds);
        sun.intensity = lightIntensity.Evaluate(Time.timeSinceLevelLoad / dayLengthInSeconds);
	}
}
