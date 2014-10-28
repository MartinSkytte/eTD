using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBase : MonoBehaviour {

	
	[HideInInspector]
	public List<Collider> colliders = new List<Collider>();

	void OnTriggerEnter(Collider c){
		colliders.Add (c);
		Debug.Log ("entered");
	}
	
	void OnTriggerExit(Collider c){
		colliders.Remove(c);	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
