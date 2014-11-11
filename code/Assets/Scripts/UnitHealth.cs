using UnityEngine;
using System.Collections;

public class UnitHealth : MonoBehaviour {

	public int health;

	public void TakeDamage(int amount) {
		health -= amount;
<<<<<<< HEAD
		Debug.Log ("Taken " + amount + " damage ");
=======
>>>>>>> origin/master
		if (health <= 0)
			Destroy (gameObject);
	}
}
