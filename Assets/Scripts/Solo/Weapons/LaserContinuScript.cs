using UnityEngine;
using System.Collections;

public class LaserContinuScript : MonoBehaviour
{
    private LineRenderer m_lrLine;
    private Light m_lLight;
    private string[] m_sSceneMode;
    private AudioSource m_acFireSound;

    public float m_fPuissance; // a passer en private une fois la valeur determinee
	public float m_fDamageCoefficient = 0.5f; // In case if you want to apply damage to the repair ship's laser that is reduced. To keep between 0 and 1
	public GameObject m_goParticuleEffect;
	public bool m_bRepairLaser; //True if laser can repair

    // Use this for initialization
    void Start ()
    {
        m_lrLine = gameObject.GetComponent<LineRenderer>();
        m_lLight = gameObject.GetComponent<Light>();

        m_lrLine.enabled = false;
        m_lLight.enabled = false;

        m_acFireSound = GetComponent<AudioSource>();

        m_sSceneMode = Application.loadedLevelName.Split('_');
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_sSceneMode[0] != "Menu")
        {
            if (!GameObject.Find("GameManager").GetComponent<PauseMenuScript>().m_bIsPaused)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    StopCoroutine("FireLaser");
                    StartCoroutine("FireLaser");
                }
            }
        }
	}

    IEnumerator FireLaser()
    {
        m_lrLine.enabled = true;
        m_lLight.enabled = true;

        while (Input.GetButton("Fire2") && !GameObject.Find("GameManager").GetComponent<PauseMenuScript>().m_bIsPaused)
        {
            m_lrLine.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time);

            Ray rRay = new Ray(transform.position, transform.forward);
            RaycastHit rhHit;

            m_lrLine.SetPosition(0, rRay.origin);

            m_acFireSound.Play();

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

							if (rhHit.rigidbody.gameObject.tag == "Ennemy") //If your target is an enemy
							{
								if (!m_bRepairLaser)
									rhHit.rigidbody.gameObject.GetComponent<LifeManager>().MinusLife(m_fPuissance);
								else
									rhHit.rigidbody.gameObject.GetComponent<LifeManager>().MinusLife(m_fPuissance*m_fDamageCoefficient);
							}
							if (rhHit.rigidbody.gameObject.tag == "Ally" && m_bRepairLaser) //If your target is an ally and the laser is a repair laser
							{
								rhHit.rigidbody.gameObject.GetComponent<LifeManager>().MinusLife(-m_fPuissance); //Apply negative power, as
							}                            

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
