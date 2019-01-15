using UnityEngine;
using System.Collections;

public class ElevatorDoor : MonoBehaviour {

	public Transform player;
	private bool open;
	private bool moving;
	private float movePercent;
	private float moveDistance;

	public bool leftDoor;
	public AudioSource openSound;
	public AudioSource closeSound;

	// Use this for initialization
	void Start () {
		open = false;
		moving = false;
		moveDistance = 0.65f;
		movePercent = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown (0) && (transform.position - player.transform.position).magnitude < 5){
			print ("TOGGLING ELEVATOR");
			if ( !moving ){
				moving = true;
				open = !open;
				if(open) {
					if (!openSound.isPlaying) 
						openSound.Play();
				} else {
					if (!closeSound.isPlaying)
						closeSound.Play();
				}
			}
		}

		// If the door is not already moving
		if (moving) {
			// Transform the door towards or away from moveDistance in the y direction
			if(leftDoor)
				transform.localPosition = new Vector3(0, Mathf.Lerp(0, -moveDistance, movePercent), 0);
			else
				transform.localPosition = new Vector3(0, Mathf.Lerp(0, moveDistance, movePercent), 0);

			// Increment (or decrement) the percentage the door has moved to open/ close
			movePercent += (open)? .01667f : -.01667f;

			// If the move percentage passes 1 or 0, stop it from moving
			if(movePercent <= 0 || movePercent >= 1)
				moving = false;
		}
	}
}
