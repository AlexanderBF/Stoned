using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {

    public int days;
    public GameObject sun;
    public float spiralConstant;

    GameObject mainCam;
    float alpha;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //for testing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Application.loadedLevelName == "Main Alex")
            {
                StartCoroutine(EndGame());
            }
        }
    }

    public IEnumerator EndGame()
    {
		float fadeTime = GameObject.Find ("GameManager").GetComponent<Fader> ().BeginFade (1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel("EndScene");
    }

    public  IEnumerator RestartGame()
    {
        float fadeTime = GameObject.Find("GameManager").GetComponent<Fader>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.LoadLevel("TitleScreen");
    }
    public IEnumerator SunCounter(int count)
    {
        Vector3 spawnPos = new Vector3(0, 0, 0);
        float spiralAngle = 0;
        yield return new WaitForSeconds(2);
        while (count > 0)
        {
            Instantiate(sun, spawnPos, Quaternion.identity);
            //screenshake, sound effect, whatever
            spawnPos = new Vector3(spiralConstant * spiralAngle * Mathf.Cos(spiralAngle), spiralConstant * spiralAngle * Mathf.Sin(spiralAngle), 0);
            spiralAngle += Mathf.PI / 10;
            spiralConstant -= spiralConstant * 0.01f;
            count--;
            yield return new WaitForSeconds(.1f);
        }
        if (Input.touchCount == 0)
        {
            yield return new WaitForSeconds(2);
        }
        yield return StartCoroutine(RestartGame());
    }

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName == "EndScene")
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            if (days > 0)
            {
                StartCoroutine(SunCounter(days));
            }
        }
    }
}
