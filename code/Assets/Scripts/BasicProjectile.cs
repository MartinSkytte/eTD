﻿using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour {

	public float range = 10;
	public float speed = 1;

	private float distance;
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
		distance += Time.deltaTime * speed;

		if (distance >= range) {
			Destroy(gameObject);		
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.transform.tag == "Enemy") {
			Destroy(c.gameObject);
			Destroy(gameObject);
		}
	}
}
