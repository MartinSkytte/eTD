using UnityEngine;
using System.Collections;
using LibPDBinding;

public class BombProjectile : MonoBehaviour {
	
	public float range = 10;
	public float speed = 1;
	public int damage = 2;
	public int blastRadius = 1;
	
	private float distance;
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
		distance += Time.deltaTime * speed;
		
		if (distance >= range) {
			Explode();		
		}
	}
	
	void OnTriggerEnter(Collider c){
		if (c.gameObject.transform.tag == "Enemy") {
			LibPD.SendMessage("BoomPos", "float", (this.transform.position.x-20)/40);
			LibPD.SendMessage("BoomBang","bang");
			Explode();
		}
	}

	void Explode(){
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, blastRadius);
		int i = 0;



		while (i < hitColliders.Length) {
			if (hitColliders[i].gameObject.transform.tag == "Enemy") {
				hitColliders[i].GetComponent<UnitHealth>().TakeDamage(damage);
			}
			i++;
		}

		Destroy (gameObject);
	}
}
