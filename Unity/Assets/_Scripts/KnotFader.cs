using UnityEngine;
using System.Collections;

public class KnotFader : MonoBehaviour {
	public Material fadeoutMaterial;
	public Color clear;
	
	void Update(){
		fadeoutMaterial.color = Color.Lerp (Color.white, clear, Mathf.PingPong (Time.time, 1));
	}

	public void enableMeshRenderer() {
		GetComponent<MeshRenderer> ().enabled = true;
		fadeoutMaterial.color = Color.Lerp (Color.white, clear, Mathf.PingPong (Time.time, 1));
		Invoke ("disableMeshRenderer", 1);
	}
	
	private void disableMeshRenderer() {
		GetComponent<MeshRenderer> ().enabled = false;
	}
}
