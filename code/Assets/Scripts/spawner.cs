using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawner : MonoBehaviour {

	public int currentWave;
	private LevelInfoList infoList;
	private List<LevelInfoList.LevelInfo> levelInfo;

	// Use this for initialization
	void Start () {
		infoList = (LevelInfoList)gameObject.GetComponent<LevelInfoList> ();
		levelInfo = infoList.levelsInfo;
		InitiateNewWave (currentWave);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void InitiateNewWave(int waveNumber) {
		for (int i = 0; i < levelInfo[waveNumber].amount; i++) {
			GameObject creep = levelInfo[waveNumber].creep;
			Debug.Log(creep.ToString());
			AIPather path = creep.AddComponent<AIPather>();
			path.target = levelInfo[waveNumber].target;
			path.speed = 5;
			path.maxWaypointDistance = 0.5f;

			Instantiate(creep, transform.position, transform.rotation);
		}
	}
}
