using UnityEngine;
using System.Collections;

public class BasicTower : MonoBehaviour {

	public GameObject projectile;
	public float reloadTime = 1;
	public Transform target;

	private float nextFireTime;




	void OnTriggerEnter(Collider c){
		target = c.gameObject.transform;
	}

	void OnTriggerExit(Collider c){
		if (c.gameObject.transform == target) {
			target = null;	
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target){
				if (Time.time >= nextFireTime) {
						nextFireTime = Time.time + reloadTime;
						this.transform.LookAt(target.localPosition);
						Instantiate (projectile, transform.position, transform.rotation);
				}	
		}
	}


}
