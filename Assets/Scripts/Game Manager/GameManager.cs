using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private int m_iNumberOfEnnemies;
	private int m_iNumberOfAllies;

	// Use this for initialization
	void Start () {
		m_iNumberOfEnnemies = GameObject.FindGameObjectsWithTag ("Ennemy").Length;
		Debug.Log (m_iNumberOfEnnemies);
		m_iNumberOfAllies = GameObject.FindGameObjectsWithTag ("Player").Length + GameObject.FindGameObjectsWithTag ("Ally").Length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DecreaseNumberOfEnnemies()
	{
		m_iNumberOfEnnemies--;
		if (m_iNumberOfEnnemies <= 0)
			gameObject.GetComponent<GameOverManager> ().Victory ();
		Debug.Log ("Number of Ennemies: "+m_iNumberOfEnnemies);
	}

	public void DecreaseNumberOfAllies()
	{
		m_iNumberOfAllies--;
		if (m_iNumberOfAllies <= 0)
			gameObject.GetComponent<GameOverManager> ().GameOver ();
		Debug.Log ("Number of Allies: "+m_iNumberOfAllies);
	}
}
