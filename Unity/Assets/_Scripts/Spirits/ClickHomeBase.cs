using UnityEngine;
using System.Collections;

public class ClickHomeBase : MonoBehaviour {

    public float particleSpeedMultiplier = 2;
    public static float spiritsPerPower = 5;

    public Collider homeCollider;
    GameObject fireBar;
    GameObject earthBar;
    GameObject airBar;
    GameObject waterBar;

    public GameObject fireParticles;
    public GameObject earthParticles;
    public GameObject airParticles;
    public GameObject waterParticles;

    void Start()
    {
        fireBar = GameObject.FindGameObjectWithTag("FirePower");
        earthBar = GameObject.FindGameObjectWithTag("EarthPower");
        airBar = GameObject.FindGameObjectWithTag("AirPower");
        waterBar = GameObject.FindGameObjectWithTag("WaterPower");
    }

    void Update()
    {
        for (int i=0; i<Input.touchCount; i++)
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
        //variable for how quickly the bars fill up
        float increment = 100 / spiritsPerPower;
		Debug.Log(increment);

        // collect them al
        SpiritBase[] all = FindObjectsOfType<SpiritBase>();

            for (int i = 0; i < all.Length; i++)
            {
                // check if it is inside inner circle
                if (all[i].gameObject.tag == "Winner")
                {
                    //tag for death
                    all[i].gameObject.tag = "Counted";

                    // Take me to your leader
                    switch (all[i].type)
                    {
                        case SpiritBase.SpiritType.Fire:
                            fireBar.GetComponent<UIBarControl>().StartCoroutine("UpdateBar", increment);
                            break;
                        case SpiritBase.SpiritType.Air:
                            airBar.GetComponent<UIBarControl>().StartCoroutine("UpdateBar", increment);
                            break;
                        case SpiritBase.SpiritType.Earth:
                            earthBar.GetComponent<UIBarControl>().StartCoroutine("UpdateBar", increment);
                            break;
                        case SpiritBase.SpiritType.Water:
                            waterBar.GetComponent<UIBarControl>().StartCoroutine("UpdateBar", increment);
                            break;
                        default:
                            break;
                    }
                }
                if (all[i].gameObject.tag == "Counted")
                {
                    StartCoroutine(Particles(all[i]));
                    Destroy(all[i].gameObject);
                }
            yield return null;
            }


        yield return null;
    }

    IEnumerator Particles(SpiritBase spirit)
    {
        GameObject particles;
        GameObject target;
        Vector3 startPos = new Vector3(0, 0, 0);
        float time = Time.time;
        switch (spirit.type)
        {
            case SpiritBase.SpiritType.Fire:
                particles = fireParticles;
                target = fireBar;
                break;
            case SpiritBase.SpiritType.Air:
                particles = airParticles;
                target = airBar;
                break;
            case SpiritBase.SpiritType.Earth:
                particles = earthParticles;
                target = earthBar;
                break;
            case SpiritBase.SpiritType.Water:
                particles = waterParticles;
                target = waterBar;
                break;
            default:
                particles = null;
                target = null;
                break;
        }

       var particleObject =  Instantiate(particles, gameObject.transform.position, Quaternion.identity) as GameObject;
        particleObject.GetComponent<ParticleSystem>().startSize *= 0.3f;
        particleObject.GetComponent<ParticleSystem>().Stop();
        AudioManager.PlaySound("FX/Gameplay/Spirits/Totem-Move", particleObject);

        while (Vector3.SqrMagnitude(target.transform.position - particleObject.transform.position) >= 5)
        {
            particleObject.transform.position = Vector3.Lerp(startPos, target.transform.position, (Time.time - time) * particleSpeedMultiplier);
            yield return null;
        }

        Destroy(particleObject);
    }

}
