using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AsteroidSpawn : NetworkBehaviour
{

    private string m_sSceneMode;
    public GameObject []m_goAsteroids;
    public GameObject m_goAsteroidTrou;

//	[SerializeField]
    private int m_iNbAsteroid = 50;
	private float angle ;
    private Vector3[] m_v3Positions;

    // Use this for initialization
    void Start()
    {
		angle = (180 / m_iNbAsteroid) * Mathf.Deg2Rad;

        m_sSceneMode = Application.loadedLevelName.Split('_')[1];

        m_v3Positions = new Vector3[m_iNbAsteroid];
        int iNbAsteBuildt = 0;

        while (iNbAsteBuildt < m_iNbAsteroid)
        {
//			float x = gameObject.transform.position.x + 80*Random.Range(1.5f,2.5f)*Mathf.Cos( 45*Mathf.Deg2Rad + angle * iNbAsteBuildt / m_iNbAsteroid );
//			float z = gameObject.transform.position.z + 80*Random.Range(1.5f,2.5f)*Mathf.Sin( 45*Mathf.Deg2Rad + angle * iNbAsteBuildt / m_iNbAsteroid );
            Vector3 temp = new Vector3(Random.Range(-100, 100), Random.Range(-50, 30), Random.Range(-80, 80));
//			Vector3 temp = new Vector3(x, Random.Range(-50, 30),z);
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
                TransmitAsteroides();
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
    void TransmitAsteroides()
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
			GameObject goAsteroidtemp = Instantiate(m_goAsteroids[Random.Range(0,m_goAsteroids.Length)], m_v3Positions[i], new Quaternion(Random.Range(-60, 60), Random.Range(-40, 20), Random.Range(-60, 70), 1)) as GameObject;
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
