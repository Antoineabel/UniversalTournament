using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SelectSpaceShip : MonoBehaviour {

    private int m_iCompteur;
    private GameObject m_goShip;

    public GameObject [] m_goTabShip;

	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
    void Update()
    {
        if (m_goShip == null)
        {
            InstantateShip();
            Debug.Log(" Après 1ere instantiate " + m_goShip.name);
        }

        else
        {
            if (m_goShip.name != m_goTabShip[m_iCompteur].name + "(Clone)")
            {
                Debug.Log("Sinon  m_goShip.name" + m_goShip.name);
                Debug.Log("Sinon  tab name" + m_goTabShip[m_iCompteur].name);
                Destroy(m_goShip.gameObject);
                InstantateShip();
            }
        }
        SendShipToShipManager();
    }

    private void InstantateShip()
    {
        m_goShip = Instantiate(m_goTabShip[m_iCompteur], transform.position, transform.rotation) as GameObject;
        m_goShip.transform.Rotate(Time.deltaTime, 0, 0);
        m_goShip.transform.localScale = new Vector3(10, 10, 10);
        
    }

    public void NextShip()
    {
        if (m_iCompteur == m_goTabShip.Length-1)
            m_iCompteur = 0;
        else
            m_iCompteur++;
    }

    public void PreviousShip()
    {
        if (m_iCompteur == 0)
            m_iCompteur = m_goTabShip.Length - 1;
        else
            m_iCompteur--;
    }

    public void SendShipToShipManager()
    {
        GameObject.Find("ShipManager").GetComponent<ShipManager>().SetShipPrefab(m_goTabShip[m_iCompteur]);
    }
}
