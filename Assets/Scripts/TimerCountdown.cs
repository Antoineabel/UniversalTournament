using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerCountdown : MonoBehaviour {

	private float m_fTimeMax = 301;
	public Text m_txtTexte;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (m_txtTexte) 
		{
			m_txtTexte.text = Mathf.Round ((m_fTimeMax / 60) % 60) + ":" + Mathf.Round (m_fTimeMax % 60);
			if (m_fTimeMax <= 0) 
			{
				Debug.LogWarning ("End of the Game");
			} 
			else 
			{
				m_fTimeMax -= Time.deltaTime;
			}
		}
	}
}
