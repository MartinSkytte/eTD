using UnityEngine;
using System.Collections;

public class BasicUnit : MonoBehaviour {

	public float health = 100;

	public GameObject explosionEffect;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void takeDamage(float damageAmount){//this function is called by the projectile when it collides with the enemy
		health -= damageAmount;
		if (health <= 0) 
		{
			explode ();
			return;
		}
	}
	void explode(){
		Instantiate (explosionEffect, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
