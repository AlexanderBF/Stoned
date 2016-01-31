using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private bool mainMusicPlaying = false;

	void Awake () {
		Debug.Log("Loading AudioManager...");
		// Load the Fabric manager by loading up the Audio scene!
        if(AudioManager.FabricLoaded)
        {
            enabled = false;
            Destroy(gameObject);
            return;
        }
		AudioManager.LoadFabric();
	}

	void Update () {
		if (!mainMusicPlaying) {
			if (AudioManager.FabricLoaded) {
				Debug.Log("Starting music...");
				mainMusicPlaying = true;
				AudioManager.PlaySound("MX/Main_Loop");
			}
		}
	}
}
