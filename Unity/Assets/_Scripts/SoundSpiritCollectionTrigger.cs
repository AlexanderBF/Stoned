using UnityEngine;
using System.Collections;

public class SoundSpiritCollectionTrigger : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter (Collider other) {
		Debug.Log("We have collected a spirit!");
		AudioManager.PlaySound("FX/Gameplay/Spirits/Collected", this.gameObject);

		other.gameObject.layer = LayerManager.Spirit;
		FindObjectOfType<KnotFader>().enableMeshRenderer();

        // sorry about this hack future programmer
        ParticleSystem[] particles = other.gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem ps in particles)
        {
            ps.Stop();
        }
	}
	
}
