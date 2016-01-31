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
            particles.gameObject.transform.localPosition = new Vector3(0, fill - 40, -1);
            fill = Mathf.Clamp(fill, 0, 100);
            //10 is an offset to account for the frame
            maskTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(maskTransform.rect.height, fill + 10, Time.deltaTime * 30));
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
                StoneGrower.Clean();
                fill = 0;
                UpdateBar(fill);
                Debug.Log("Fire");
                break;

            case "EarthPower":
                StoneGrower.Clean();
                fill = 0;
                UpdateBar(fill);
                Debug.Log("Earth");
                break;

            case "AirPower":
                StoneGrower.Clean();
                fill = 0;
                UpdateBar(fill);
                Debug.Log("Air");
                break;

            case "WaterPower":
                StoneGrower.Clean();
                fill = 0;
                UpdateBar(fill);
                Debug.Log("Water");
                break;

            default:
                break;
        }
    }
}
