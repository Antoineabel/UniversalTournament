using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PuissanceProjectileMulti : NetworkBehaviour
{
    public float m_fPuissance; // a passer en private une fois la valeur determinee
    public float m_fSpeed; // a passer en private une fois la valeur determinee

    [SyncVar]
    public Quaternion SyncRotation;

    private Transform myTransform;

    private string m_sSceneMode;

    // Use this for initialization
    void Start()
    {
        m_sSceneMode = Application.loadedLevelName.Split('_')[1];
        if (m_sSceneMode == "Multi")
        {
            myTransform = GetComponent<Transform>();
            myTransform.rotation = SyncRotation;
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * m_fSpeed);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision _cCollision)
    {
        Debug.Log(_cCollision.gameObject.tag);
        if (_cCollision.gameObject.CompareTag("Player"))
        {
            LiveLost(_cCollision.gameObject);
        }
        Destroy(this.gameObject, 2.0f);
    }

    void LiveLost(GameObject _goDamaged)
    {
        Debug.Log(_goDamaged.name + " perd de la vie !");
        _goDamaged.transform.GetChild(1).transform.GetChild(0).GetComponent<LifeManager>().MinusLife(m_fPuissance);
    }
   }
