using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AsteroidSpawn : NetworkBehaviour
{

    private string m_sSceneMode;
    public GameObject m_goAsteroid;
    public GameObject m_goAsteroidTrou;

    private int m_iNbAsteroid = 70;

    private Vector3[] m_v3Positions;

    // Use this for initialization
    void Start()
    {
        m_sSceneMode = Application.loadedLevelName.Split('_')[1];

        m_v3Positions = new Vector3[m_iNbAsteroid];
        int iNbAsteBuildt = 0;
        while (iNbAsteBuildt < m_iNbAsteroid)
        {
            Vector3 temp = new Vector3(Random.Range(-80, 80), Random.Range(-50, 30), Random.Range(-60, 70));
            if (CheckDistance(temp))
            {
                m_v3Positions[iNbAsteBuildt] = temp;
                iNbAsteBuildt++;
            }
        }
        if (m_sSceneMode == "Solo")
        {
            InstantiateAsteroid();
            InstantiateAsteroidTrou();
        }
        else
        {
            if (isServer)
            {
                TransmitShoot();
            }
        }
    }

    private bool CheckDistance(Vector3 _v3Pos)
    {
        for (int i = 0; i < m_v3Positions.Length; i++)
        {
            if (Vector3.Distance(_v3Pos, m_v3Positions[i]) < 5)
            {
                return false;
            }
        }
        return true;
    }
    [Client]
    void TransmitShoot()
    {
        CmdShootMulti();
    }

    [Command]
    public void CmdShootMulti()
    {
        InstantiateAsteroid();
        InstantiateAsteroidTrou();
    }
    private void InstantiateAsteroid()
    {
        for (int i = 0; i < m_v3Positions.Length; i++)
        {
            GameObject goAsteroidtemp = Instantiate(m_goAsteroid, m_v3Positions[i], new Quaternion(Random.Range(-60, 60), Random.Range(-40, 20), Random.Range(-60, 70), 1)) as GameObject;
            goAsteroidtemp.transform.parent = transform;
            if (m_sSceneMode == "Solo")
            {
                Destroy(goAsteroidtemp.GetComponent<NetworkTransform>());
                Destroy(goAsteroidtemp.GetComponent<NetworkIdentity>());
            }
            else
            {
                NetworkServer.Spawn(goAsteroidtemp);
            }
        }
    }


    private void InstantiateAsteroidTrou()
    {
        GameObject goAsteroidtemp = Instantiate(m_goAsteroidTrou, new Vector3(-145, 0, 0), Quaternion.identity) as GameObject;
        if (m_sSceneMode == "Solo")
        {
            Destroy(goAsteroidtemp.GetComponent<NetworkTransform>());
            Destroy(goAsteroidtemp.GetComponent<NetworkIdentity>());
        }
        else
        {
            NetworkServer.Spawn(goAsteroidtemp);
        }
    }

}
