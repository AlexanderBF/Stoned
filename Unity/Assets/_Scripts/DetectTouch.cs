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
			for (int i = 0; i < Input.touchCount; i++) {
				RaycastHit hit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.GetTouch (i).position), out hit)) {
					//rotating inner circle
					if (hit.collider == innerCircle) {
						Rotate (innerCircle, Vector3.forward);
					}
                    //rotating outer circle
                    else if (hit.collider == outerCircle) {
						Rotate (outerCircle, Vector3.back);
					}
				}
			}
		}
		if (Input.touchCount > 0) {
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit)) {
				if(hit.collider == innerCircle) {
					Rotate(innerCircle, Vector3.down);
				} else if (hit.collider == outerCircle) {
					Rotate(outerCircle, Vector3.up);
				}
			}
		}
	}

	void Rotate(Collider circle, Vector3 direction) {
		circle.transform.Rotate(direction);
		Handheld.Vibrate ();
	}
}
