using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {
	int currentScore= 0;
	int currentLevel=1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		print (currentScore);
		if (currentLevel == 1) {
			
		}
	}

	public void scoreUp(){
		currentScore++;
	}
	public void levelUp(){
		currentLevel++;
	}

	public void resetScore(){
		print ("Score reset");
		currentScore = 0;
	}
}
