using UnityEngine;
using System.Collections;

public class DetectTouch : MonoBehaviour {
	public Collider innerCircle;
	public Collider outerCircle;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//detect touches
		if (Input.touchCount > 0) {
			foreach(Touch touch in Input.touches) {
				RaycastHit hit;
				if(Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit)) {
					if(hit.collider == innerCircle) {
						Rotate(innerCircle, Vector3.down);
					} else if (hit.collider == outerCircle) {
						Rotate(outerCircle, Vector3.up);
					}
				}
			}
		}
	}

	void Rotate(Collider circle, Vector3 direction) {
		Handheld.Vibrate();
		circle.transform.Rotate(direction);
	}
}
