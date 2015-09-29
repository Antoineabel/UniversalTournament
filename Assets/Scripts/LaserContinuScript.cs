using UnityEngine;
using System.Collections;

public class LaserContinuScript : MonoBehaviour
{
    private LineRenderer m_lrLine;
    private Light m_lLight;

    public static float m_fPuissance; // a passer en private une fois la valeur determinee

    // Use this for initialization
    void Start ()
    {
        m_lrLine = gameObject.GetComponent<LineRenderer>();
        m_lLight = gameObject.GetComponent<Light>();

        m_lrLine.enabled = false;
        m_lLight.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetButtonDown("Fire2"))
        {
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
	}

    IEnumerator FireLaser()
    {
        m_lrLine.enabled = true;
        m_lLight.enabled = true;

        while (Input.GetButton("Fire2"))
        {
            m_lrLine.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time);

            Ray rRay = new Ray(transform.position, transform.forward);
            RaycastHit rhHit;

            m_lrLine.SetPosition(0, rRay.origin);

            if (Physics.Raycast(rRay, out rhHit, 100))
            {
                m_lrLine.SetPosition(1, rhHit.point);
                if(rhHit.rigidbody)
                {
                    // vie -= degats;
                    rhHit.rigidbody.AddForceAtPosition(transform.forward * 50, rhHit.point);
                }
            }
            else
            {
                m_lrLine.SetPosition(1, rRay.GetPoint(100));
            }
            yield return null;
        }
        m_lrLine.enabled = false;
        m_lLight.enabled = false;
    }

    static public float getPuissance()
    {
        return m_fPuissance;
    }
}
