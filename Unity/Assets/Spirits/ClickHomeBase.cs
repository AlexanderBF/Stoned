using UnityEngine;
using System.Collections;

public class ClickHomeBase : MonoBehaviour {

    public Collider homeCollider;

    void Update()
    {
        for(int i=0; i<Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);
            if(t.phase == TouchPhase.Began)
            {
                Test(t.position); 
            }
        }

        if (Input.touchCount == 0 && Input.GetMouseButtonDown(0))
            Test(Input.mousePosition);
    }

    void Test(Vector3 screenPosition)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if(homeCollider.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Collect all the fun particles
            StartCoroutine(Collect());
        }
    }

    IEnumerator Collect()
    {
        // collect them al
        SpiritBase[] all = FindObjectsOfType<SpiritBase>();

        for(int i=0; i<all.Length; i++)
        {
            // check if it is inside inner circle
            if(all[i].gameObject.layer == LayerManager.SpiritClearedInner)
            {
                // Take me to your leader
            }
            yield return null;
        }


        yield return null;
    }

}
