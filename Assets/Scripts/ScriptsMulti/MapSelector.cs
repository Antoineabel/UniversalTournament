using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class MapSelector : MonoBehaviour {
	
	private int m_iCompteur;
    private Object m_oScene;

	public RawImage m_riVisuelMap;
	public Texture [] m_tTabVisuelMap;
	
	public Object [] m_oTabMap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
		if (m_riVisuelMap.texture == null)
		{
			ChangeTextureMap();
		}
		
		else
		{
			if (m_riVisuelMap.texture != m_tTabVisuelMap[m_iCompteur])
			{
				ChangeTextureMap();
			}
		}
	}
	
	private void ChangeTextureMap()
	{
        m_riVisuelMap.texture = m_tTabVisuelMap[m_iCompteur];
        SendMapToServer();
	}
	
	public void NextMap()
	{
		if (m_iCompteur == m_oTabMap.Length-1)
			m_iCompteur = 0;
		else
			m_iCompteur++;
	}
	
	public void PreviousMap()
	{
		if (m_iCompteur == 0)
			m_iCompteur = m_oTabMap.Length - 1;
		else
			m_iCompteur--;
	}
	
	public void SendMapToServer()
	{
        m_oScene = m_oTabMap[m_iCompteur];
        GameObject.Find("NetworkSettings").GetComponent<NetworkSettingsManager>().SetMap(m_oScene);
	}
}
