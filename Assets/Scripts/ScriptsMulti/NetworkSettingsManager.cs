using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSettingsManager : MonoBehaviour {

    private static GameObject m_goShipPrefab;
    private static Object m_oMap;
    private int cpt;

	// Use this for initialization
	void Start () {
        cpt = 0;
        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        {
            cpt++;
            //Debug.Log("n°"+cpt+" Je suis un GameObject et je m'appelle "+go.name);
        }
        //Debug.Log("Il y a " + cpt + " GameObject actifs");
        cpt = 0;

        if (Application.loadedLevelName == "NetSettings")
            GameObject.Find("GO_NetworkManager").GetComponent<NetworkManager>().ServerChangeScene(m_oMap.name);
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

    public void SetMap(Object _oMap)
    {
        m_oMap = _oMap;
    }

    public Object GetMap()
    {
        return m_oMap;
    }
}
