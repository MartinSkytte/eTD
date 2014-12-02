using UnityEngine;
using System.Collections;

public class Match3 : MonoBehaviour {
	
	//Swap speed
	public float swapSpeed = 10.0f;

	//Board
	public int[,] board;

	public int goal;

	//Board blocks
	public Transform[] blocks;
	
	//Check if swap effect ended
	private bool swapEffect, paused, newgame;
	
	private Color blockColor = Color.white;

	public int blocksDestroyed = 0;

	public int score=0;

	public int credits = 0;

	public string finalID;

	public float scale = 0;
	


	public float timeLeft;
	
	void Start(){
		GameEventManager.GameWon += GameWon;
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;
		board = new int[10,10];
		GenBoard();

		paused = true;

		newgame = true;
		GameStart ();
		Debug.Log ("goal:"+  goal);
	}


	int getGoal()
	{
		return goal;
	}

	void GameWon(){
		Debug.Log("Game Won called");
		GUIManager.endMatchTutorial ();
		CreditManager();
		timeLeft = 0;
		renderer.enabled = false;
		enabled = false;
	}

	void CreditManager(){
		scale = (float)score / (float)goal;
		Debug.Log ("scale:" + scale + "score:" + score + "goal:" + goal);
		if (goal != 0) {
						if (scale > 2 || scale <= 0) {
								credits += 0;		
						} else {
								credits += (int)Mathf.FloorToInt (100 * (2 - 2*scale));
								Debug.Log ("credits:" + credits);
						}
			GUIManager.SetCredits (credits);
			newgame = true;
			}
	}

	void GameOver (){
		Debug.Log("Game Over called");
		GUIManager.endMatchTutorial ();
		CreditManager();
		timeLeft = 0;
		renderer.enabled = true;
		enabled = true;
	}
	
	private void GameStart (){
		if (newgame == true) {
						paused = false;
						renderer.enabled = true;
						enabled = true;
						score = 0;
						GUIManager.SetCurrentNumber (score);
						goal = Random.Range (10, 100);
						GUIManager.SetGoalNumber (goal);
						newgame = false;
				}
	}

	void Update(){

		if (Input.GetMouseButtonDown(1)) {
			score = 0;
			GUIManager.SetCurrentNumber(score);
			GameEventManager.TriggerGameOver();	
		}

		//Select block effect
		if(Block.select){
			if(blockColor == Color.white){
				blockColor = Block.select.gameObject.renderer.material.color;
			}
			//Change block color over time
			Block.select.gameObject.renderer.material.color = Color32.Lerp(blockColor, Color.black, Mathf.PingPong(Time.time,1));
		}
		
		if(Block.select && Block.moveTo){ //If 2 blocks are selected, what we need more?
			
			//Check if they are near each other
			if(CheckIfNear()==true){
				//Near
				if(!swapEffect){
					swapEffect = true;
					StartCoroutine(swapBlockEffect(true));//Swap their position and data using new swap effect, also check for match3
				}
			}
			else{
				//Not near
				Block.select.gameObject.renderer.material.color = blockColor;
				blockColor = Color.white;
				Block.select = null;
				Block.moveTo = null;
				//We can again select new blocks
			}
		}
		if (!paused) {
			timeLeft += Time.deltaTime;
			if (timeLeft < 0)timeLeft = 0;
			int timeInSeconds = (int)timeLeft;
		}
	}
	
	
	
	
	void GenBoard(){
		
		for(int x=0; x<board.GetLength(0); x++){
			for(int y=0; y<board.GetLength(1); y++){
				int randomNumber = Random.Range(0,4); //ID
				Transform obj = (Transform)Instantiate(blocks[randomNumber].transform, new Vector3(x,y,0), Quaternion.AngleAxis(270, Vector3.up));
				obj.parent = transform;
				Block b = obj.gameObject.AddComponent<Block>();
				//Set values
				b.ID = randomNumber;
				b.x =x;
				b.y =y;
				//Set ID in board at this position
				board[x,y] = randomNumber;
			}
		}
	}
	
	
	bool CheckIfNear(){
		Block sel = Block.select.gameObject.GetComponent<Block>();
		Block mov = Block.moveTo.gameObject.GetComponent<Block>();
		
		//Check if near
		if(sel.x-1 == mov.x && sel.y == mov.y){
			//Left
			return true;
		}
		if(sel.x+1 == mov.x && sel.y == mov.y){
			//Right
			return true;
		}
		if(sel.x == mov.x && sel.y+1 == mov.y){
			//Up
			return true;
		}
		if(sel.x == mov.x && sel.y-1 == mov.y){
			//Down
			return true;
		}
		Debug.Log("What are you trying to select!");
		return false;
	}
	
	
	
