using UnityEngine;
using System.Collections;
using LibPDBinding;
using System.Collections.Generic;

public class BasicTower : MonoBehaviour {

	public GameObject projectile;
	public float reloadTime = 1;
	public int upgradeCost = 10;

	private List<Transform> targets = new List<Transform> ();

	private float nextFireTime;

	void OnMouseDown(){
		GUIManager.showTowerGUI (this.gameObject);
	}


	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Enemy") {
			targets.Add(c.gameObject.transform);
		}
	}

	void OnTriggerExit(Collider c){
			targets.Remove(c.gameObject.transform);	
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (targets.Count > 0){
				if (Time.time >= nextFireTime) {
					nextFireTime = Time.time + reloadTime;
					this.transform.LookAt(targets[0].localPosition);
					Instantiate (projectile, transform.position, transform.rotation);
					LibPD.SendMessage("TowerPos", "float", 0.5);
					LibPD.SendMessage("TowerBang","bang");
				}	
		}
	}

	public void upgradetower(){
		reloadTime = reloadTime / 1.5f;
		upgradeCost = (int)Mathf.Round(upgradeCost * 1.5f);
	}
}
