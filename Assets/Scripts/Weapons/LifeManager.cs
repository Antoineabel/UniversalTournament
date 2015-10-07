using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

    public float m_fLife;
    public GameObject m_goParticuleEffectExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag=="Player")
        Debug.Log(m_fLife);
	}

    public void MinusLife(float _fWeaponPower)
    {
        if ((m_fLife - _fWeaponPower) <= 0f)
        {
            GameObject goParticule;
			goParticule = Instantiate(m_goParticuleEffectExplosion, this.transform.position, this.transform.rotation) as GameObject;

			if (gameObject.tag == "Ennemy")
				GameObject.Find("GameManager").GetComponent<GameManager>().DecreaseNumberOfEnnemies();
			if (gameObject.tag == "Player" || gameObject.tag == "Ally")
				GameObject.Find("GameManager").GetComponent<GameManager>().DecreaseNumberOfAllies();

            Destroy(goParticule, 5.0f);
            Destroy(this.gameObject);            
        }
        else
        {
            m_fLife -= _fWeaponPower;
        }
    }    
}
