using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GUIText CurrentNumberText, GoalNumberText, CreditsText;
	
	private static GUIManager instance;
	private bool firstRun;
	
	public GameObject[] towers;
	
	private TowerPlacement towerPlacement;
	private spawner spawn;

	private string nextWaveText;
	
	//TowerGUI variables
	private BasicTower tower;
	private bool showTowerMenu;
	public static float sWidth, sHeight;

	// Use this for initialization
	void Start () {
		towerPlacement = GameObject.Find ("Managers").GetComponent<TowerPlacement> ();
		spawn = GameObject.Find("Spawn").GetComponent<spawner>();
		//this.SetCurrentNumber (0);
	 	instance = this;
		sWidth = Screen.width/100;
		sHeight = Screen.height/100;

		CreditsText.enabled = false;
		
		GameEventManager.GameWon += GameWon;
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			GameEventManager.TriggerGameStart ();
		}
	}

	public static void endTutorial(){

		instance.GetComponentInChildren<Canvas> ().enabled = false;
		Debug.Log ("end tutorial");
		}

	public static void SetCurrentNumber(int currentN){
		Debug.Log ("supposed to be writing");
		instance.CurrentNumberText.text = "Score: " + currentN.ToString();
	}
	
	public static void SetSpawnButtonText(string text) {
		instance.nextWaveText = text;
	}

	public static void SetGoalNumber(int goalN){
		instance.GoalNumberText.text = "Goal: " + goalN.ToString();
	}
	
	public static void SetCredits(int credits){
		//instance.CreditsText.text = credits.ToString ()+ "$";
	}
	
	public static void showTowerGUI(GameObject selected){
		instance.showTowerMenu = true;
		instance.tower = selected.GetComponent<BasicTower>();
	}
	
	private void GameOver(){
		enabled = true;
		
	}
	
	private void GameStart(){
		//enabled = false;
	}
	
	private void GameWon(){

		enabled = true;
	}
	
	void OnGUI() {

		
		GUI.Box(new Rect(Screen.width/2,Screen.height/20,150,30), "money: "+towerPlacement.money.ToString()+"$");


		if (instance.GetComponentInChildren<Canvas> ().enabled == false)
		{
			for (int i = 0; i < towers.Length; i++) {
				if (GUI.Button (new Rect (Screen.width / 20, Screen.height / 20 + Screen.height / 12 * i, 120, 30), towers [i].name + " (10$)")) {
					towerPlacement.setItem (towers [i]);
				}	
			}

			if(GUI.Button(new Rect(Screen.width/20*4,Screen.height/20 + Screen.height/12 * 0,100,30), nextWaveText)) {
				spawn.wavesEnabled = true;
				spawn.nextWave();			
			}
			if(instance.showTowerMenu){
				GUI.Box(new Rect(Screen.width/20,Screen.height/20 + Screen.height/12*10,100,30), tower.name);
				if(GUI.Button(new Rect(Screen.width/20*4,Screen.height/20 + Screen.height/12*10,100,30), "Upgrade:"+tower.upgradeCost+"$")) {
					if(tower.upgradeCost <= towerPlacement.money){
						towerPlacement.mt.credits -= tower.upgradeCost;
						tower.upgradetower();			
					}
				}
			}
			
		}
	}
	
}


