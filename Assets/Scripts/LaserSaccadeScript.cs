using UnityEngine;
using System.Collections;

public class LaserSaccadeScript : MonoBehaviour
{
    public Rigidbody m_rProjectile;

    public static float m_fPuissance; // a passer en private une fois la valeur determinee
    public float m_fSpeed; // a passer en private une fois la valeur determinee
    public float m_fTimebeforedestruction; // a passer en private une fois la valeur determinee

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1"))
        {
            Rigidbody rProjectile_ = Instantiate(m_rProjectile, transform.position, transform.rotation) as Rigidbody;
            rProjectile_.velocity = transform.TransformDirection(Vector3.forward * m_fSpeed);
            Destroy(rProjectile_.gameObject, m_fTimebeforedestruction);
        }
   	}

    static public float getPuissance()
    {
        return m_fPuissance;
    }
}
