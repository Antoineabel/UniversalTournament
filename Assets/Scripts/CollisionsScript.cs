﻿using UnityEngine;
using System.Collections;

public class CollisionsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag != "Bullet") 
		{
			if (gameObject.tag == "Player")
				LiveLost ();
			gameObject.GetComponent<Rigidbody> ().AddForce (other.contacts [0].normal * 1000);
		}
	}


	void LiveLost()
	{
		Debug.LogWarning (gameObject.name + " perd de la vie !");
	}
}
