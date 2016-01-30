using UnityEngine;
using System.Collections;

public class UIControlTest : MonoBehaviour {

    public GameObject bar1;
    public GameObject bar2;

    float fill1 = 0f;
    float fill2 = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            fill1 += 1f;
            bar1.GetComponent<UIBarControl>().UpdateBar(fill1);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            fill2 += 1f;
            bar2.GetComponent<UIBarControl>().UpdateBar(fill2);
        }
    }
}
