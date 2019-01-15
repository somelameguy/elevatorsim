using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {
	int currentScore= 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		print (currentScore);
	}

	public void scoreUp(){
		currentScore++;
	}

	public void resetScore(){
		print ("Score reset");
		currentScore = 0;
	}
}
