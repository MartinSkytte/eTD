using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GUIText CurrentNumberText, GoalNumberText, GameWonText, GameOverText, InstructionsText, MatchText, TheGameText;

	private static GUIManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameWon += GameWon;
		TheGameText.text = "eTD";
		//SetMidText("Start");
		//TheGameText.text= "eTD The Game";
		//sTheGameText.text="eTD The Game";
		//GameOverText.enabled = false;
		//GameWonText.enabled = false;
		//TheGameText.text = "eTD";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			GameEventManager.TriggerGameStart ();
		}
	}

	public static void SetMidText(string tex){
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
		}
	}


	public static void SetCurrentNumber(int currentN){
		instance.CurrentNumberText.text = currentN.ToString();
	}

	public static void SetGoalNumber(int goalN){
		instance.GoalNumberText.text = goalN.ToString();
	}


	private void GameOver(){
		//SetMidText ("Lost");
		//GameWonText.enabled = false;
		//GameOverText.enabled = true;
		TheGameText.enabled = true;
		TheGameText.text="Game Over";
		InstructionsText.enabled = true;
		enabled = true;

	}


	private void GameStart(){
		//TheGameText.enabled = false;
		TheGameText.enabled = false;
		//GameWonText.enabled = false;
		//GameOverText.enabled = false;
		//TheGameText.enabled = false;
		InstructionsText.enabled = false;
		enabled = false;

	}

	private void GameWon(){
		TheGameText.enabled = true;
		SetMidText("Win");
		//TheGameText.text="You Win";
		//GameWonText.enabled = true;
		//GameOverText.enabled = false;
		InstructionsText.enabled = true;
		enabled = true;
	}


}
