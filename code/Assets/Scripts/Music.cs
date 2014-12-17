using UnityEngine;
using System.Collections;
using LibPDBinding;

public class Music : MonoBehaviour {

	private float nextActionTime = 0.0f; 
	public float period = 1f;

	// Use this for initialization
	void Start () {
		LibPD.SendMessage ("Load-current-melody", "bang");
		LibPD.SendMessage ("bpm", "float", 120f);
		LibPD.SendMessage ("musicVolume", "float", 1f);
	}
}
