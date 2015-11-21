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
        Destroy(this.gameObject);
    }
   }
