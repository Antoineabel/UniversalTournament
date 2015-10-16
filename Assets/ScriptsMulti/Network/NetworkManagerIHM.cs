using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class NetworkManagerIHM : MonoBehaviour {

    private string m_sIPServer;
    private string m_sIPClient;
    private string m_sSceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetServerIP(Text _txtIPServer)
    {
        m_sIPServer = _txtIPServer.text;
    }

    public void SetClientIP(Text _txtIPClient)
    {
        m_sIPClient = _txtIPClient.text;
    }

    public void SetSceneName(string _sSceneName)
    {
        Debug.Log("net set scene name" + m_sSceneName);
        m_sSceneName = _sSceneName;
    }

    public void StartServer()
    {
        GameObject.Find("GO_NetworkManager").GetComponent<NetworkManager>().StopHost();
        GameObject.Find("GO_NetworkManager").GetComponent<NetworkManager>().networkAddress = m_sIPServer;
        Debug.Log("Je lance le serveur à l'adresse: " + m_sIPServer);
        GameObject.Find("GO_NetworkManager").GetComponent<NetworkManager>().StartHost();
    }

    public void JoinServer()
    {
        GameObject.Find("GO_NetworkManager").GetComponent<NetworkManager>().networkAddress = m_sIPClient;
        GameObject.Find("GO_NetworkManager").GetComponent<NetworkManager>().StartClient();
    }
}
