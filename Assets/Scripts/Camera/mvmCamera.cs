using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mvmCamera : MonoBehaviour {

	public Image m_imgCockpit;

	private float m_fDistOld;
	private float m_fHeightOld;
	private float m_fAlphaOld;

	private bool m_bIsFirstPers;
	
	// Use this for initialization
	void Start () {
		m_bIsFirstPers = false;
		m_fDistOld = GetComponent<SmoothFollow> ().GetDistance ();
		m_fHeightOld = GetComponent<SmoothFollow> ().GetHeight ();
		m_fAlphaOld = m_imgCockpit.canvasRenderer.GetAlpha ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1)){
			m_bIsFirstPers = true;
		}
		if(Input.GetKeyDown(KeyCode.F3)){
			m_bIsFirstPers = false;
		}

		if (m_bIsFirstPers) 
		{
			if (m_fDistOld > 13)
				m_fDistOld -= Time.deltaTime*3;
			if (m_fHeightOld > 0.5f)
				m_fHeightOld -= Time.deltaTime;

			if (m_fDistOld <=13 && m_fHeightOld<=0.5f && m_fAlphaOld<=255)
				m_imgCockpit.canvasRenderer.SetAlpha(m_fAlphaOld+=Time.deltaTime);
		}
		else 
		{
			if (m_fDistOld < 20)
				m_fDistOld+=Time.deltaTime*3;
			if (m_fHeightOld < 3)
				m_fHeightOld+=Time.deltaTime;

			if (m_fAlphaOld>=0)
				m_imgCockpit.canvasRenderer.SetAlpha(m_fAlphaOld-=Time.deltaTime*10);
		}
		GetComponent<SmoothFollow> ().SetDistanceAndHeight (m_fDistOld, m_fHeightOld);
	}
}
	 