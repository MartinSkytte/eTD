﻿using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour {


	void OnTriggerEnter (Collider other)
	{

		if (other.gameObject.tag == "Enemy") {
			Destroy (other.gameObject);
		}
	}

}
