using UnityEngine;
using System.Collections;

public class CollisionsScript : MonoBehaviour {

	private float m_fCollideForce = 500;
    private string [] m_sSceneMode;

	// Use this for initialization
	void Start () {
        m_sSceneMode = Application.loadedLevelName.Split('_');
        Debug.LogError("J'ai un collision script " + this.transform.gameObject.name);
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
                if (this.gameObject.GetComponent<LifeManager>())
                {
                    Debug.LogError("J'ai été touché " + this.transform.gameObject.name);
                    if (m_sSceneMode[1] == "Solo")
                    {
                        LiveLost(_cCollider.gameObject.GetComponent<PuissanceProjectile>().m_fPuissance);
                    }
                    else
                    {
                        Debug.LogError("J'ai été touché en multi");
                        LiveLost(_cCollider.gameObject.GetComponent<PuissanceProjectileMulti>().m_fPuissance);
                    }
                }
                else
                {
                    Debug.LogError("J'ai été touché par balles et j'ai pas de lifemanager");
                }
            break;

        case "Object":
            gameObject.GetComponent<Rigidbody>().AddForce(_cCollider.contacts[0].normal * m_fCollideForce);
            //			LiveLost(_cCollider.relativeVelocity.magnitude);
            LiveLost(m_fCollideForce / 10);
            break;

        case "Player":
            if (tag == "Ennemy")
                LiveLost(GetComponent<LifeManager>().m_fLife);
            break;
        case "Ennemy":
            if (tag == "Player")
                LiveLost(GetComponent<LifeManager>().m_fLife);
            break;
        case "Laser":
            if (this.gameObject.GetComponent<LifeManager>())
            {
                if (m_sSceneMode[1] == "Multi")
                {
                    LiveLost(_cCollider.gameObject.GetComponent<PuissanceLaserMulti>().m_fPuissance);
                }
            }
            break;
		}
	}


	void LiveLost(float _fDamage)
	{
		Debug.Log(gameObject.name + " perd de la vie !");
		GetComponent<LifeManager> ().MinusLife(_fDamage);
	}
}
