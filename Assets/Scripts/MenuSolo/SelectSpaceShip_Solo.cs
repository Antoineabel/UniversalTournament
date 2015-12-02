using UnityEngine;
using System.Collections;

public class SelectSpaceShip_Solo : MonoBehaviour
{
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
        }
        else
        {
            if (m_goShip.name != m_goTabShip[m_iCompteur].name + "(Clone)")
            {
                Destroy(m_goShip.gameObject);
                InstantateShip();
            }
        }
        m_goShip.transform.Rotate(0, Time.deltaTime * 100, 0);
        SendShipToGameSettingsManager();
    }

    private void InstantateShip()
    {
        m_goShip = Instantiate(m_goTabShip[m_iCompteur], transform.position, transform.rotation) as GameObject;
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

    public void SendShipToGameSettingsManager()
    {
        GameObject.Find("GameSettings").GetComponent<GameSettingsManager>().SetShipPrefab(m_goTabShip[m_iCompteur]);
    }
}
