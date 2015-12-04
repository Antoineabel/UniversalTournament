using UnityEngine;
using System.Collections;

public class rotateAst : MonoBehaviour {
    public float vitesseRot=1;
   // public Vector3 pos = new Vector3(0, 0, 0);
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.RotateAroundLocal(new Vector3(0, 0, 1), Time.deltaTime * vitesseRot);
      //  gameObject.transform.Rotate(new Vector3(0, 1, 0)* Time.deltaTime * vitesseRot);
    }

    public void setSpeed(float s)
    {
        vitesseRot = s;
    }
}