	bool CheckMatch(){
		//Check for any mistake in the board
		testBoard ();
		
		//Get all blocks in scene
		Block[] allb = FindObjectsOfType(typeof(Block)) as Block[];
		Block sel = Block.select.gameObject.GetComponent<Block> ();
		Block mov = Block.moveTo.gameObject.GetComponent<Block> ();
		
		
		
		//SELECTED BLOCK
		//Check how many blocks have same ID as our selected block(for each direction)
		int countU = 0; //Count Up
		int countD = 0; //Count Down
		int countL = 0; //Count Left
		int countR = 0; //Count RIght
		
		//Check how many same blocks have sam ID...
		//Left
		for(int l = sel.x-1; l>=0; l--){
			if(board[l,sel.y]==sel.ID){//If block have same ID
				countL++;
			}
			if(board[l,sel.y]!=sel.ID){//If block have same ID
				break;
			}
		}
		//Right
		for(int r = sel.x; r<board.GetLength(0); r++){
			if(board[r,sel.y]==sel.ID){//If block have same ID
				countR++;
			}
			if(board[r,sel.y]!=sel.ID){//If block have same ID
				break;
			}
		}
		//Down
		for(int d = sel.y-1; d>=0; d--){
			if(board[sel.x,d]==sel.ID){
				countD++;
			}
			if(board[sel.x,d]!=sel.ID){
				break;
			}
		}
		
		//Up
		for(int u = sel.y; u<board.GetLength(1); u++){
			if(board[sel.x,u]==sel.ID){
				countU++;
			}
			
			if(board[sel.x,u]!=sel.ID){
				break;
			}
		}
		
		//MOVE TO BLOCK
		int countUU = 0; //Count Up
		int countDD = 0; //Count Down
		int countLL = 0; //Count Left
		int countRR = 0; //Count RIght
		
		//Check how many same blocks have sam ID...
		//Left
		for(int l = mov.x-1; l>=0; l--){
			if(board[l,mov.y]==mov.ID){//If block have same ID
				countLL++;

			}
			if(board[l,mov.y]!=mov.ID){//If block have same ID
				break;
			}
		}
		//Right
		for(int r = mov.x; r<board.GetLength(0); r++){
			if(board[r,mov.y]==mov.ID){//If block have same ID
				countRR++;

			}
			if(board[r,mov.y]!=mov.ID){//If block have same ID
				break;
			}
		}
		//Down
		for(int d = mov.y-1; d>=0; d--){
			if(board[mov.x,d]==mov.ID){
				countDD++;

			}
			if(board[mov.x,d]!=mov.ID){
				break;
			}
		}
		
		//Up
		for(int u = mov.y; u<board.GetLength(1); u++){
			if(board[mov.x,u]==mov.ID){
				countUU++;
			}
			
			if(board[mov.x,u]!=mov.ID){
				break;
			}
		}
		
		
		
		
		//Check if there is 3+ match 
		if((countL+countR>=3 || countD+countU>=3) || (countLL+countRR>=3 || countDD+countUU>=3)){
			if(countL+countR>=3){
				//Destroy and mark empty block
				for(int cl = 0; cl<=countL; cl++){
					foreach(Block b in allb){
						if(b.x == sel.x-cl && b.y == sel.y){
							b.StartCoroutine("destroyBlock");
							board[b.x,b.y] += 500; //To mark empty block
						}
					}
				}
				for(int cr = 0; cr<countR; cr++){
					foreach(Block b in allb){
						if(b.x == sel.x+cr && b.y == sel.y){
							b.StartCoroutine("destroyBlock");
							board[b.x,b.y] += 500; //To mark empty block
						}
					}
				}
			}
			if(countD+countU>=3){
				for(int cd = 0; cd<=countD; cd++){
					foreach(Block blc in allb){
						if(blc.x == sel.x && blc.y == sel.y - cd){
							board[blc.x, blc.y] += 500;
							blc.StartCoroutine("destroyBlock");
						}
					}
				}
				for(int cu = 0; cu<countU; cu++){
					foreach(Block blc in allb){
						if(blc.x == sel.x && blc.y == sel.y+cu){
							board[blc.x, blc.y] += 500;
							blc.StartCoroutine("destroyBlock");
						}
					}
				}
			}
			
			
			
			if(countLL+countRR>=3){
				//Destroy and mark empty block
				for(int cl = 0; cl<=countLL; cl++){
					foreach(Block b in allb){
						if(b.x == mov.x-cl && b.y == mov.y){
							b.StartCoroutine("destroyBlock");
							board[b.x,b.y] += 500; //To mark empty block
						}
					}
				}
				for(int cr = 0; cr<countRR; cr++){

					foreach(Block b in allb){
						if(b.x == mov.x+cr && b.y == mov.y){
							b.StartCoroutine("destroyBlock");
							board[b.x,b.y] += 500; //To mark empty block
						}
					}
				}
			}
			if(countDD+countUU>=3){
				for(int cd = 0; cd<=countDD; cd++){
					foreach(Block blc in allb){
						if(blc.x == mov.x && blc.y == mov.y - cd){
							board[blc.x, blc.y] += 500;
							blc.StartCoroutine("destroyBlock");
						}
					}
				}
				for(int cu = 0; cu<countUU; cu++){
					foreach(Block blc in allb){
						if(blc.x == mov.x && blc.y == mov.y+cu){
							board[blc.x, blc.y] += 500;
							blc.StartCoroutine("destroyBlock");
						}
					}
				}
			}
			//Respawn blocks
			MoveY();
			//score = setScore (finalID, blocksDestroyed);
			GUIManager.SetCurrentNumber(score);
			if (score == goal || score > goal) {
				GameEventManager.TriggerGameOver();
			}
			//blocksDestroyed = 0;
			return true;
		}
		

		
		return false;
	}
	
