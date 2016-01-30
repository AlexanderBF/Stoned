using UnityEngine;
using System.Collections;

public class UIBarControl : MonoBehaviour {

    public float fill;
    public GameObject mask;

    RectTransform maskTransform;

    void Start()
    {
        maskTransform = mask.GetComponent<RectTransform>();
    }


    public void UpdateBar(float input)
    {
        input = Mathf.Clamp(input, 0, 100);
        maskTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, input);
    }
}
