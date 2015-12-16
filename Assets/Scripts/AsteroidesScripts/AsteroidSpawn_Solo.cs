using UnityEngine;
using System.Collections;

public class AsteroidSpawn_Solo : MonoBehaviour
{
    public GameObject []m_goAsteroids;
    public GameObject m_goAsteroidTrou;

    private int m_iNbAsteroid = 150;
//	private float angle ;
    private Vector3[] m_v3Positions;
    private float m_fDistanceBetweenAsteroides;

    // Use this for initialization
    void Start()
    {
        m_fDistanceBetweenAsteroides = 6f;
        //		angle = (180 / m_iNbAsteroid) * Mathf.Deg2Rad;

        m_v3Positions = new Vector3[m_iNbAsteroid];
        int iNbAsteBuildt = 0;

        while (iNbAsteBuildt < m_iNbAsteroid)
        {
//			float x = gameObject.transform.position.x + 80*Random.Range(1.5f,2.5f)*Mathf.Cos( 45*Mathf.Deg2Rad + angle * iNbAsteBuildt / m_iNbAsteroid );
//			float z = gameObject.transform.position.z + 80*Random.Range(1.5f,2.5f)*Mathf.Sin( 45*Mathf.Deg2Rad + angle * iNbAsteBuildt / m_iNbAsteroid );
            Vector3 temp = new Vector3(Random.Range(-400, 400), Random.Range(-200, 200), Random.Range(-300, 300));
//			Vector3 temp = new Vector3(x, Random.Range(-50, 30),z);
			if (CheckDistance(temp))
            {
                m_v3Positions[iNbAsteBuildt] = temp;
                iNbAsteBuildt++;
            }
        }
        InstantiateAsteroid();
        InstantiateAsteroidTrou();
    }

    private bool CheckDistance(Vector3 _v3Pos)
    {
        for (int i = 0; i < m_v3Positions.Length; i++)
        {
            if (Vector3.Distance(_v3Pos, m_v3Positions[i]) < m_fDistanceBetweenAsteroides)
            {
                return false;
            }
        }
        return true;
    }

    private void InstantiateAsteroid()
    {
        for (int i = 0; i < m_v3Positions.Length; i++)
        {
			GameObject goAsteroidtemp = Instantiate(m_goAsteroids[Random.Range(0,m_goAsteroids.Length)], m_v3Positions[i], new Quaternion(Random.Range(-60, 60), Random.Range(-40, 20), Random.Range(-60, 70), 1)) as GameObject;
            goAsteroidtemp.transform.parent = transform;
        }
    }


    private void InstantiateAsteroidTrou()
    {
		Instantiate (m_goAsteroidTrou, new Vector3 (-350, 0, 0), Quaternion.identity);
	}

}
