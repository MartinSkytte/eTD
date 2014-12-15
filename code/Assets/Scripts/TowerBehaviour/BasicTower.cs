using UnityEngine;
using System.Collections;
using LibPDBinding;
using System.Collections.Generic;

public class BasicTower : MonoBehaviour {

	public GameObject projectile;
	public float reloadTime = 1;
	public int upgradeCost = 10;
	public bool isPlaced = false;
//	private Queue<Transform> targets = new Queue<Transform>();

	private float nextFireTime;

	void OnMouseDown(){
		GUIManager.showTowerGUI (this.gameObject);
	}


//	void OnTriggerEnter(Collider c){
//		if (c.gameObject.tag == "Enemy") {
//			targets.Enqueue(c.gameObject.transform);
//		}
//	}

//	void OnTriggerExit(Collider c){
//		
//		if (c.gameObject.tag == "Enemy") {
//			targets.Dequeue();
//		}
//	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int i;
		Collider[] targets = Physics.OverlapSphere(this.transform.position, 5);
		if (targets.Length > 0){
			for(i = 0; i < targets.Length;){
				if(targets[i].gameObject.tag == "Enemy"){
					break;
				}else{
					i++;
				}

			}
				if (Time.time >= nextFireTime) {
					nextFireTime = Time.time + reloadTime;
					this.transform.LookAt(targets[i].transform.position);
					Instantiate (projectile, transform.position, transform.rotation);
					
					LibPD.SendMessage("TowerPos", "float", (this.transform.position.x-20)/40);
					LibPD.SendMessage("TowerBang","bang");
				}	
		}
	}

	public void upgradetower(){
		reloadTime = reloadTime / 1.5f;
		upgradeCost = (int)Mathf.Round(upgradeCost * 1.5f);
	}
}
