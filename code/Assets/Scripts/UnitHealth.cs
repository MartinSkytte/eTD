using UnityEngine;
using System.Collections;

public class UnitHealth : MonoBehaviour {

	public int health;

	public void TakeDamage(int amount) {
		health -= amount;
		Debug.Log ("Taken " + amount + " damage ");
		if (health <= 0)
			Destroy (gameObject);
	}
}
