using UnityEngine;
using System.Collections;

public class UIBarControl : MonoBehaviour {

    public float fill;

    GameObject mask;
    ParticleSystem particles;
    RectTransform maskTransform;
    UnityEngine.UI.Image powerOn;

    void Start()
    {
        //get transform for the mask
        mask = gameObject.transform.FindChild("Mask").gameObject;
        maskTransform = mask.GetComponent<RectTransform>();
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
        powerOn = gameObject.GetComponent<RectTransform>().FindChild("PowerOn").gameObject.GetComponent<UnityEngine.UI.Image>();

    }

    void Update()
    {
        if (fill < 100)
        {
            powerOn.enabled = false;
        }
        else
        {
            powerOn.enabled = true;
        }
        //10 is an offset to account for the frame
        maskTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(maskTransform.rect.height, fill + 10, Time.deltaTime * 30));
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
                CleanParticleController.Clean(SpiritBase.SpiritType.Fire);
                FindObjectOfType<SpiritPool>().darkenPool();
                fill = 0;
                UpdateBar(fill);
				AudioManager.PlaySound("FX/Gameplay/Totem/Use/Fire");
				Vibration.Vibrate(1500);
                Debug.Log("Fire");
                break;

            case "EarthPower":
                StoneGrower.Clean();
                CleanParticleController.Clean(SpiritBase.SpiritType.Earth);
                FindObjectOfType<SpiritPool>().darkenPool();
                fill = 0;
                UpdateBar(fill);
				AudioManager.PlaySound("FX/Gameplay/Totem/Use/Earth");
				Vibration.Vibrate(1500);
                Debug.Log("Earth");
                break;

            case "AirPower":
                StoneGrower.Clean();
                CleanParticleController.Clean(SpiritBase.SpiritType.Air);
                FindObjectOfType<SpiritPool>().darkenPool();
                fill = 0;
                UpdateBar(fill);
				AudioManager.PlaySound("FX/Gameplay/Totem/Use/Air");
				Vibration.Vibrate(1500);
                Debug.Log("Air");
                break;

            case "WaterPower":
                StoneGrower.Clean();
                CleanParticleController.Clean(SpiritBase.SpiritType.Water);
                FindObjectOfType<SpiritPool>().darkenPool();
                fill = 0;
                UpdateBar(fill);
				AudioManager.PlaySound("FX/Gameplay/Totem/Use/Water");
				Vibration.Vibrate(1500);
                Debug.Log("Water");
                break;

            default:
                break;
        }
    }
}
