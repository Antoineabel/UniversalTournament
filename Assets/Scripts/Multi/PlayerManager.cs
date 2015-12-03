using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerManager : NetworkBehaviour {
    private GameObject m_goShip;
    private Transform myTransform;

	// Use this for initialization
    void Start()
    {
        myTransform = GetComponent<Transform>();
//        m_goShip = Instantiate(GameObject.Find("NetworkSettings").GetComponent<NetworkSettingsManager>().GetShipPrefab(), myTransform.position, myTransform.rotation) as GameObject;
        //NetworkServer.Spawn(m_goShip);
        m_goShip.transform.GetChild(0).GetComponent<ShipData>().SetIFIsLocalPlayer(isLocalPlayer);
        m_goShip.transform.SetParent(myTransform);
		
        if (isLocalPlayer) {
			myTransform.FindChild ("Camera").GetComponent<Camera> ().enabled = true;
		} else {
			m_goShip.tag = "Ennemy";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
