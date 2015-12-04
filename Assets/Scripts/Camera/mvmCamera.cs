using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mvmCamera : MonoBehaviour {

	private Image m_imgCockpit;

	private float m_fDistOrigin;
	private float m_fHeightOrigin;
	private float m_fAlphaOld;
	private float m_fDistNew;
	private float m_fHeightNew;

	private bool m_bIsFirstPers;
	
	// Use this for initialization
	void Start () { 
        m_imgCockpit = GameObject.Find("Cockpit").GetComponent<Image>();
		m_bIsFirstPers = false;

		m_fDistNew = m_fDistOrigin = GetComponent<SmoothFollow> ().GetDistance ();
		m_fHeightNew = m_fHeightOrigin = GetComponent<SmoothFollow> ().GetHeight ();
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
			if (m_fDistNew > 13)
				m_fDistNew -= Time.deltaTime*6;
			if (m_fHeightNew > 0.5f)
				m_fHeightNew -= Time.deltaTime*2;

			if (m_fDistNew <=13 && m_fHeightNew<=0.5f && m_fAlphaOld<=255)
				m_imgCockpit.canvasRenderer.SetAlpha(m_fAlphaOld+=Time.deltaTime);
		}
		else 
		{
			if (m_fAlphaOld>=0)
				m_imgCockpit.canvasRenderer.SetAlpha(m_fAlphaOld-=Time.deltaTime*10);
			if (m_fDistNew < m_fDistOrigin)
				m_fDistNew+=Time.deltaTime*12;
			if (m_fHeightNew < m_fHeightOrigin)
				m_fHeightNew+=Time.deltaTime*4;
		}
		GetComponent<SmoothFollow> ().SetDistanceAndHeight (m_fDistNew, m_fHeightNew);
	}
}
	 