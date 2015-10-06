using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

    public float m_fLife;
    public GameObject m_goParticuleEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

//        Debug.Log(m_fLife);

	}
    void OnCollisionEnter (Collision _cCollider)
    {
        Debug.Log(_cCollider.gameObject.name);
        if (_cCollider.gameObject.tag=="SimpleShoot")
        {
            MinusLifeSimpleShoot(_cCollider.gameObject.GetComponent<PuissanceProjectile>().m_fPuissance);
        }
    }

    public void MinusLifeSimpleShoot(float _fWeaponPower)
    {
        if ((m_fLife - _fWeaponPower) <= 0f)
        {
            GameObject goParticule;
            goParticule = Instantiate(m_goParticuleEffect, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(goParticule, 15.0f);
            Destroy(this.gameObject);            
        }
        else
        {
            m_fLife -= _fWeaponPower;
        }
    }

    public void MinusLifeLaserShoot(float _fWeaponPower)
    {
        if ((m_fLife - _fWeaponPower) <= 0f)
        {
            GameObject goParticule;
            goParticule = Instantiate(m_goParticuleEffect, this.transform.position, this.transform.rotation) as GameObject;
            Destroy(goParticule, 15.0f);
            Destroy(this.gameObject);
        }
        else
        {
            m_fLife -= _fWeaponPower;
        }
    }
}
