using UnityEngine;
using System.Collections;

// Determine the type of movement to simulate
public enum MoveBehavior : byte {Rotate, Rest, TrackPlayer };

public class MOVETEST : MonoBehaviour {

	public MoveBehavior movement;
	public float speed = 180.0f;
	public AudioSource thurSound;

	private Quaternion towardsPlayer;
	private int ticker=0;
	private GameObject player;
	private float distanceToPlayer=0;
	private bool tooClose=false;


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
		Vector3 playerPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;

		// Move based on the movement type and speed selected
		switch (movement) {

			case MoveBehavior.Rotate:
			// Looks around idly. Should probably rename this.
				moveRotate ();
				break;
			case MoveBehavior.Rest:
			// Do nothing
				moveRest ();
				break;
			case MoveBehavior.TrackPlayer:
			// Look directly at player
				getPlayerPosition ();
				trackPlayer ();
				break;

			default: break; 
		}
		distanceToPlayer = Vector3.Distance (playerPosition,transform.position);
		// Switch to TrackPlayer if you are very close to enemy. Variable tooClose prevents triggering this multiple times 
		if (distanceToPlayer < 0.8f && !tooClose) {
			print ("touching enemy");
			movement = MoveBehavior.TrackPlayer;
			tooClose = true;
			if (!thurSound.isPlaying) {
				//audioSource.PlayOneShot (thurSound, 0.7f);
				thurSound.Play();
			}
			StartCoroutine (DelayEnemy ());
		}
		// Increment the timer
		ticker ++;
	}
	private IEnumerator DelayEnemy(){
		yield return new WaitForSeconds(5f);
		tooClose = false;
		movement = MoveBehavior.Rotate;
	}

	void getPlayerPosition(){
		//if (Time.frameCount % 30 == 0) {
			player = GameObject.FindGameObjectWithTag ("Player");
			towardsPlayer = Quaternion.LookRotation (player.transform.position - transform.position);
		//}
	}

	// When alerted, should use rotate towards for immediate head turn. Bad for continuous follow since its choppy. Use Slerp for gradual tracking/ returning to uninterested state.
	void trackPlayer(){
		transform.rotation =  Quaternion.Slerp(transform.rotation, towardsPlayer, 0.2f);
		//transform.rotation= towardsPlayer;

	}

	

	// Looks around randomly and returns to original look place.
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
