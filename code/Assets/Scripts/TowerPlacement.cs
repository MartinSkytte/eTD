using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour {

	private TowerBase towerBase;
	private Transform currentTower;
	private bool hasPlaced;

	public int money;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTower && !hasPlaced) {
			Vector3 m = Input.mousePosition;
			m = new Vector3(m.x, m.y, transform.position.z);
			Vector3 p = camera.ScreenToWorldPoint(m);
			currentTower.position = new Vector3(Mathf.RoundToInt(-p.x),Mathf.RoundToInt(-p.y),-1);
		
		}

		if (Input.GetMouseButtonDown(0)) {
			if(isLegalPosition()){
				if (money >= towerBase.price) {
					if(hasPlaced == false){
						money -= towerBase.price;
					}
					hasPlaced = true;
				}
			}
		}
	}

	public void setItem(GameObject t){
		hasPlaced = false;
		currentTower = ((GameObject)Instantiate (t)).transform;
		towerBase = currentTower.GetComponentInChildren<TowerBase> ();

	}

	bool isLegalPosition(){
		Debug.Log("wallcount "+towerBase.WallColliders.Count);
				if (towerBase.BuildingColliders.Count > 0 || towerBase.WallColliders.Count <= 0) {
						return false;
				} else {
						return true;
				}
		}
}
