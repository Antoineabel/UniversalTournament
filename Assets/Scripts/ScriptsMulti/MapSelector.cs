using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class MapSelector : MonoBehaviour {
	
	private static int m_iCompteur;
    private string m_oScene;

	public RawImage m_riVisuelMap;
	public Texture [] m_tTabVisuelMap;

    public string[] m_oTabMap;

	// Use this for initialization
	void Start () {
        m_iCompteur = 0;
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
        Debug.Log("Le compteur est " + m_iCompteur + "les map sont" + m_oTabMap[0]+ " et " + m_oTabMap[1]);
        Debug.Log("LA MAP UTILE EST " + m_oScene);
        GameObject.Find("NetworkSettings").GetComponent<NetworkSettingsManager>().SetMap(m_oScene);
	}
}
