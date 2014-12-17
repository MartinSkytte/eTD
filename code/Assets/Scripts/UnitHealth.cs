using UnityEngine;
using System.Collections;
using LibPDBinding;

public class UnitHealth : MonoBehaviour
{

		public int health;

		public void TakeDamage (int amount)
		{
				health -= amount;
				if (health <= 0) {
						//weird error
						LibPD.SendMessage("DeathPos", "float", (this.transform.position.x-20)/40);
						LibPD.SendMessage("DeathBang","bang");
						Destroy (gameObject);
						spawner.unitsInWave--;
				}
		}
}
