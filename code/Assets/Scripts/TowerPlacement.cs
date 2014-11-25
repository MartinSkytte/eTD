using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour {

	private TowerBase towerBase;
	private Transform currentTower;
	private bool hasPlaced;
	private Camera cam;
	private Match3 mt;
	private GameObject tempTower; // variable for making checks with price ect.

	public int money;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Camera").camera;
		mt = GameObject.Find ("GameObject").GetComponent<Match3> ();
	}
	
	// Update is called once per frame
	void Update () {
		money = mt.credits;
		if (currentTower && !hasPlaced) {
			Vector3 m = Input.mousePosition;
			m = new Vector3(m.x, m.y, transform.position.z);
			Vector3 p = cam.ScreenToWorldPoint(m);
			currentTower.position = new Vector3(Mathf.RoundToInt(p.x),Mathf.RoundToInt(p.y),-1);
		
		}

		if (Input.GetMouseButtonDown(0)) {
			if(isLegalPosition()){
					hasPlaced = true;
			}
		}
	}

	public void setItem(GameObject t){
		tempTower = (GameObject)Instantiate (t);
		if (tempTower.transform.GetComponentInChildren<TowerBase>().price <= mt.credits) {

			hasPlaced = false;
			currentTower = tempTower.transform;
			towerBase = currentTower.GetComponentInChildren<TowerBase> ();
			mt.credits -= towerBase.price;
		}
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
