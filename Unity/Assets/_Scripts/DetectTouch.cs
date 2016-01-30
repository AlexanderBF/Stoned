using UnityEngine;
using System.Collections;

public class DetectTouch : MonoBehaviour {

    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeedInner = 1.0f;
    public float rotationSpeedOuter = 1.0f;

    public Collider innerCircle;
	public Collider outerCircle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //detect touches
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(i).position), out hit))
                {
                    //rotating inner circle
                    if (hit.collider == innerCircle)
                    {
                        // Rotate(innerCircle, Vector3.forward);
                        Rotate(innerCircle, rotationAxis, rotationSpeedInner);
                    }
                    //rotating outer circle
                    else if (hit.collider == outerCircle)
                    {
                        // Rotate(outerCircle, Vector3.back);
                        Rotate(outerCircle, rotationAxis, rotationSpeedOuter);
                    }
                }


            }


        }
        // Alex was here adding mouse input
        else if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                //rotating inner circle
                if (hit.collider == innerCircle)
                {
                    // Rotate(innerCircle, Vector3.forward);
                    Rotate(innerCircle, rotationAxis, rotationSpeedInner);
                }
                //rotating outer circle
                else if (hit.collider == outerCircle)
                {
                    // Rotate(outerCircle, Vector3.back);
                    Rotate(outerCircle, rotationAxis, rotationSpeedOuter);
                }
            }
        }
    }

	void Rotate(Collider circle, Vector3 direction, float speed) {
        // circle.transform.Rotate(direction);
        circle.transform.parent.Rotate(direction, speed * Time.deltaTime);
	}
}
