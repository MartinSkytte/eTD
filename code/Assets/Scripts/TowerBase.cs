using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBase : MonoBehaviour {


	public int price = 10;

	
	[HideInInspector]
	public List<Collider> BuildingColliders = new List<Collider>();


	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Building"){
			Debug.Log (c.gameObject.name);
			BuildingColliders.Add (c);
		}
	}
	
	void OnTriggerExit(Collider c){
		if (c.gameObject.tag == "Building") {
			BuildingColliders.Remove (c);	
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
