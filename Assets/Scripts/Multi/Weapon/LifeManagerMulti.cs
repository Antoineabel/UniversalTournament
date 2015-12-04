using UnityEngine;
using System.Collections;

public class LifeManagerMulti : MonoBehaviour
{

    public float m_fLife;
    public GameObject m_goParticuleEffectExplosion;

    public bool m_bBouclierIsActive;
    public float m_fValeurMaxBouclier;
    public float m_fBouclier;

    // Use this for initialization
    void Start()
    {
        m_fBouclier = m_fValeurMaxBouclier;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Player")
        {
            Debug.Log("Vie : " + m_fLife.ToString());
            Debug.Log("Bouclier : " + m_fBouclier.ToString());
        }

        DesactiverBouclier();
        RegenerationBouclier();
    }

    public void MinusLife(float _fWeaponPower)
    {
        if (m_bBouclierIsActive)
        {
            if ((m_fBouclier - _fWeaponPower) <= 0f)
            {
                m_fBouclier = 0f;
            }
            else
            {
                m_fBouclier -= _fWeaponPower;
            }
        }
        else
        {
            if ((m_fLife - _fWeaponPower) <= 0f)
            {
                m_fLife = 0f;
				Debug.Log("Je suis mort"+name);
                GameObject goParticule;
                goParticule = Instantiate(m_goParticuleEffectExplosion, this.transform.position, this.transform.rotation) as GameObject;

                Destroy(goParticule, 5.0f);
                Destroy(this.gameObject);
            }
            else
            {
                if (gameObject.tag == "Player")
                {
                   // Camera.main.GetComponent<CameraShake>().PlayShake();
                }
				Debug.Log("Je perd de la vie"+name);
                m_fLife -= _fWeaponPower;
            }
        }
    }

    public void RegenerationBouclier()
    {
        if (m_bBouclierIsActive)
        {
            if (m_fBouclier < m_fValeurMaxBouclier)
            {
                if (m_fBouclier > 0f)
                {
                    m_fBouclier += Time.deltaTime;
                }
            }
        }
    }

    public void DesactiverBouclier()
    {
        if (m_fBouclier <= 0)
        {
            m_bBouclierIsActive = false;
        }
    }

    public void ActiverBouclier()
    {
        m_bBouclierIsActive = true;
    }
}
