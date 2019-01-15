using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour {
	Image circle; //Circle UI image
	GameObject player; //Get player object
	spherecast playerScript; //Initialize spherecast script that holds bool for eye contact
	bool isDamaged= false;
	float health=5;
	float currentHealth=5;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		circle = GetComponent<Image>();
		playerScript = player.GetComponent<spherecast> ();
	}
	
	// Update is called once per frame
	void Update () {
		isDamaged = playerScript.isPlayerInTrouble ();
		//print (isDamaged);
		if (currentHealth > 0 && isDamaged) {
			currentHealth -= Time.deltaTime;
			circle.fillAmount = currentHealth / health;
		}

	}
}
