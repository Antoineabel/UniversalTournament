using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class NetworkManagerIHM : NetworkBehaviour {

    private string m_sIPServer;
    private string m_sIPClient;

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

    public void StartServer()
    {
        GetComponent<NetworkManager>().networkAddress = m_sIPServer;
        GetComponent<NetworkManager>().StartHost();
    }

    public void JoinServer()
    {
        GetComponent<NetworkManager>().networkAddress = m_sIPClient;
        GetComponent<NetworkManager>().StartClient();
    }
}
