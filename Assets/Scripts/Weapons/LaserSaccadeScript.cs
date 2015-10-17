using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LaserSaccadeScript : NetworkBehaviour
{
    public GameObject m_rProjectile;
    public float m_fSpeed; // a passer en private une fois la valeur determinee
    public float m_fSecondsUntilDestroy; // a passer en private une fois la valeur determinee

    private float m_fTime;

    private Transform m_tOriginShoot;

    [SyncVar]
    private Quaternion SyncRotation;

    private string[] m_sSceneMode;

    // Use this for initialization
    void Start ()
    {
        m_sSceneMode = Application.loadedLevelName.Split('_');
        m_fTime = 0;
        if (m_sSceneMode[1] != "Solo")
            m_tOriginShoot = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetButton("Fire1")
            && m_sSceneMode[0] != "Menu")
        {
            if (m_sSceneMode[1] == "Multi")
            {
                if(isLocalPlayer)
                {
                    if (m_fTime > 0.1f)
                    {
                        m_fTime = 0;
                        TransmitShoot();
                    }
                    m_fTime += Time.deltaTime;
                }
                
            }
            else
            {
                Shoot();
            }
        }
   	}

	public void Shoot()
	{
		GameObject rProjectile_ = Instantiate(m_rProjectile, transform.position, transform.rotation) as GameObject;
		rProjectile_.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * m_fSpeed);
		Destroy(rProjectile_, m_fSecondsUntilDestroy);
	}


    [Client]
    void TransmitShoot()
    {
        CmdShootMulti();
    }

    [Command]
    public void CmdShootMulti()
    {
        GameObject rProjectile_ = Instantiate(m_rProjectile, m_tOriginShoot.position, m_tOriginShoot.rotation) as GameObject;
        rProjectile_.GetComponent<PuissanceProjectile>().SyncRotation = m_tOriginShoot.rotation;
        NetworkServer.Spawn(rProjectile_);
        Destroy(rProjectile_.gameObject, m_fSecondsUntilDestroy);
    }
}
