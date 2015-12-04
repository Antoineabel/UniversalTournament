using UnityEngine;
using System.Collections;

public class CollisionsScript : MonoBehaviour {

	private float m_fCollideForce = 500;
    private string[] m_sSceneMode;
	// Use this for initialization
	void Start () {
        m_sSceneMode = Application.loadedLevelName.Split('_');
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision _cCollider)
	{
		string sTag=_cCollider.gameObject.tag;
		switch(sTag)
		{
		case "SimpleShoot":
            if(this.gameObject.GetComponent<LifeManager>())
                {
                    if (m_sSceneMode[1] == "Solo")
                    {
                        LiveLost(_cCollider.gameObject.GetComponent<PuissanceProjectile>().m_fPuissance);
                    }
                    else
                    {
                        LiveLost(_cCollider.gameObject.GetComponent<PuissanceProjectileMulti>().m_fPuissance);

                    }
                }
			break;
			
		case "Object":
			gameObject.GetComponent<Rigidbody> ().AddForce (_cCollider.contacts [0].normal * m_fCollideForce);
//			LiveLost(_cCollider.relativeVelocity.magnitude);
			LiveLost(m_fCollideForce/10);
			break;
			
		case "Player":
			if (tag =="Ennemy")
				LiveLost(GetComponent<LifeManager> ().m_fLife);
			break;
		case "Ennemy":
			if (tag == "Player" )
				LiveLost(GetComponent<LifeManager> ().m_fLife);
			break;
		}
	}


	void LiveLost(float _fDamage)
	{
		Debug.Log(gameObject.name + " perd de la vie !"+_fDamage+" Pour etre exacte");
		if (m_sSceneMode [1] == "Solo") {
			GetComponent<LifeManager> ().MinusLife (_fDamage);
		} else {
			GetComponent<LifeManagerMulti> ().MinusLife (_fDamage);
		}
	}
}
