﻿using UnityEngine;
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

	// Use this for initialization
	void Start () {
		infoList = (LevelInfoList)gameObject.GetComponent<LevelInfoList> ();
		levelInfo = infoList.levelsInfo;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimeLeft <= 0 && amount != 0) {

			GameObject creep = (GameObject)Instantiate(levelInfo[currentWave].creep, transform.position, transform.rotation);
			creep.GetComponent<AIPather>().target = target;
			creep.GetComponent<AIPather>().maxWaypointDistance = 0.5f;

			amount--;
			spawnTimeLeft = spawnTime;
		} else {
			spawnTimeLeft -= Time.deltaTime;
		}
	}

	void OnGUI() {
		if(GUI.Button(new Rect(Screen.width/5,Screen.height/15 + Screen.height/12 * 0,100,30), "Next Wave")) {
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
}
