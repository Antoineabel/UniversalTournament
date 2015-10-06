using UnityEngine;
using System.Collections;


public class GameOverManager : MonoBehaviour {
	
	Animator m_anim;
	
	void Awake() {
		m_anim = GetComponent <Animator> ();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void GameOver() {
		m_anim.SetTrigger ("GameOver");
	}
}
