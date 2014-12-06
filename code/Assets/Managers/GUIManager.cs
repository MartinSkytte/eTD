using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GUIText CurrentNumberText, GoalNumberText;

	public GUIStyle textStyle, gameOverStyle;

	private static GUIManager instance;
	private bool firstRun;
	
	public GameObject[] towers;
	
	private TowerPlacement towerPlacement;
	private spawner spawn;

	private string nextWaveText;

	public static GameObject TDTutorial;
	public static GameObject MatchTutorial;

	//public static GameObject TDTutorial = GameObject.FindGameObjectWithTag("TDTutorial");
	//public static GameObject MatchTutorial = GameObject.FindGameObjectWithTag("MatchTutorial");

	//TowerGUI variables
	public bool gameOver;
	private BasicTower tower;
	private bool showTowerMenu;
	public static float sWidth, sHeight;
	static bool endTutorial;
	public static int lives;
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


		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;

		lives = 20;
		//textStyle = new GUIStyle ();
		textStyle.fontSize = 22;
		gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			GameEventManager.TriggerGameStart ();
		}
		if (lives < 1)
			gameOver = true;
	}

	//public void startMatch3(){
	//	/*GameObject match3object = */GameObject.FindGameObjectWithTag("Match3Object").SetActive (true);
	//}

	public static void endMatchTutorial(){
		GUIManager.TDTutorial.SetActive(true);
		GUIManager.MatchTutorial.SetActive(false);
		endTutorial = false;
	}
	public static void endTDTutorial(){
		GUIManager.TDTutorial.SetActive(false);
	
	}


	public static void SetCurrentNumber(int currentN){
		instance.CurrentNumberText.text = "Score: " + currentN.ToString();
	}
	
	public static void SetSpawnButtonText(string text) {
		instance.nextWaveText = text;
	}

	public static void SetGoalNumber(int goalN){
		instance.GoalNumberText.text = "Goal: " + goalN.ToString();
	}
	
	//public static void SetCredits(int credits){
		//instance.CreditsText.text = credits.ToString ()+ "$";
	//}
	
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

		
		GUI.Box(new Rect(sWidth * 51.5f,sHeight * 10,150,30), "Credits: "+towerPlacement.money.ToString()+"$",textStyle);
		GUI.Box (new Rect (sWidth * 51.5f+150, sHeight * 10, 150, 30), "Lives: " + lives.ToString () , textStyle);
		if (gameOver) 
		{
			spawn.wavesEnabled = false;
			GUI.Box (new Rect (sWidth * 10, sHeight * 30, sWidth * 80, sHeight * 40), "GAME OVER", gameOverStyle);
			if (GUI.Button (new Rect (sWidth * 50, sHeight * 80, 100, 30), "New Game?")) 
			{
				//do things to start a new game
			}
		}
		if (!endTutorial && !gameOver)
		{
			for (int i = 0; i < towers.Length; i++) {
				if (GUI.Button (new Rect (sWidth*5 + 165*i,sHeight * 18 , 160, 30), towers [i].name + " (10$)")) {
					towerPlacement.setItem (towers [i]);
				}	
			}

			if(GUI.Button(new Rect(sWidth*50,sHeight * 20,100,30), nextWaveText)) {
				spawn.wavesEnabled = true;
				spawn.nextWave();			
			}

			if(instance.showTowerMenu){
				//GUI.Box(new Rect(Screen.width/20,Screen.height/20 + Screen.height/12*10,100,30), tower.name,textStyle);
				GUI.Box(new Rect(sWidth*13,sHeight*82 ,100,30), tower.name,textStyle);
				if(GUI.Button(new Rect(sWidth*13+150,sHeight*82,100,30), "Upgrade:"+tower.upgradeCost+"$")) {
					if(tower.upgradeCost <= towerPlacement.money){
						towerPlacement.mt.credits -= tower.upgradeCost;
						tower.upgradetower();			
					}
				}
			}

			
		}
	}
}	



