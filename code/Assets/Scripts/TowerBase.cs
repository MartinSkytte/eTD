using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBase : MonoBehaviour {

	public int price = 10;

	
	[HideInInspector]
	public List<Collider> colliders = new List<Collider>();

	void OnTriggerEnter(Collider c){
		colliders.Add (c);
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
