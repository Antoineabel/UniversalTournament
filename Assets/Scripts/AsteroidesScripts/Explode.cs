﻿using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour
{
    //public GameObject explosion;
    public GameObject m1;
    public GameObject m2;
    public GameObject particuleFX;
    // Use this for initialization
    void Start () {
		/*;*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision col){
			print("test");
		if(col.gameObject.tag=="Asteroid")
		{
			//GameObject expl = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            GameObject expl1 = Instantiate(m1, transform.position, transform.rotation) as GameObject;
            GameObject expl2 = Instantiate(m2, transform.position, transform.rotation) as GameObject;

            ContactPoint contact = col.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            GameObject partFX = Instantiate(particuleFX, pos,rot) as GameObject;
            Destroy(gameObject);
            Destroy(expl1, 5);
            Destroy(expl2, 5);
            Destroy(partFX, 3);
            //Destroy(col.gameObject);
        }
	}
}
