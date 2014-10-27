﻿using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GUIText CurrentNumberText, GoalNumberText, GameWonText, GameOverText, InstructionsText, MatchText;

	private static GUIManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameOverText.enabled = false;
		GameWonText.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			GameEventManager.TriggerGameStart ();
		}
	}

	public static void SetCurrentNumber(int currentN){
		instance.CurrentNumberText.text = currentN.ToString();
	}

	public static void SetGoalNumber(float goalN){
		instance.GoalNumberText.text = goalN.ToString();
	}


	private void GameOver(){
		GameWonText.enabled = false;
		GameOverText.enabled = true;
		InstructionsText.enabled = true;
		enabled = true;

	}


	private void GameStart(){
		GameWonText.enabled = false;
		GameOverText.enabled = false;
		InstructionsText.enabled = false;
		enabled = false;
	}

	private void GameWon(){
		GameWonText.enabled = true;
		InstructionsText.enabled = true;
		enabled = true;
	}


}
