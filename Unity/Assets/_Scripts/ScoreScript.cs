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
        float spawnDelay = .5f; 
        Vector3 spawnPos = new Vector3(0, 0, 0);
        float spiralAngle = -Mathf.PI / 8;
        yield return new WaitForSeconds(2);
        while (count > 0)
        {
            AudioManager.PlaySound("FX/Endgame/Score");
            //screenshake, sound effect, whatever
            spawnPos = new Vector3(spiralConstant * spiralAngle * Mathf.Cos(spiralAngle), spiralConstant * spiralAngle * Mathf.Sin(spiralAngle), 0);
            spiralAngle += (Mathf.PI / 2 * spiralConstant);
            spiralConstant -= (spiralConstant - 0.15f) * 0.15f;
            Instantiate(sun, spawnPos, Quaternion.identity);
            count--;
            spawnDelay -= (spawnDelay - 0.1f) * 0.1f;
            yield return new WaitForSeconds(spawnDelay);
        }
        if (count == 0)
        {
            yield return StartCoroutine(RestartGame());
        }
    }

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName == "Main Alex")
        {
            days = 0;
        }
        if (Application.loadedLevelName == "EndScene")
        {
            if (days > 0)
            {
                StartCoroutine(SunCounter(days));
            }
        }
    }
}
