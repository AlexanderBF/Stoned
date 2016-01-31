using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			StartCoroutine(FadeToNextLevel());
		}
		if(Input.GetMouseButton(0)){
			StartCoroutine(FadeToNextLevel());
		}
	}

	IEnumerator FadeToNextLevel(){
		float fadeTime = GameObject.Find ("Title Screen").GetComponent<Fader> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel ("Main Alex");
	}
}
