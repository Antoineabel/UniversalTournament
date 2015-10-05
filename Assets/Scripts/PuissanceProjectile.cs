using UnityEngine;
using System.Collections;

public class PuissanceProjectile : MonoBehaviour
{
    public float m_fPuissance; // a passer en private une fois la valeur determinee

    // Use this for initialization
    void Start()
    {

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
