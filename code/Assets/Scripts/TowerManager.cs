using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {

	public GameObject[] towers;

	private TowerPlacement towerPlacement;

	// Use this for initialization
	void Start () {
		towerPlacement = GetComponent<TowerPlacement> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		for (int i = 0; i < towers.Length; i++) {
			if(GUI.Button(new Rect(Screen.width/20,Screen.height/15 + Screen.height/12 * i,120,30),towers[i].name + " (10$)")){
				towerPlacement.setItem(towers[i]);
			}	
		
			GUI.Box(new Rect(Screen.width/20*4,Screen.height/15,150,30), "money:"+" "+towerPlacement.money.ToString()+"$");
		
		}

	}
}
