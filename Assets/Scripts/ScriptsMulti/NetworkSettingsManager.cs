using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSettingsManager : MonoBehaviour {

    private static GameObject m_goShipPrefab;
    private static string m_oMap;
    private static int cpt;

	// Use this for initialization
	void Start () {
        cpt = 0;
        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        {
            cpt++;
        }
        cpt = 0;

        if (Application.loadedLevelName == "NetSettings")
        {
            Debug.Log("je suis une bite" + m_oMap);
            GameObject.Find("GO_NetworkManager").GetComponent<NetworkManager>().ServerChangeScene(m_oMap);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetShipPrefab(GameObject _goShipPrefab)
    {
        m_goShipPrefab = _goShipPrefab;
    }

    public GameObject GetShipPrefab()
    {
        return m_goShipPrefab;
    }

    public void SetMap(string _oMap)
    {
        m_oMap = _oMap;
    }

    public string GetMap()
    {
        return m_oMap;
    }
}