	int setScore(string theID){//, int numBlocks){
		switch (theID) {
		case "0": //Red block
			score += 1;
			break;
		case "1": //Blue block
			score += 2;
			break;
		case "2": //Yellow block
			score += 3;
			break;
		case "3": //Green block
			score += 4;
			break;
		}
		return score;
	
	}
	//Move blocks down
	void MoveY(){
		Block[] allb = FindObjectsOfType(typeof(Block)) as Block[];
		int moveDownBy = 0; //How many times we need to move them down?
		
		for(int x=0; x<board.GetLength(0); x++){
			for(int y=board.GetLength(1)-1; y>=0; y--){//Start from top, go down
				if(board[x,y]>=500){//If we found empty block
					blocksDestroyed ++;
					string c =board[x,y].ToString();
					finalID = c[c.Length-1].ToString();
					setScore(finalID);
					foreach(Block b in allb){
						if(b.x == x && b.y > y){//Every block above this empty block will be marked
							b.readyToMove = true;//Mark this block as ready to move
							b.y -=1;//New block position

						}
					}
					
					moveDownBy++;//We move them one more time down
				}
			}
			
			//Find blocks we marked
			foreach(Block b in allb){
				if(b.readyToMove){
					b.StartCoroutine(b.moveDown(moveDownBy)); //Fall down effect
					b.readyToMove = false;//They will not fall again, now
					board[b.x,b.y] = b.ID;//New ID in board
				}
			}
			//Mark new empty blocks
			MarkEmpty (x,moveDownBy);
			//Reset
			moveDownBy = 0;
		}
		//Respawn blocks
		Respawn ();
	}
	
