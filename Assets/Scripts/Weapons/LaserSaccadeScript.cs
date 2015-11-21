using UnityEngine;
using System.Collections;

public class LaserSaccadeScript : MonoBehaviour
{
    public GameObject m_rProjectile;
    public float m_fSpeed; // a passer en private une fois la valeur determinee
    public float m_fSecondsUntilDestroy; // a passer en private une fois la valeur determinee

    private float m_fTime;

    private Transform m_tOriginShoot;

    private string[] m_sSceneMode;

    // Use this for initialization
    void Start ()
    {
        m_sSceneMode = Application.loadedLevelName.Split('_');
        m_fTime = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetButton("Fire1")
            && m_sSceneMode[0] != "Menu")
        {
            if (m_fTime > 0.1f)
            {
                m_fTime = 0;
                Shoot();
            }
            m_fTime += Time.deltaTime;
        }
   	}

	public void Shoot()
	{
		GameObject rProjectile_ = Instantiate(m_rProjectile, transform.position, transform.rotation) as GameObject;
		rProjectile_.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * m_fSpeed);
		Destroy(rProjectile_, m_fSecondsUntilDestroy);
	}
}
