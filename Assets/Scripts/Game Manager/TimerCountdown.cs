using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerCountdown : MonoBehaviour {

	private float m_fTimeMax ;
	private float m_fMinutes;
	private float m_fSecondes;
	private string m_sDisplayedMinutes;
	private string m_sDisplayedSecondes;

	public Text m_txtTexte;
	public GameObject m_goGameOverScreen;

	// Use this for initialization
	void Start () {
		m_fTimeMax = 60 * 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_txtTexte) 
		{	
			m_fMinutes = Mathf.Floor (m_fTimeMax / 60);
			m_fSecondes = Mathf.Floor (m_fTimeMax -m_fMinutes * 60);
			m_sDisplayedMinutes = m_fMinutes.ToString();
			m_sDisplayedSecondes = m_fSecondes.ToString();

			if(m_fSecondes<10)
				m_sDisplayedSecondes = "0"+m_fSecondes;

			if (m_fMinutes<=0)
				m_sDisplayedMinutes="0";

			if (m_fTimeMax<=0)
				m_sDisplayedSecondes="00";

			m_txtTexte.text = m_sDisplayedMinutes + ":" + m_sDisplayedSecondes;

			if (m_fTimeMax <= 0) 
			{
				Debug.LogWarning ("End of the Game");
				m_goGameOverScreen.GetComponent<GameOverManager>().GameOver();
			} 
			else 
			{
				m_fTimeMax -= Time.deltaTime;
			}
		}
	}
}
