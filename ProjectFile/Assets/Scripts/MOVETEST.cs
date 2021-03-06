﻿using UnityEngine;
using System.Collections;

// Determine the type of movement to simulate
public enum MoveBehavior : byte { Strafe, WashingMachine, Rotate, Rest };

public class MOVETEST : MonoBehaviour {

	public MoveBehavior movement;
	public float speed = 5.0f;

	private int ticker=0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// Move based on the movement type and speed selected
		switch (movement) {
			case MoveBehavior.Strafe:
				moveStrafe ();
				break;
			case MoveBehavior.WashingMachine:
				moveWashingMachine ();
				break;
			case MoveBehavior.Rotate:
				moveRotate ();
				break;
			case MoveBehavior.Rest:
				moveRest ();
				break;
			default: break; 
		}

		// Increment the timer
		ticker ++;
	}

	
	// Move back and forth
	void moveStrafe(){
		transform.Translate (0, 0, speed * Time.deltaTime);
		if (ticker > 60) {
			speed *= -1;
			ticker=0;
		}
	}
	

	// Rotate in place back and forth
	void moveWashingMachine(){
		transform.Rotate(Vector3.up, speed);
		if (ticker > 60) {
			speed *= -1;
			ticker=0;
		}
	}
	

	// Rotate in place in one direction
	void moveRotate(){
		transform.Rotate(Vector3.up, speed);
	}


	// Stay still
	void moveRest(){
		
	}

}
