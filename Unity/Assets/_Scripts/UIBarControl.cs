using UnityEngine;
using System.Collections;

public class UIBarControl : MonoBehaviour {

    public float fill;
    public GameObject mask;

    RectTransform maskTransform;

    void Start()
    {
        //get transform for the mask
        maskTransform = mask.GetComponent<RectTransform>();
    }

    //public function for updating the bars
    public void UpdateBar(float input)
    {
        input = Mathf.Clamp(input, 0, 100);
        maskTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, input);
    }

    //function for powers
    public void UsePower()
    {
        switch (gameObject.tag)
        {
            case "FirePower":
                Debug.Log("Fire");
                break;

            case "EarthPower":
                Debug.Log("Earth");
                break;

            case "AirPower":
                Debug.Log("Air");
                break;

            case "WaterPower":
                Debug.Log("Water");
                break;

            default:
                break;
        }
    }
}
