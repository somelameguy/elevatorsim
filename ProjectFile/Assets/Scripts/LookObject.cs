using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookObject : MonoBehaviour {
	Renderer rend;
	bool lookedAt=false;
	bool finished=false;
	float r=0;
	float g=0;
	float b=0;

	Game_Manager managerScript;


	// Use this for initialization
	void Start () {
		rend= GetComponent<Renderer>();
		managerScript = GameObject.Find("GameManager").GetComponent<Game_Manager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (g < 1) {
			rend.material.color = new Color (r, g, b);
			if (!lookedAt && (g > 0)) {
				g -= 0.008f;
			}
			lookedAt = false;
		} 
		if (!finished && g >= 1) {
			managerScript.scoreUp ();
			finished = true;
		}

	}

	public void changeColor(){
		lookedAt = true;
		if (g < 1) {
			g += 0.01f;
		}
	}
}
