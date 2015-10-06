using UnityEngine;
using System.Collections;


public class GAMEOVERYEAAAAAH : MonoBehaviour {

	public GameOverManager gameOverManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump"))
			gameOverManager.GameOver ();
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Poyo !");
		gameOverManager.GameOver ();
	}

}
