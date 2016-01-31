using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {

    public int days;

    public Texture2D fadeTexture;
    public float fadeSpeed;
    public GameObject sun;
    public float spiralConstant;

    GameObject mainCam;
    float alpha;
    int fade = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(EndGame());
        }
    }

    void OnGUI()
    {
        alpha += (fadeSpeed * Time.deltaTime) * fade;
        Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }

    public IEnumerator EndGame()
    {
        fade = 1;
        yield return new WaitForSeconds(fadeSpeed);
        Application.LoadLevel("EndScene");
    }

    public IEnumerator SunCounter(int count)
    {
        Vector3 spawnPos = new Vector3(0, 0, 0);
        float spiralAngle = 0;
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
    }

    public IEnumerator ScreenShake(float time)
    {
        float timer = 0;
        Vector3 startPos= mainCam.transform.position;
        while (timer <= time)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), mainCam.transform.position.z);
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, randomPosition, Time.deltaTime);
            timer += Time.deltaTime;
        }
        yield return null;
        mainCam.transform.position = startPos;

    }

    void OnLevelWasLoaded()
    {
        if (Application.loadedLevelName == "EndScene")
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            fade = -1;
            if (days > 0)
            {
                StartCoroutine(SunCounter(days));
            }
        }
    }
}
