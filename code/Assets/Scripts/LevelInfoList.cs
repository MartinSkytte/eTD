using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelInfoList : MonoBehaviour {

	//Level info class
	[System.Serializable]
	public class LevelInfo {
		public GameObject creep;
		public Transform target;
		public int amount;
		public int health;
		public float spawnTime;
	}

	public List<LevelInfo> levelsInfo = new List<LevelInfo> (1);

	void AddNew() {
		levelsInfo.Add(new LevelInfo());
	}

	void Remove(int index) {
		levelsInfo.RemoveAt (index);
	}
}
