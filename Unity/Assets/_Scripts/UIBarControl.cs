using UnityEngine;
using System.Collections;

public class UIBarControl : MonoBehaviourCo {

    public float fill;

    GameObject mask;
    ParticleSystem particles;
    RectTransform maskTransform;

    void Start()
    {
        //get transform for the mask
        mask = gameObject.transform.FindChild("Mask").gameObject;
        maskTransform = mask.GetComponent<RectTransform>();
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    //testing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fill += 10;
            StartCoroutine("UpdateBar", fill);        }
    }

    //public function for updating the bars
    public IEnumerator UpdateBar(float input)
    {
        particles.Play();

        while (maskTransform.rect.height < input)
        {
            input = Mathf.Clamp(input, 0, 100);
            float fillIncrease = Mathf.Lerp(maskTransform.rect.height, input, Time.deltaTime * 30);
            maskTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fillIncrease);
            particles.transform.localPosition = new Vector3(0, input - 50, -5);
            yield return null;
        }
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
