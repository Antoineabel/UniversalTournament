using UnityEngine;
using System.Collections;

public class CapturePoints : MonoBehaviour
{
    public float m_fCompteur;
    public int m_iNombreVaisseaux;
    public bool m_bIsCaptured;
    public bool m_bIsBeingCaptured;

    // Use this for initialization
    void Start()
    {
        m_bIsCaptured = false;
        m_bIsBeingCaptured = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_fCompteur);
        if(!m_bIsBeingCaptured && !m_bIsCaptured)
        {
            if (m_fCompteur > 0f)
            {
                m_fCompteur--;
            }
        }
    }

    void OnTriggerStay(Collider _cCollider)
    {
        if (_cCollider.gameObject.tag == "Player")
        {
            if (m_bIsCaptured)
            {
                Debug.Log("Zone capturee !");
                m_bIsBeingCaptured = false;   
            }
            else
            {
                if (m_fCompteur < 100.0f)
                {
                    m_fCompteur++;
                }
                else
                {
                    m_bIsCaptured = true;
                    m_fCompteur = 100.0f;
                }
            }
        }
    }

    void OnTriggerExit(Collider _cCollider)
    {
        if (_cCollider.gameObject.tag == "Player")
        {
            m_iNombreVaisseaux--;
            if (m_iNombreVaisseaux <= 0)
            {
                if (m_fCompteur < 100.0f)
                {
                    if (m_fCompteur > 0.0f)
                    {
                        m_bIsBeingCaptured = false;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider _cCollider)
    {
        if (_cCollider.gameObject.tag == "Player")
        {
            m_iNombreVaisseaux++;
            if(!m_bIsCaptured)
            {
                m_bIsBeingCaptured = true;
            }
        }
    }
}