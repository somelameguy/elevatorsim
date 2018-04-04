using UnityEngine;
using System.Collections;

public class Raycast_Enemy : MonoBehaviour {
	public GameObject player;

	int layerMask = 1 << 9; // bitmask that basically just means layer #x.
	public float distanceToObstacle;
	public Vector3 p1;
	public RaycastHit hit;
	public Vector3 enemyForward;
	private float sphereCastWidth = 0.1f;
	public bool detectedPlayer=false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenCenter = new Vector3 (Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);
		Vector3 forward = transform.TransformDirection (Vector3.forward);

		if (Physics.Raycast(player.transform.position,forward,out hit, Mathf.Infinity,layerMask)) {
			Debug.DrawLine(player.transform.position, hit.point, Color.red, 0.1f);
		}

		p1 = transform.position;
		distanceToObstacle = 0;
		enemyForward = transform.forward;
		// Cast a sphere forward relative to camera. 
		if (Physics.SphereCast (p1, sphereCastWidth, enemyForward, out hit, 10, layerMask)) {
			distanceToObstacle = hit.distance;
			detectedPlayer = true;
			print ("Enemy Sees Player Eyes");
		} 
		else {
			distanceToObstacle = 10;
			detectedPlayer = false;
		}

	}

	public bool getDetectedPlayer(){
		return detectedPlayer;
	}


	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (p1 + enemyForward * distanceToObstacle, sphereCastWidth);
		//Gizmos.DrawWireSphere (p1 + transform.forward * distanceToObstacle, 0.5f);
	}
}
