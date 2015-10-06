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

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag != "Bullet" && other.gameObject.tag != "Untagged") 
		{
			if (gameObject.tag == "Player"  || gameObject.tag == "Ennemy")
				LiveLost ();
			gameObject.GetComponent<Rigidbody> ().AddForce (other.contacts [0].normal * m_fCollideForce);
		}
	}


	void LiveLost()
	{
		Debug.LogWarning (gameObject.name + " perd de la vie !");
		GetComponent<LifeManager> ().MinusLifeLaserShoot (500);
	}
}
