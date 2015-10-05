using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public float vitesseX = 5f;
	public float vitesseY = 15f;
	public float vitesseZ = 500f;

	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(vitesseX, vitesseY, vitesseZ)*Time.deltaTime);	
	}

}
