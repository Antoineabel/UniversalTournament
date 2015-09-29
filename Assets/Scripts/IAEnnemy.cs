using UnityEngine;
using System.Collections;

public class IAEnnemy : MonoBehaviour {

	//------------Variables----------------//
	public Transform target;
	public int moveSpeed;
	public float rotationSpeed;
	public int maxdistance;
	private Transform myTransform;
	//------------------------------------//    
/*	public float speed ;
	private RaycastHit hit;
	public float rayDistance=5.0f;*/

	void Awake()
	{
		myTransform = transform;
	}
	
	void Start ()
	{

	}
	
	
	void Update ()
	{
		//Vector3 lookDirection =(target.position - myTransform.position).normalized;

		if (Vector3.Distance (target.position, myTransform.position) < maxdistance) 
		{
			//Move towards target
			transform.LookAt (target.position);     
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

		}
	/*	transform.Translate (Vector3.forward * Time.deltaTime * speed); 

		if (Physics.Raycast (myTransform.position ,myTransform.forward,out hit,rayDistance) )
		    {
			lookDirection+= hit.normal;
		}*/
}
}





	