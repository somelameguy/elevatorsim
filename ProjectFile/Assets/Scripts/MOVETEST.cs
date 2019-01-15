using UnityEngine;
using System.Collections;

// Determine the type of movement to simulate
public enum MoveBehavior : byte { WashingMachine, Rotate, Rest, TrackPlayer };

public class MOVETEST : MonoBehaviour {

	public MoveBehavior movement;
	public float speed = 180.0f;
	private Quaternion towardsPlayer;
	private int ticker=0;
	private GameObject player;
	//int interval = 0;
	bool isCoroutineStarted = false;

	Quaternion randomLook;
	float randomLookTime;
	bool restingRotation = false;
	Quaternion restingQuaternion;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		towardsPlayer = Quaternion.LookRotation (player.transform.position - transform.position);
		randomLook= transform.rotation;
		randomLookTime = 0.2f;
		restingQuaternion = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		// Move based on the movement type and speed selected
		switch (movement) {
			case MoveBehavior.WashingMachine:
				moveWashingMachine ();
				break;
			case MoveBehavior.Rotate:
				moveRotate ();
				break;
			case MoveBehavior.Rest:
				moveRest ();
				break;
			case MoveBehavior.TrackPlayer:
				getPlayerPosition ();
				trackPlayer ();
				break;

			default: break; 
		}

		// Increment the timer
		ticker ++;
	}

	void getPlayerPosition(){
		//if (Time.frameCount % 30 == 0) {
			player = GameObject.FindGameObjectWithTag ("Player");
			towardsPlayer = Quaternion.LookRotation (player.transform.position - transform.position);
		//}
	}

	// When alerted, should use rotate towards for immediate head turn. Bad for continuous follow since its choppy. Use Slerp for gradual tracking/ returning to uninterested state.
	void trackPlayer(){
		//if (Time.frameCount % interval == 0){
		//transform.rotation =  Quaternion.Slerp(transform.rotation, towardsPlayer, Time.deltaTime * speed);
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, towardsPlayer, 5);
		transform.rotation= towardsPlayer;
		//}
	}

	
	// Move back and forth
	IEnumerator Idle(){
		isCoroutineStarted = true;
		float time = 3.0f;
		Quaternion startingRotation = transform.rotation;
		Quaternion random= Random.rotation;
		transform.rotation = Quaternion.Slerp (startingRotation, random, time);
		yield return new WaitForEndOfFrame();

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
		
		//transform.Rotate(new Vector3(random,random2,0), speed);
		transform.rotation = Quaternion.Slerp (transform.rotation, randomLook, randomLookTime);
		if ((ticker > 120) && !restingRotation) {
			restingQuaternion = transform.rotation;
			Quaternion randomQuaternionX = Quaternion.Euler (Random.Range (-10.0f, 10.0f), Random.Range (-50.0f, 50.0f), 0);
			randomLook = transform.rotation * randomQuaternionX; //Quaternion.Euler(transform.rotation + Random.Range(-10.0f,10.0f),Random.Range(-50.0f,50.0f),0);
			randomLookTime = Random.Range (0.01f, 0.2f);
			ticker = 0;
			restingRotation = true;
		} 
		else if ((ticker > 120) && restingRotation) {
			randomLook = restingQuaternion;
			ticker = 0;
			restingRotation = false;
		}
	}


	// Stay still
	void moveRest(){
		
	}
}
