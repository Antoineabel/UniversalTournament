using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour
{

    public float m_fLife;
	public float m_fMaxLife;
    public GameObject m_goParticuleEffectExplosion;

    public bool m_bBouclierIsActive;
    public float m_fValeurMaxBouclier;
    public float m_fBouclier;

    // Use this for initialization
    void Start()
    {
		//m_fMaxLife = m_fMaxLife;
        //m_fBouclier = m_fValeurMaxBouclier;
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
			if ((m_fBouclier - _fWeaponPower) >=m_fValeurMaxBouclier)
			{
				m_fBouclier = m_fValeurMaxBouclier;
			}
            if ((m_fBouclier - _fWeaponPower) <= 0f)
            {
                m_fBouclier = 0f;
                GameObject.Find("GameManager").GetComponent<GameManager>().m_txtBouclierText.text = "Bouclier: " + Mathf.Round(m_fBouclier).ToString();
            }
            else
            {
                m_fBouclier -= _fWeaponPower;
            }
        }
        else
        {
			if ((m_fLife - _fWeaponPower) >= m_fMaxLife)
			{
				m_fLife = m_fMaxLife;
				ActiverBouclier();
			}
            if ((m_fLife - _fWeaponPower) <= 0f)
            {
                m_fLife = 0f;
                GameObject.Find("GameManager").GetComponent<GameManager>().m_txtLifeText.text = "Life: " + Mathf.Round(m_fLife).ToString();

                GameObject goParticule;
                goParticule = Instantiate(m_goParticuleEffectExplosion, this.transform.position, this.transform.rotation) as GameObject;

                if (gameObject.tag == "Ennemy")
                    GameObject.Find("GameManager").GetComponent<GameManager>().DecreaseNumberOfEnnemies();
                if (gameObject.tag == "Player" || gameObject.tag == "Ally")
                    GameObject.Find("GameManager").GetComponent<GameManager>().DecreaseNumberOfAllies();

                Destroy(goParticule, 5.0f);
                Destroy(this.gameObject);
            }
            else
            {
                if (gameObject.tag == "Player")
                {
                   // Camera.main.GetComponent<CameraShake>().PlayShake();
                }
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
		m_fBouclier = 1;
	}
}
