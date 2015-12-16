using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceShip{
	[RequireComponent(typeof (SpaceShipController))]
	public class IAEnnemy : MonoBehaviour
	{
		// This script represents an AI 'pilot' capable of flying the plane towards a designated target.
		// It sends the equivalent of the inputs that a user would send to the Aeroplane controller.
		[SerializeField] private float m_RollSensitivity = .2f;         // How sensitively the AI applies the roll controls
		[SerializeField] private float m_PitchSensitivity = .5f;        // How sensitively the AI applies the pitch controls
		[SerializeField] private float m_LateralWanderDistance = 5;     // The amount that the plane can wander by when heading for a target
		[SerializeField] private float m_LateralWanderSpeed = 0.11f;    // The speed at which the plane will wander laterally
		[SerializeField] private float m_MaxClimbAngle = 45;            // The maximum angle that the AI will attempt to make plane can climb at
		[SerializeField] private float m_MaxRollAngle = 45;             // The maximum angle that the AI will attempt to u
		[SerializeField] private float m_SpeedEffect = 0.01f;           // This increases the effect of the controls based on the plane's speed.
		[SerializeField] private float m_TakeoffHeight = 20;            // the AI will fly straight and only pitch upwards until reaching this height
		//[SerializeField] private Transform m_Target;                    // the target to fly towards
		
		private SpaceShipController m_AeroplaneController;  // The aeroplane controller that is used to move the plane
		private float m_RandomPerlin;                       // Used for generating random point on perlin noise so that the plane will wander off path slightly
		private bool m_TakenOff;                            // Has the plane taken off yet
		//private Transform myTransform;
//	public Transform Target2; 
		private Transform CiblePlayer,CibleAlly,Cible,Asteroid,Ennemy,myTransform,CibleBaseEnnemy;  
	//	private Transform Cible; 
	//	private Transform Asteroid; 
	//	private Transform Ennemy; 
		//private Transform Ast;
		private GameObject Ast;
		private Vector3 directionAst;

		public Rigidbody m_rProjectile;
		bool m_chasse;
		
		public static float m_fPuissance; // a passer en private une fois la valeur determinee
		public float m_fSpeed; // a passer en private une fois la valeur determinee
		public float m_fTimebeforedestruction; // a passer en private une fois la valeur determinee
		public float thrust;
		public Rigidbody rb;
		RaycastHit hit;

		private void Start()
		{
			m_chasse=true;

            // get the reference to the aeroplane controller, so we can send move input to it and read its current state.
            m_AeroplaneController = GetComponent<SpaceShipController>();

            // pick a random perlin starting point for lateral wandering
            m_RandomPerlin = Random.Range(0f, 180f);
            CiblePlayer = GameObject.FindWithTag("Player").transform;
            CibleAlly = GameObject.FindWithTag("Allier").transform;
            CibleBaseEnnemy = GameObject.FindWithTag("BaseEnnemy").transform;
            Cible = GameObject.FindWithTag("Base").transform;
            //Asteroid=GameObject.FindGameObjectWithTag("Object");
            Ast = GameObject.FindWithTag("Base");
            Ennemy = GameObject.FindWithTag("Ennemy").transform;

            myTransform = transform;
            rb = GetComponent<Rigidbody>();
        }
		
		// reset the object to sensible values
		public void Reset()
		{
			m_TakenOff = false;
		}
		
		
		// fixed update is called in time with the physics system update
		private void FixedUpdate()
		{

				if (Vector3.Distance (CibleAlly.position, myTransform.position) < 100 && m_chasse==true) 
				{
					
					SetTarget (CibleAlly);
					eviterAsteroid ();
					eviterAllier (); 
					deplacement ();

				//m_SpeedEffect=10;
			//	m_fSpeed=5;

					
					if (Vector3.Distance (CibleAlly.position, myTransform.position) < 40) 
					{
					   
						
						rb.AddForce(0,0,30);
						shoot ();
					//	eviterAsteroid ();
						eviterAllier ();
			
						deplacement ();
					}
			
	/*				 if (Vector3.Distance (CibleAlly.position, myTransform.position) < 10) 
					{
						Debug.Log("test");
						m_chasse=false;
						SetTarget (Cible);
						eviterAsteroid ();  
						eviterAllier ();
						deplacement ();
						
					}*/
				}
			/*if (Vector3.Distance (CibleAlly.position, myTransform.position) > 30) {
				m_chasse=true;
				SetTarget(CibleAlly);
				eviterAsteroid ();  
				eviterAllier ();
				deplacement ();
				
			}*/
				else
				{
					//m_chasse=true;
					SetTarget(Cible);
					eviterAsteroid ();  
					eviterAllier ();
					deplacement ();
                    float distance = Vector3.Distance(Cible.position, myTransform.position);
                     if (distance < 100)
                     {
                    GetComponent<SpaceShipController>().m_MaxEnginePower = 40f;
                        SetTarget(CibleBaseEnnemy);
                        eviterAsteroid();
                        eviterAllier();
                        deplacement();
                    }

            }
           
        }
		
		
		// allows other scripts to set the plane's target
		public void SetTarget(Transform target)
		{
			Cible = target;
		}
		public void deplacement()
		{

			if (Cible != null)

			{	
				//eviter();
				//Debug.Log("entrer");
				Vector3 targetPos = Cible.position +
				transform.right*(Mathf.PerlinNoise(Time.time*m_LateralWanderSpeed, m_RandomPerlin)*2 - 1)*m_LateralWanderDistance;

				Vector3 localTarget = transform.InverseTransformPoint(targetPos);
				float targetAngleYaw = Mathf.Atan2(localTarget.x, localTarget.z);
				float targetAnglePitch = -Mathf.Atan2(localTarget.y, localTarget.z);

				targetAnglePitch = Mathf.Clamp(targetAnglePitch, -m_MaxClimbAngle*Mathf.Deg2Rad,m_MaxClimbAngle*Mathf.Deg2Rad);

				float changePitch = targetAnglePitch - m_AeroplaneController.PitchAngle;

				const float throttleInput = 0.5f;

				float pitchInput = changePitch*m_PitchSensitivity;

				float desiredRoll = Mathf.Clamp(targetAngleYaw, -m_MaxRollAngle*Mathf.Deg2Rad, m_MaxRollAngle*Mathf.Deg2Rad);
				float yawInput = 0;
				float rollInput = 0;
				if (!m_TakenOff)
				{

					if (m_AeroplaneController.Altitude > m_TakeoffHeight)
					{
						m_TakenOff = true;
					}
				}
				else
				{
					// now we have taken off to a safe height, we can use the rudder and ailerons to yaw and roll
					yawInput = targetAngleYaw;
					rollInput = -(m_AeroplaneController.RollAngle - desiredRoll)*m_RollSensitivity;
				}
				
				// adjust how fast the AI is changing the controls based on the speed. Faster speed = faster on the controls.
				float currentSpeedEffect = 1 + (m_AeroplaneController.ForwardSpeed*m_SpeedEffect);
				rollInput *= currentSpeedEffect;
				pitchInput *= currentSpeedEffect;
				yawInput *= currentSpeedEffect;
				
				// pass the current input to the plane (false = because AI never uses air brakes!)
				m_AeroplaneController.Move(rollInput, pitchInput, yawInput, throttleInput, false);
				
				
				
			}
			else 
			{
				// no target set, send zeroed input to the planeW
				m_AeroplaneController.Move(0,0,0,0,false);

			}
			/*if( Vector3.Distance (Target2.position, myTransform.position) < 40)
			{
				Debug.Log("asteroide");
				m_AeroplaneController.Move(0,0,0,0,false);
			}*/





		}
		private void shoot()
		{

				//Debug.Log ("shoot");
				transform.LookAt (Cible.position);
				Rigidbody rProjectile_ = Instantiate (m_rProjectile, transform.position, transform.rotation) as Rigidbody;
				rProjectile_.velocity = transform.TransformDirection (Vector3.forward * m_fSpeed);
				Destroy(rProjectile_.gameObject, m_fTimebeforedestruction);



					

		}
		private void eviterAllier()
		{
			directionAst = Ast.transform.position - myTransform.position;
			Vector3 fwd = myTransform.InverseTransformDirection (Vector3.forward);
			Vector3 fwd2 = myTransform.TransformDirection (Vector3.forward);
			Vector3 fwdl = myTransform.InverseTransformDirection (Vector3.left);
		
			if (Physics.Raycast (myTransform.position, Vector3.forward, out hit, Mathf.Infinity)) {
				//Debug.Log ("detection ennemy devant");
				if (hit.transform.tag == "Ennemy" && hit.distance < 50) 
				{

					//Debug.Log ("eviter ennemy devant");
					rb.AddForce (0, 0, 10);
					
				}
			}
			if (Physics.Raycast (myTransform.position, Vector3.right, out hit, Mathf.Infinity))
			{
				//Debug.Log ("detection ennemy droite");
				//Debug.Log (hit.distance);
				
				if (hit.transform.tag=="Ennemy" && hit.distance < 100)
				{
					float y=Random.Range(-30,30);
					float z=Random.Range(-30,30);
					//Debug.Log ("eviter ennemy droite");
					rb.AddForce (-10,0,0);	
				}
			}
			if (Physics.Raycast (myTransform.position, Vector3.left, out hit, Mathf.Infinity))
			{
				//Debug.Log ("detection ennemy gauche");
				
				if (hit.transform.tag=="Ennemy" && hit.distance < 100)
				{
					float y=Random.Range(-30,30);
					float z=Random.Range(-30,30);
					//Debug.Log ("eviter ennemy gauche");
					rb.AddForce (10,0,0);	
				}
			}
			if (Physics.Raycast (myTransform.position, Vector3.up, out hit, Mathf.Infinity))
			{
				//Debug.Log ("detection ennemy gauche");
				
				if (hit.transform.tag=="Ennemy" && hit.distance < 200)
				{
					float y=Random.Range(-30,30);
					float z=Random.Range(-30,30);
				//	Debug.Log ("eviter ennemy gauche");
					rb.AddForce (0,-5,-10);	
				}
			}
			if (Physics.Raycast (myTransform.position, Vector3.down, out hit, Mathf.Infinity))
			{
				//Debug.Log ("detection ennemy gauche");
				
				if (hit.transform.tag=="Ennemy" && hit.distance < 200)
				{
					float y=Random.Range(-30,30);
					float z=Random.Range(-30,30);
				//	Debug.Log ("eviter ennemy gauche");
					rb.AddForce (0,5,-10);	
				}
			}
		//	rb.AddForce (0,0,0);

		}
		private void fuite()
		{
			//SetTarget(CibleBaseEnnemy);
		}

		private void eviterAsteroid ()
		{
			Vector3 fwd = myTransform.InverseTransformDirection (Vector3.forward);
			if (Physics.Raycast (myTransform.position, fwd, out hit, Mathf.Infinity)) 
			{
				
				if (hit.transform.tag == "Object" && hit.distance < 100)
				{
					//Debug.Log ("detection ast");
					rb.AddForce (0, 50, 30);
				}
			}

		}



		static public float getPuissance()
		{
			return m_fPuissance;
		}
	}
}


