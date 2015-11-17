using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LaserSaccadeScript : NetworkBehaviour
{
    public Rigidbody m_rProjectile;
    public float m_fSpeed; // a passer en private une fois la valeur determinee
    public float m_fSecondsUntilDestroy; // a passer en private une fois la valeur determinee

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetButton("Fire1")
            && Application.loadedLevelName != "MenuSolo"
            && Application.loadedLevelName != "MenuMulti")
        {
            if (Application.loadedLevelName == "gameMulti")
                CmdShootMulti();
            else
                Shoot();
        }
   	}

	public void Shoot()
	{
        Debug.Log("Shoot Solo");
		Rigidbody rProjectile_ = Instantiate(m_rProjectile, transform.position, transform.rotation) as Rigidbody;
        GameObject goParent_ = GameObject.FindGameObjectWithTag("Player");
        rProjectile_.velocity = transform.TransformDirection(Vector3.forward * m_fSpeed) + goParent_.GetComponent<Rigidbody>().velocity;
        Destroy(rProjectile_.gameObject, m_fSecondsUntilDestroy);
	}


    //[Client]
    void TransmitShoot()
    {
        CmdShootMulti();
    }

    //[Command]
    public void CmdShootMulti()
    {
        Debug.Log("Shoot Multi");
        Rigidbody rProjectile_ = Instantiate(m_rProjectile, transform.position, transform.rotation) as Rigidbody;
        GameObject goParent_ = GameObject.FindGameObjectWithTag("Player");
        rProjectile_.velocity = transform.TransformDirection(Vector3.forward * m_fSpeed) + goParent_.GetComponent<Rigidbody>().velocity;
        NetworkServer.Spawn(rProjectile_.gameObject);
        Destroy(rProjectile_.gameObject, m_fSecondsUntilDestroy);
    }
}
