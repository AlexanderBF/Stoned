using UnityEngine;
using System.Collections;

public class AspectRatioControl : MonoBehaviour {

    public float size3x4 = 8.44f;
    public float size9x16 = 10.71f;

	// Use this for initialization
	void Start () {

        float aspect = Screen.width * 1.0f / Screen.height;

        float a1 = 3.0f / 4;
        float a0 = 9 / 16.0f;

        Camera.main.orthographicSize = Mathf.Lerp(
            size3x4, size9x16,
            (a1 - aspect) / (a1- a0)
        );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
