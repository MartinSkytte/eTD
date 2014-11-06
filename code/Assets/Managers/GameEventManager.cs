using UnityEngine;
using System.Collections;

public static class GameEventManager {
	public delegate void GameEvent();

	public static event GameEvent GameStart, GameOver, GameWon;

	public static void TriggerGameStart(){
		if (GameStart != null) {
			GameStart();		
		}
	}
	public static void TriggerGameOver(){
		if (GameOver != null) {
			GameOver();
		}
	}
	public static void TriggerGameWon(){
		if (GameWon != null) {
			GameWon();
		}
	}
}
