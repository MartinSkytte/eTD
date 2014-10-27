using UnityEngine;
using System.Collections;

//using UnityEngine;
//using System.Collections;

public class Block : MonoBehaviour {
	
	//Position of the block
	public int ID;
	public int x;
	public int y;
	
	private Vector3 myScale;
	private Vector3 newScale;
	private float startTime;
	
	public static Transform select;
	public static Transform moveTo;

	public bool readyToMove;
	public bool fallEffect;

	void Start(){
		myScale = transform.localScale;
		newScale = Vector3.zero;
		startTime = Time.time;
		if(fallEffect){
			StartCoroutine("fallDown");
		}
	}
	
	void Update(){
		if(!fallEffect && Time.time-startTime<3){//After block spawn, the grow effect will last no more than 3 seconds
			if(!fallEffect)transform.localScale = Vector3.Lerp(newScale,myScale, (Time.time-startTime)/2);
			if(fallEffect)transform.localScale = Vector3.Lerp(newScale,myScale, (Time.time-startTime)*5); newScale = transform.localScale;
		}

	}
	
	void OnMouseOver(){
		transform.localScale = new Vector3(myScale.x+0.2f,myScale.y+0.2f,myScale.z+0.2f);
		//Select objects
		if(Input.GetMouseButtonDown(0)){
			if(!select){
				select = transform;
			}
			else if(select != transform && !moveTo ){ //If one block selected, then check if we are not selecting same block again, select second block
				moveTo = transform;
			}
		}
	}
	
	void OnMouseExit(){
		transform.localScale = myScale;
	}



	public IEnumerator moveDown(int rows){
		Match3 match = GameObject.FindObjectOfType (typeof(Match3)) as Match3;
		Vector3 lastPos = transform.position;
		Vector3 newPos = new Vector3 (transform.position.x, transform.position.y - rows, transform.position.z);
		float time = 0;

		while(time<1){
			time += Time.deltaTime;
			transform.position = Vector3.Lerp (lastPos, newPos, time);
			yield return null;
		}

		
	}


	public IEnumerator destroyBlock(){
		Vector3 lastPos = transform.localScale;
		float time = 0;
		
		while(time<1){
			time += Time.deltaTime;
			transform.localScale = Vector3.Lerp (lastPos, Vector3.zero, time);
			yield return null;
		}

		Destroy (gameObject);
		
		
	}

	public IEnumerator fallDown(){
		//Match3 match;
		Block[] allb = FindObjectsOfType(typeof(Block)) as Block[];
		Vector3 newPos = transform.position;
		Vector3 lastPos = new Vector3 (transform.position.x, transform.position.y +15, transform.position.z);
		float time = 0;
		
		while(time<1){
			time += Time.deltaTime;
			transform.position = Vector3.Lerp (lastPos, newPos, time);
			yield return null;
		}



	}

}
