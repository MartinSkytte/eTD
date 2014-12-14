using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawner : MonoBehaviour {

	public int currentWave;
	public Transform target;
	public bool wavesEnabled;
	private LevelInfoList infoList;
	private List<LevelInfoList.LevelInfo> levelInfo;
	private float spawnTime;
	private float spawnTimeLeft;
	private int amount = 0;
	private float countDown = 10;
	private int waveCounter;
	private Queue<int> wavesQueue = new Queue<int> ();

	public static int unitsInWave = 0;
	private static GUIManager instance;

	// Use this for initialization
	void Start () {
		waveCounter = currentWave;
		infoList = (LevelInfoList)gameObject.GetComponent<LevelInfoList> ();
		levelInfo = infoList.levelsInfo;
		GUIManager.SetSpawnButtonText ("Next wave");
	}
	
	// Update is called once per frame
	void Update () {
		if ((levelInfo.Count-1) < currentWave)
			return;

		if (amount <= 0 && wavesQueue.Count != 0) {
			currentWave = wavesQueue.Dequeue();
			countDown = 10.0f;
			GUIManager.SetSpawnButtonText ("Next wave");
			GUIManager.SetLevelNumber((currentWave+1));

			if ((levelInfo.Count-1) < currentWave) {
				Debug.Log("GAME OVER");
				GUIManager.SetGameOver(true);
				return;
			}
			spawnTime = levelInfo [currentWave].spawnTime;
			spawnTimeLeft = spawnTime;
			amount = levelInfo [currentWave].amount;
			spawner.unitsInWave = amount;
		}

		if (spawnTimeLeft <= 0 && amount != 0) {
			GameObject creep = (GameObject)Instantiate(levelInfo[currentWave].creep, transform.position, transform.rotation);
			creep.GetComponent<AIPather>().target = target;
			creep.GetComponent<AIPather>().maxWaypointDistance = 0.5f;
			creep.GetComponent<UnitHealth>().health = levelInfo[currentWave].health;

			amount--;
			spawnTimeLeft = spawnTime;
		} else {
			spawnTimeLeft -= Time.deltaTime;
		}
		if (wavesEnabled) {
			if (spawner.unitsInWave <= 0) {
				countDown -= Time.deltaTime;
				GUIManager.SetSpawnButtonText ("Next wave (" + ((int)countDown).ToString () + ")");
				if (countDown <= 0) {
					nextWave ();
					countDown = 10.0f;
				}
			}
		}
	}


	public void nextWave(){
		try {
			GUIManager.endTDTutorial();
			waveCounter++;
			wavesQueue.Enqueue(waveCounter);
		} catch (UnityException ue) {
			Debug.Log(ue.Message);
		}
	}
}
