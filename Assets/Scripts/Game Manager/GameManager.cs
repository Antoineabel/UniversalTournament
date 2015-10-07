using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	private int m_iNumberOfEnnemies;
	private int m_iNumberOfAllies;
	private bool m_bIsLost;
	private bool m_bIsWin;

	public GameObject m_goPlayer;
	public Text m_txtLifeText;

	// Use this for initialization
	void Start () {
		m_iNumberOfEnnemies = GameObject.FindGameObjectsWithTag ("Ennemy").Length;
		m_iNumberOfAllies = GameObject.FindGameObjectsWithTag ("Player").Length + GameObject.FindGameObjectsWithTag ("Ally").Length;

		m_bIsLost = m_bIsWin = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (m_goPlayer)
			m_txtLifeText.text = "Life: "+Mathf.Round(m_goPlayer.GetComponent<LifeManager> ().m_fLife).ToString();
	}

	public bool GetIfIsLost()
	{
		return m_bIsLost;
	}

	public bool GetIfIsWin()
	{
		return m_bIsWin;
	}

	public void DecreaseNumberOfEnnemies()
	{
		m_iNumberOfEnnemies--;
		if (m_iNumberOfEnnemies <= 0) 
		{
			gameObject.GetComponent<GameOverManager> ().Victory ();
			m_bIsWin = true;
		}
		Debug.Log ("Number of Ennemies: "+m_iNumberOfEnnemies);
	}

	public void DecreaseNumberOfAllies()
	{
		m_iNumberOfAllies--;
		if (m_iNumberOfAllies <= 0) 
		{
			gameObject.GetComponent<GameOverManager> ().GameOver ();
			m_bIsLost = true;
		}
		Debug.Log ("Number of Allies: "+m_iNumberOfAllies);
	}
}
