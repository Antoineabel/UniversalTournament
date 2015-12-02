using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LaserSaccadeScript : NetworkBehaviour
{
    public Rigidbody m_rProjectile;
    public float m_fSpeed; // a passer en private une fois la valeur determinee
    public float m_fSecondsUntilDestroy; // a passer en private une fois la valeur determinee

    private float m_fTime;
    private string[] m_sSceneMode;

    // Use this for initialization
    void Start ()
    {
        m_sSceneMode = Application.loadedLevelName.Split('_');
        m_fTime = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetButton("Fire1") 
            && m_sSceneMode[0] != "Menu")
        {
            if(m_fTime > 0.1f)
            {
                m_fTime = 0f;
                Shoot();
            }
            m_fTime += Time.deltaTime;
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
}
