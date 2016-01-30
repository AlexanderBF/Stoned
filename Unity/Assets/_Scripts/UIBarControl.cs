using UnityEngine;
using System.Collections;

public class UIBarControl : MonoBehaviour {

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

    //public function for updating the bars
    public IEnumerator UpdateBar(float input)
    {
        fill += input;
        while (maskTransform.rect.height < fill)
        {
            particles.Play();
            particles.gameObject.transform.localPosition = new Vector3(0, fill - 50, -1);
            fill = Mathf.Clamp(fill, 0, 100);
            maskTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(maskTransform.rect.height, fill, Time.deltaTime * 30));
            yield return null;
        }
        particles.Stop();
    }

    //function for powers
    public void UsePower()
    {
        if (fill < 100)
        {
            return;
        }
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
