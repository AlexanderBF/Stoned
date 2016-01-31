using UnityEngine;
using System.Collections;

public class SoundSpiritCollectionTrigger : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter () {
		Debug.Log("We have collected a spirit!");
		AudioManager.PlaySound("FX/Gameplay/Spirits/Collected", this.gameObject);
	}
	
}
