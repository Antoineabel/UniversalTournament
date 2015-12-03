﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PuissanceProjectileMulti : NetworkBehaviour
{
    public float m_fPuissance; // a passer en private une fois la valeur determinee
    public float m_fSpeed; // a passer en private une fois la valeur determinee

    [SyncVar]
    public Quaternion SyncRotation;

    private Transform myTransform;

    // Use this for initialization
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myTransform.rotation = SyncRotation;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision _cCollision)
    {
        //Debug.Log(_cCollision.gameObject.tag);
        if (_cCollision.gameObject.CompareTag("Player"))
        {
            LiveLost(_cCollision.gameObject);
        }
        Destroy(this.gameObject);
    }

    void LiveLost(GameObject _goDamaged)
    {
        Debug.Log(_goDamaged.name + " perd de la vie !");
        _goDamaged.GetComponent<LifeManager>().MinusLife(m_fPuissance);
    }

	public void ApplySpeed(Vector3 _v3Force)
	{
        GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * m_fSpeed) + _v3Force;
	}

}
