using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {
	public GameObject player;
	RaycastHit hit;
	Ray ray;
	static int defaultMask = 1 << 0; // bitmask that basically just means layer #0.
	static int lookMask = 1 << 10; // bitmask for lookable objects
	static int finalMask = defaultMask | lookMask;
	public float sphereRadius = 0.5f;
	public float maxDistance = 5.0f;

	private Vector3 direction;
	public Vector3 screenCenter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		screenCenter = new Vector3 (Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);
		ray=Camera.main.ScreenPointToRay(screenCenter);

		if (Physics.Raycast(ray,out hit, 10 , finalMask )) {
			LookObjectHandling (hit);
			// Find the line from the camera to the mouse position.
			Vector3 incomingVec = hit.point - Camera.main.transform.position;
			
			// Use the point's normal to calculate the reflection vector.
		    Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
			//print ("DETECTED");
			// Draw lines to show the incoming "beam" and the reflection.
			Debug.DrawLine(player.transform.position, hit.point, Color.red, 0.01f);
			Debug.DrawRay(hit.point, reflectVec, Color.green);
		}

	}

	void LookObjectHandling(RaycastHit thing){
		GameObject LookedObject = thing.transform.gameObject;
		int lookObjectLayer = 10;
		if (LookedObject.layer == lookObjectLayer) {
			LookObject objectScript = LookedObject.GetComponent<LookObject> ();
			objectScript.changeColor ();
		}
	}

}
