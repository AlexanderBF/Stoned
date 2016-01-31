using UnityEngine;
using System.Collections;

public class SpiritPool : MonoBehaviour {
	private ParticleSystem pool;

	public void brightenPool() {
		pool = this.gameObject.GetComponent<ParticleSystem> ();
		pool.startColor = new Color (pool.startColor.r + 0.1f,
		                            pool.startColor.g + 0.1f,
		                            pool.startColor.b + 0.1f,
		                            pool.startColor.a);
	}
	
	public void darkenPool() {
		pool = this.gameObject.GetComponent<ParticleSystem> ();
		pool.Stop();
	}
}
