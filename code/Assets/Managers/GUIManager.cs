using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LibPDBinding;


public class GUIManager : MonoBehaviour {
	
	public GUIText CurrentNumberText, GoalNumberText;

	public GUIStyle textStyle, gameOverStyle, nextWaveStyle;

	private static GUIManager instance;
	private bool firstRun;
	
	public GameObject[] towers;
	public int[] towerPrices;
	
	private TowerPlacement towerPlacement;
	private spawner spawn;

	private string nextWaveText;
	private int currentLevel;
	public float sfxVolume = 1.0f;
	public float musicVolume = 1.0f;
	public float masterVolume = 1.0f;

	public static GameObject TDTutorial;
	public static GameObject MatchTutorial;
	public GameObject selectedIndicator;
	private GameObject tmpTower;

	//TowerGUI variables
	public bool gameOver;
	private BasicTower tower;
	private bool showTowerMenu;
	public static float sWidth, sHeight;
	static bool endTutorial;
	public static int lives;
	private int current, goal;
	public bool endTDFlag;
	// Use this for initialization
	void Start () {
		TDTutorial = GameObject.FindGameObjectWithTag("TDTutorial");
		GUIManager.MatchTutorial = GameObject.FindGameObjectWithTag("MatchTutorial");
		GUIManager.TDTutorial.SetActive(false);

		towerPlacement = GameObject.Find ("Managers").GetComponent<TowerPlacement> ();
		spawn = GameObject.Find("Spawn").GetComponent<spawner>();
	 	instance = this;
		sWidth = Screen.width/100;
		sHeight = Screen.height/100;
		
		endTutorial = true;
		endTDFlag = false;

		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;

		lives = 20;
		textStyle.fontSize = 22;
		gameOver = false;

	}
	private int updateSound;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			GameEventManager.TriggerGameStart ();
		}
		if (lives < 1)
			gameOver = true;

		//only every 50th update to not take up to many rescources.
		if (updateSound > 50) {
			LibPD.SendMessage ("sfxVolume", "float", sfxVolume);
			LibPD.SendMessage ("musicVolume", "float", musicVolume);
			LibPD.SendMessage ("masterVolume", "float", masterVolume);
			updateSound = 0;
		} else {
			updateSound++;	
		}
	}


	public static void endMatchTutorial(){
		GUIManager.TDTutorial.SetActive(true);
		GUIManager.MatchTutorial.SetActive(false);
		endTutorial = false;
		instance.CurrentNumberText.text = "";
		instance.GoalNumberText.text = "";
		instance.endTDFlag = true;
	}
	public static void endTDTutorial(){
		instance.endTDFlag = false;
		GUIManager.TDTutorial.SetActive(false);
		instance.CurrentNumberText.text = "Score: " + instance.current.ToString();
		instance.GoalNumberText.text = "Goal: " + instance.goal.ToString();
	}


	public static void SetCurrentNumber(int currentN){
		instance.current = currentN;
		if (!instance.endTDFlag)
			instance.CurrentNumberText.text = "Score: " + currentN.ToString();
	}

	public static void SetGameOver(bool gameover) {
		instance.gameOver = gameover;
	}

	public static void SetLevelNumber(int level) {
		instance.currentLevel = level;
	}

	public static void SetSpawnButtonText(string text) {
		instance.nextWaveText = text;
	}

	public static void SetGoalNumber(int goalN){
		instance.goal = goalN;
		if (!instance.endTDFlag)
			instance.GoalNumberText.text = "Goal: " + goalN.ToString();
	}

	
	public static void showTowerGUI(GameObject selected){
		instance.showTowerMenu = true;
		instance.tower = selected.GetComponent<BasicTower>();
		instance.selectedIndicator.transform.position = instance.tower.transform.position;
	}
	
	private void GameOver(){

	}
	
	private void GameStart(){

	}
	
	private void GameWon(){

		enabled = true;
	}
	
	void OnGUI() {

		sfxVolume = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), sfxVolume, 0.0F, 1.0F);
		GUI.Box (new Rect(120, 15, 100, 30),"sfx");
		musicVolume = GUI.HorizontalSlider(new Rect(25, 50, 100, 30), musicVolume, 0.0F, 1.0F);
		GUI.Box (new Rect(120, 40, 100, 30),"music");
		masterVolume = GUI.HorizontalSlider(new Rect(25, 75, 100, 30), masterVolume, 0.0F, 1.0F);
		GUI.Box (new Rect(120, 65, 100, 30),"master");

		GUI.Box(new Rect(sWidth * 55f,sHeight * 10,150,30), "Credits: "+towerPlacement.money.ToString()+"$",textStyle);
		GUI.Box (new Rect (sWidth * 55f+145, sHeight * 10, 150, 30), "Lives: " + lives.ToString () , textStyle);
		GUI.Box (new Rect (sWidth * 55f+260, sHeight * 10, 150, 30), "Level: " + currentLevel.ToString () , textStyle);
		if (gameOver) 
		{

			spawn.wavesEnabled = false;
			GUI.Box (new Rect (sWidth * 10, sHeight * 30, sWidth * 80, sHeight * 40), "GAME OVER", gameOverStyle);
			if (GUI.Button (new Rect (sWidth * 50, sHeight * 80, 100, 30), "New Game?")) 
			{
				//do things to start a new game
				Debug.Log(Application.loadedLevel);
				Application.LoadLevel(Application.loadedLevel);
			}

		}
		if (!endTutorial && !gameOver)
		{
			for (int i = 0; i < towers.Length; i++) {
				if (GUI.Button (new Rect (sWidth*4 + 165*i,sHeight * 18 , 160, 30), towers [i].name + " ("+towerPrices[i]+"$)")) {
					towerPlacement.setItem (towers [i]);
				}	
			}

			if(GUI.Button(new Rect(sWidth*35,sHeight * 10,100,30), nextWaveText,nextWaveStyle)) {
				spawn.wavesEnabled = true;
				spawn.nextWave();			
			}

			if(instance.showTowerMenu){
				GUI.Box(new Rect(sWidth*13,sHeight*88 ,100,30), tower.name,textStyle);
				if(GUI.Button(new Rect(sWidth*13+150,sHeight*88,100,30), "Upgrade:"+tower.upgradeCost+"$")) {
					if(tower.upgradeCost <= towerPlacement.money){
						towerPlacement.mt.credits -= tower.upgradeCost;
						tower.upgradetower();			
					}
				}
			}

			
		}
	}
}	