	//Mark new empty blocks
	void MarkEmpty(int x, int num){
		Block[] allb = FindObjectsOfType(typeof(Block)) as Block[];
		for(int i=0; i<num; i++){
			board[x,board.GetLength(1)-1-i] = 500;
		}
		
	}
	
	//Just spawn a new blocks at the empty position, but using fall effect
	void Respawn(){
		for(int x=0; x<board.GetLength(0); x++){
			for(int y=0; y<board.GetLength(1); y++){
				if(board[x,y]==500){
					int randomNumber = Random.Range(0,4); //ID
					Transform obj = (Transform)Instantiate(blocks[randomNumber].transform, new Vector3(x,y,0), Quaternion.AngleAxis(270, Vector3.up));
					obj.parent = transform;
					Block b = obj.gameObject.AddComponent<Block>();
					//Set values
					b.ID = randomNumber;
					b.x = x;
					b.y = y;
					//This block will fall down
					b.fallEffect = true;
					//Set ID in board at this position
					board[x,y] = randomNumber;
					
					
				}
			}
		}
	}
	
	
	//Check for any mistake in the board
	public void testBoard(){
		Block[] allb = FindObjectsOfType(typeof(Block)) as Block[];
		for(int x=0; x<board.GetLength(0); x++){
			for(int y=0; y<board.GetLength(1); y++){
				foreach(Block b in allb){
					if(b.x == x && b.y ==y){
						if(board[x,y]!=b.ID){//Found a mistake
							board[x,y] = b.ID;//Fix the mistake
						}
					}
				}
			}
		}
		
	}
	
	
	//Swap block effect
	//checkMatch = true - it will check for match3
	IEnumerator swapBlockEffect(bool match3){
		
		Block sel = Block.select.gameObject.GetComponent<Block>();
		Block mov = Block.moveTo.gameObject.GetComponent<Block>();
		
		Vector3 selTempPos = sel.transform.position;
		Vector3 movTempPos = mov.transform.position;
		
		float time = 0;
		
		while(time<1){
			time += Time.deltaTime * swapSpeed;
			sel.transform.position = Vector3.Lerp(selTempPos, movTempPos, time);
			mov.transform.position = Vector3.Lerp(movTempPos, selTempPos, time);
			yield return null;
		}
		
		int tempX = sel.x;
		int tempY = sel.y;
		
		//Swap data
		sel.x = mov.x;
		sel.y = mov.y;
		
		mov.x = tempX;
		mov.y = tempY;
		
		//Change ID in board
		board[sel.x,sel.y]=sel.ID;
		board[mov.x,mov.y]=mov.ID;
		
		
		//Do we want to run the code to check for match?
		if(match3 == true){
			//Check for match3
			if(CheckMatch()==true){
				
				//There is match
				swapEffect = false;//End effect
				Block.select.gameObject.renderer.material.color = blockColor;
				blockColor = Color.white;
				Block.select = null;
				Block.moveTo = null;
			}
			else{
				//There is no match, return them in their default position
				StartCoroutine(swapBlockEffect(false));//Swap their position and data using new swap effect, without checking for match3
				Block.select.gameObject.renderer.material.color = blockColor;
				blockColor = Color.white;
				Block.select = null;
				Block.moveTo = null;
			}
		}
		else{//We don't
			swapEffect = false; //End effect
		}
		
	}
	
}