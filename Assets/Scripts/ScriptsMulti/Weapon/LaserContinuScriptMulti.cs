using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LaserContinuScriptMulti : NetworkBehaviour
{
    private LineRenderer m_lrLine;
    private Light m_lLight;

    private string[] m_sSceneMode;

    public float m_fPuissance; // a passer en private une fois la valeur determinee
    public GameObject m_goParticuleEffect;

    private float m_fCylindreScaleY;
    public GameObject m_goLaserCylindre;

    private Transform m_tOriginShoot;
    private GameObject go; 
    // Use this for initialization
    void Start ()
    {
        m_tOriginShoot = transform;
        m_fCylindreScaleY = m_goLaserCylindre.transform.localScale.y;
        m_lrLine = gameObject.GetComponent<LineRenderer>();
        m_lLight = gameObject.GetComponent<Light>();

        m_lrLine.enabled = false;
        m_lLight.enabled = false;
        m_sSceneMode = Application.loadedLevelName.Split('_');
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_sSceneMode[0] != "Menu" )
        {
            if (!GameObject.Find("GameManager").GetComponent<PauseMenuScript>().GetIsPaused())
            {
                if (isLocalPlayer)
                {
                    if (Input.GetButton("Fire2") )
                    {
                        //Debug.Log("Laser continue sur joueur local");
                        //StopCoroutine("FireLaser");
                        //StartCoroutine("FireLaser");
                        Vector3 v3Pos = new Vector3(m_tOriginShoot.position.x, m_tOriginShoot.position.y, m_tOriginShoot.position.z - m_fCylindreScaleY );
                        if (go == null)
                        {
                            go = Instantiate(m_goLaserCylindre) as GameObject;
                        }
                        go.transform.localScale = new Vector3(1, m_fCylindreScaleY, 1);
                        
                        go.transform.rotation = new Quaternion(transform.forward.x, transform.forward.y, transform.forward.z, transform.rotation.w);
                        go.transform.position = v3Pos;

                        if (m_fCylindreScaleY < 100)
                        {
                            m_fCylindreScaleY++;
                        }
                    }
                }
            }
        }
	}

    IEnumerator FireLaser()
    {
        m_lrLine.enabled = true;
        m_lLight.enabled = true;

        while (Input.GetButton("Fire2") && !GameObject.Find("GameManager").GetComponent<PauseMenuScript>().GetIsPaused())
        {
            m_lrLine.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time);

            Ray rRay = new Ray(transform.position, transform.forward);
            RaycastHit rhHit;

            m_lrLine.SetPosition(0, rRay.origin);
            if (Physics.Raycast(rRay, out rhHit, 100))
            {
                m_lrLine.SetPosition(1, rhHit.point);
                if (rhHit.rigidbody)
                {
                    if (rhHit.rigidbody.tag != "SimpleShoot")
                    {
                        if (rhHit.rigidbody.gameObject.GetComponent<LifeManager>())
                        {
                            rhHit.rigidbody.gameObject.GetComponent<LifeManager>().MinusLife(m_fPuissance);
                            GameObject goParticule;
                            goParticule = Instantiate(m_goParticuleEffect, rhHit.transform.position, rhHit.transform.rotation) as GameObject;
                            Destroy(goParticule, 1.0f);
                        }
                        else
                        {
                            Debug.Log("No Life Manager");
                        }
                    }
                }
                else
                {

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
}
