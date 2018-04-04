using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_script : MonoBehaviour {
	public Texture2D sightIcon;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		float xMin = (Screen.width / 2) - (sightIcon.width / 2);
		float yMin = (Screen.height / 2) - (sightIcon.height / 2);
		GUI.DrawTexture (new Rect (xMin, yMin, sightIcon.width, sightIcon.height),sightIcon);
	
	}
}
