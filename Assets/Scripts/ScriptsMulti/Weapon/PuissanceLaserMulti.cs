using UnityEngine;
using System.Collections;

public class PuissanceLaserMulti : MonoBehaviour {

    public float m_fPuissance; // a passer en private une fois la valeur determinee
    public bool m_bHitSmth;
    public Vector3 m_v3ColisionPoint;

	// Use this for initialization
	void Start () {
        m_bHitSmth = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionStay(Collision other)
    {
        m_bHitSmth = true;
        Debug.Log("Je touche " + other.gameObject.name);
        m_v3ColisionPoint = other.contacts[0].point;

        if(other.gameObject.GetComponent<LifeManager>())
        {
            other.gameObject.GetComponent<LifeManager>().MinusLife(m_fPuissance);
        }

    }

    void OnCollisionExit(Collision other)
    {
        m_bHitSmth = false;
        Debug.LogError("Je ne touche plus " + other.gameObject.name);
    }

}
