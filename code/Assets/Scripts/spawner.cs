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
	private int amount = 0;
	public static int unitsInWave = 0;

	// Use this for initialization
	void Start () {
		infoList = (LevelInfoList)gameObject.GetComponent<LevelInfoList> ();
		levelInfo = infoList.levelsInfo;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimeLeft <= 0 && amount != 0) {
			spawner.unitsInWave++;
			GameObject creep = (GameObject)Instantiate(levelInfo[currentWave].creep, transform.position, transform.rotation);
			creep.GetComponent<AIPather>().target = target;
			creep.GetComponent<AIPather>().maxWaypointDistance = 0.5f;
			creep.GetComponent<UnitHealth>().health = levelInfo[currentWave].health;

			amount--;
			spawnTimeLeft = spawnTime;
		} else {
			spawnTimeLeft -= Time.deltaTime;
		}
		Debug.Log ("amount: " + spawner.unitsInWave);
	}

	public void nextWave(){
		try {
			currentWave++;
			spawnTime = levelInfo [currentWave].spawnTime;
			spawnTimeLeft = spawnTime;
			amount = levelInfo [currentWave].amount;
		} catch (UnityException ue) {
			Debug.Log(ue.Message);
		}
	}
}
