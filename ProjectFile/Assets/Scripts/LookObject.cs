using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookObject : MonoBehaviour {
	Renderer rend;
	bool lookedAt=false;
	bool finished=false;
	float fillSeconds=2;
	float decay=1.8f;
	float r=0;
	float g=0;
	float b=0;
	float currR=0;
	float currG=0;
	float currB=0;
	float redIncrement=0;
	float greenIncrement=0;
	float blueIncrement=0;


	Game_Manager managerScript;


	// Use this for initialization
	void Start () {
		rend= GetComponent<Renderer>();
		managerScript = GameObject.Find("GameManager").GetComponent<Game_Manager> ();
		r = rend.material.color.r;
		g = rend.material.color.g;
		b = rend.material.color.b;
		currR = r;
		currG = g;
		currB = b;
		// fills to green in 2 seconds
		redIncrement = ((r) / (fillSeconds*60));
		if (redIncrement == 0) {
			redIncrement = (1 / (fillSeconds*60));
		}
		greenIncrement = ((1 - g) / (fillSeconds*60));
		blueIncrement = ((b) / (fillSeconds*60));
		if (blueIncrement == 0) {
			blueIncrement = (1 / (fillSeconds*60));
		}
	}
	
	// Update is called once per frame
	// Moves color towards original color when not looked at. 
	void Update () {
		boundColors ();
		if (currG < 1 || currR>0 || currB>0) {
			rend.material.color = new Color (currR, currG, currB);
			if (!lookedAt && ((currG >0))) {
				currG -= greenIncrement*decay;
				currR += redIncrement*decay;
				currB += blueIncrement*decay;
			}
			lookedAt = false;
		} 
		if (!finished && currR <= 0 && currG >= 1 && currB <= 0) {
			managerScript.scoreUp ();
			finished = true;
		}

	}

	// Moves color towards green when looked at. Green = (0,1,0)
	public void changeColor(){
		print ("HEIKFJKELJKFW");
		lookedAt = true;
		//if (g < 1) {
		//	g += 0.01f;
		//}
		if (currG < 1 || currR>0 || currB>0) {
			currR -= redIncrement;
			currG += greenIncrement;
			currB -= blueIncrement;
		}
	}

	void boundColors(){
		if (currR > r) {
			currR = r;
		}
		if (currG < g) {
			currG = g;
		}
		if (currB > b) {
			currB = b;
		}
			
	}
}
