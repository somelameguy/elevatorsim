﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	CharacterController cc;

	public GameObject playerCam;

	//----Movement Vars---
	float forwardSpeed; 
	float sideSpeed; 
	float speedMultiplier= 3.0f; 
	Vector3 speed;
	float gravity=0;
	float jumpSpeed=5.0f;

	//-----Rotation vars----
	float sideRotSpeed;
	float verticalRotSpeed;
	float verticalRotRange=60.0f;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
		// Screen.lockCursor = true; //Get rid of mouse on screen
		Cursor.lockState = UnityEngine.CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Movement values
		forwardSpeed = Input.GetAxis ("Vertical") * speedMultiplier;
		sideSpeed = Input.GetAxis ("Horizontal") * speedMultiplier;
		gravity += Physics.gravity.y * Time.deltaTime;

		//Rotation values
		sideRotSpeed = Input.GetAxis ("Mouse X") * speedMultiplier;
		verticalRotSpeed -= Input.GetAxis ("Mouse Y") *speedMultiplier;
		
		//Rotation 
		transform.Rotate (0, sideRotSpeed, 0);
		verticalRotSpeed = Mathf.Clamp (verticalRotSpeed, -verticalRotRange, verticalRotRange);
		playerCam.transform.localRotation=Quaternion.Euler (verticalRotSpeed, 0, 0);

		//Movement 
		speed= new Vector3(sideSpeed,gravity,forwardSpeed);
		speed = transform.rotation * speed;
		cc.Move (speed* Time.fixedDeltaTime);

		if (cc.isGrounded && Input.GetButtonDown ("Jump")) {
			gravity=jumpSpeed;
		}

		if (Input.GetMouseButtonDown (1)) {
			//print ("Pressed mouse button 1");
			if(Cursor.lockState == UnityEngine.CursorLockMode.Locked)
				Cursor.lockState = UnityEngine.CursorLockMode.None;
			else 
				Cursor.lockState = UnityEngine.CursorLockMode.Locked;
			Cursor.visible = !Cursor.visible;
		}

	}
}
