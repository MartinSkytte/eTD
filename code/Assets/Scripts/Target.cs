using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	void OnTriggerEnter(Collider c) {
		Destroy(c.gameObject);
	}
}
