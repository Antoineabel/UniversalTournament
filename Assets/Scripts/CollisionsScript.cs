using UnityEngine;
using System.Collections;

public class CollisionsScript : MonoBehaviour {

	private float m_fCollideForce = 500;

	// Use this for initialization
	void Start () {
	
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
			LiveLost(_cCollider.gameObject.GetComponent<PuissanceProjectile>().m_fPuissance);
			break;
			
		case "Object":
			gameObject.GetComponent<Rigidbody> ().AddForce (_cCollider.contacts [0].normal * m_fCollideForce);
			LiveLost(_cCollider.relativeVelocity.magnitude);
			break;
			
		case "Player":
		case "Ennemy":
			if (tag == "Player" || tag =="Ennemy")
				LiveLost(GetComponent<LifeManager> ().m_fLife);
			break;
		}
	}


	void LiveLost(float _fDamage)
	{
		Debug.Log(gameObject.name + " perd de la vie !");
		GetComponent<LifeManager> ().MinusLife(_fDamage);
	}
}
