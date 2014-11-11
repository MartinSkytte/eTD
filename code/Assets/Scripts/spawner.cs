using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawner : MonoBehaviour {

	public int currentWave;
	public Transform target;
	private LevelInfoList infoList;
	private List<LevelInfoList.LevelInfo> levelInfo;
	private float spawnTime;
	private float spawnTimeLeft;
	private int amount;

	// Use this for initialization
	void Start () {
		infoList = (LevelInfoList)gameObject.GetComponent<LevelInfoList> ();
		levelInfo = infoList.levelsInfo;
		spawnTime = levelInfo [currentWave].spawnTime;
		spawnTimeLeft = spawnTime;
		amount = levelInfo [currentWave].amount;
		Debug.Log ("amount: " + amount + " spawnTime: " + spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimeLeft <= 0 && amount != 0) {

			GameObject creep = (GameObject)Instantiate(levelInfo[currentWave].creep, transform.position, transform.rotation);
			creep.GetComponent<AIPather>().target = target;
			creep.GetComponent<AIPather>().maxWaypointDistance = 0.5f;

			Debug.Log ("amount: " + amount + " spawnTime: " + spawnTime);
			amount--;
			spawnTimeLeft = spawnTime;
		} else {
			spawnTimeLeft -= Time.deltaTime;
		}
	}
}
