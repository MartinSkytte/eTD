using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GUIText CurrentNumberText, GoalNumberText, InstructionsText, TheGameText, TimerText, CreditsText;

	private static GUIManager instance;

	public GameObject[] towers;
	
	private TowerPlacement towerPlacement;
	private spawner spawn;
	private GUI

	// Use this for initialization
	void Start () {
		towerPlacement = GameObject.Find ("Managers").GetComponent<TowerPlacement> ();
		spawn = GameObject.Find("Spawn").GetComponent<spawner>();
		instance = this;
		this.OnGUI ();

		CreditsText.enabled = false;

		GameEventManager.GameWon += GameWon;
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;
		SetMidText("Start");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			GameEventManager.TriggerGameStart ();
		}
	}

	public static void SetMidText(string tex){
		instance.TheGameText.enabled = true;
		switch (tex) {
		case "Start":
			instance.TheGameText.text="eTD The Game";
			break;
		case "Win":
			instance.TheGameText.text="You Win";
			break;
		case "Lost":
			instance.TheGameText.text="Game Over";
			break;
		case "Nothing":
			instance.TheGameText.enabled = false;
			break;
		}
	}

	public static void SetSpawnButtonText(string text) {
		instance.spawn.guiText.text = text;
	}

	public static void SetCurrentNumber(int currentN){
		instance.CurrentNumberText.text = currentN.ToString();
	}

	public static void SetGoalNumber(int goalN){
		instance.GoalNumberText.text = goalN.ToString();
	}

	public static void SetCurrentTime(int currentT){//countdown timer in seconds
		instance.TimerText.text = currentT.ToString();
	}

	public static void SetCredits(int credits){
		//instance.CreditsText.text = credits.ToString ()+ "$";
	}

	private void GameOver(){
		SetMidText ("Lost");
		InstructionsText.enabled = true;
		enabled = true;

	}
	
	private void GameStart(){
		SetMidText("Start");
		InstructionsText.enabled = false;
		enabled = false;
	}

	private void GameWon(){
		SetMidText("Win");
		InstructionsText.enabled = true;
		enabled = true;
	}

	void OnGUI() {
		for (int i = 0; i < towers.Length; i++) {
			if(GUI.Button(new Rect(Screen.width/20,Screen.height/20 + Screen.height/12 * i,120,30),towers[i].name + " (10$)")){
				towerPlacement.setItem(towers[i]);
			}	
			
			GUI.Box(new Rect(Screen.width/2,Screen.height/20,150,30), "money:"+" "+towerPlacement.money.ToString()+"$");

			if(GUI.Button(new Rect(Screen.width/20*4,Screen.height/20 + Screen.height/12 * 0,100,30), "Next Wave")) {
				spawn.nextWave();			}
		}
		
	}
}
