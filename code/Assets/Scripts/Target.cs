using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{

		void OnTriggerEnter (Collider c)
		{
				if (c.gameObject.tag == "Enemy") {
						Destroy (c.gameObject);
						spawner.unitsInWave--;
						if (GUIManager.lives > 0)
							GUIManager.lives--;
				}
		}
}
