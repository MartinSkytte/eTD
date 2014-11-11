using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBase : MonoBehaviour {


	public int price = 10;

	
	[HideInInspector]
	public List<Collider> BuildingColliders = new List<Collider>();
	[HideInInspector]
	public List<Collider> WallColliders = new List<Collider>();

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Building"){
			BuildingColliders.Add (c);
		}
		if (c.gameObject.layer == LayerMask.NameToLayer("Obstacle")){
			Debug.Log("");
			WallColliders.Add (c);
		}

	}
	
	void OnTriggerExit(Collider c){
		if (c.gameObject.tag == "Building") {
			BuildingColliders.Remove (c);	
		}
		if (c.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
			WallColliders.Remove (c);	
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
