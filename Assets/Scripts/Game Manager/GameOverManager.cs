using UnityEngine;
using System.Collections;


public class GameOverManager : MonoBehaviour {

	public Canvas m_cCanvas;
	private Animator m_anim;

	void Awake() {
		m_anim = m_cCanvas.gameObject.GetComponent <Animator> ();
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
