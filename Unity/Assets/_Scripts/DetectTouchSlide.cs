using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DetectTouchSlide : MonoBehaviourCo {

    private static List<int> usedFinger = new List<int>();

    public LayerMask raycastLayer;
    public Collider myCollider;
	private bool stoneSoundOn = false;

    abstract private class Touch
    {
        protected abstract TouchPhase Phase();
        protected abstract Vector3 Position();
        protected virtual int FingerId() { return -1; }

        public TouchPhase phase
        {
            get { return Phase(); }
        }

        public Vector3 position
        {
            get { return Position(); }
        }

        public int fingerId
        {
            get { return FingerId(); }
        }
    }
    private class TouchMouse : Touch
    {
        override protected TouchPhase Phase()
        {
            if (Input.GetMouseButtonDown(0))
                return TouchPhase.Began;
            else if (Input.GetMouseButton(0))
                return TouchPhase.Moved;
            return TouchPhase.Ended;
        }

        protected override Vector3 Position()
        {
            return Input.mousePosition;
        }
    }
    private class TouchTouch : Touch
    {
        private UnityEngine.Touch touch;
        public TouchTouch(UnityEngine.Touch touch)
        {
            this.touch = touch;
        }

        protected override TouchPhase Phase()
        {
            return touch.phase;
        }
        protected override Vector3 Position()
        {
            return touch.position;
        }
        protected override int FingerId()
        {
            return touch.fingerId;
        }
    }

    void OnEnable()
    {
        Do(() =>
        {
            for(int i=0; i<Input.touchCount; i++)
            {
                Touch t = new TouchTouch(Input.GetTouch(i));
                if(t.phase == TouchPhase.Began)
                {
                    StartCoroutine(Slide(t));
                }
            }

            if(Input.touchCount == 0 && Input.GetMouseButton(0))
            {
                StartCoroutine(Slide(new TouchMouse()));
            }
            return enabled;
        });
    }

    Vector3 InputPosition(int fingerId)
    {
        if (fingerId < 0) return Input.mousePosition;
        for(int i=0; i<Input.touchCount; i++)
        {
            Touch t = new TouchTouch(Input.GetTouch(i));
            if (t.fingerId == fingerId)
                return t.position;
        }
        return Vector3.zero;
    }

    bool GetFinger(int fingerId, out Touch touch)
    {
        if(fingerId < 0)
        {
            touch = new TouchMouse();
            return true;
        }

        for(int i=0; i<Input.touchCount; i++)
        {
            Touch t = new TouchTouch(Input.GetTouch(i));
            if (t.fingerId == fingerId)
            {
                touch = t;
                return true;
            }
        }
        touch = new TouchMouse();
        return false;
    }

    bool alreadySliding = false;
    float rotateAngle = 0.0f;

    IEnumerator Slide(Touch touch)
    {
        if (alreadySliding) yield break;

        Vector3 lastPosition = touch.position;

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit, Mathf.Infinity, raycastLayer))
        // if(myCollider.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit, Mathf.Infinity))
        {
            if(hit.collider != myCollider)
            {
                // nay
                yield break;
            }
        }

        int fingerId = touch.fingerId;

        if (DetectTouchSlide.usedFinger.Contains(fingerId))
            yield break;

        alreadySliding = true;

        DetectTouchSlide.usedFinger.Add(fingerId);

        while (true)
        {
            // Find the touch
            if (GetFinger(fingerId, out touch))
            {
                if(touch.phase == TouchPhase.Moved)
                {
                    Vector3 position = touch.position;

                    Vector3 screenPointCenter = Camera.main.WorldToScreenPoint(transform.position);

                    Vector3 legA = position - screenPointCenter;
                    Vector3 legB = lastPosition - screenPointCenter;
                    float angle = Vector3.Angle(legA, legB);

                    lastPosition = position;

                    Debug.Log(angle);

                    float sign = Mathf.Sign(
                        -Vector3.Cross(legA, legB).z
                    );

                    rotateAngle = angle * sign;
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    break;
                }
            }
            else break;

            yield return null;
        }

        DetectTouchSlide.usedFinger.Remove(fingerId);
        alreadySliding = false;
        
    }

	void Start() {
		if (!stoneSoundOn) {
			Debug.Log("Start the stone sound...");
			AudioManager.PlaySound("FX/Gameplay/Stones-Move");
			stoneSoundOn = true;
		}
	}

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, rotateAngle);
        rotateAngle *= 0.5f;
        Fabric.EventManager.Instance.SetParameter("FX/Gameplay/Stones-Move", "Speed", Mathf.Abs(rotateAngle), null);
/*		if (rotateAngle > 0) {
	        Debug.Log("Speed is: " + rotateAngle);
	    }
*/
    }
}
