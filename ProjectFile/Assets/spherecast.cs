using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spherecast : MonoBehaviour {
	public float distanceToObstacle;
	public Vector3 p1;
	public RaycastHit hit;
	public Vector3 cameraForward;
	public bool playerSeeEnemy;
	private float sphereCastWidth = 0.1f;
	private int layerMask = 1 << 8; // bitmask that basically just means layer #8.
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		p1 = transform.position;
		distanceToObstacle = 0;
		cameraForward = Camera.main.transform.forward;
		// Cast a sphere forward relative to camera. 
		if (Physics.SphereCast (p1, sphereCastWidth, cameraForward, out hit, 10, layerMask)) {
			distanceToObstacle = hit.distance;
			//print ("Player Sees Eyes");
			playerSeeEnemy = true;
			//print (isPlayerInTrouble ());

		} 
		else {
			distanceToObstacle = 10;
			playerSeeEnemy = false;
		}
	}

	public bool isPlayerInTrouble(){
		try{
			GameObject enemySeen=GameObject.FindGameObjectWithTag ("Player");
			// Gets if enemy sees player. Used in the conditional that detects if player sees enemy.
			enemySeen = hit.collider.gameObject;
			//Get parent of object seen, since parent holds script.
			Raycast_Enemy enemyscript = enemySeen.transform.parent.GetComponent<Raycast_Enemy> ();
			return (enemyscript.getDetectedPlayer ());
		}
		catch (NullReferenceException e){
			return (false);
		}

	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (p1 + cameraForward * distanceToObstacle, sphereCastWidth);
		//Gizmos.DrawWireSphere (p1 + transform.forward * distanceToObstacle, 0.5f);
	}
}
