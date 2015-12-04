﻿using System;
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
		private Transform myTransform;
//	public Transform Target2; 
		private Transform EchangeCible;  
		private Transform Cible; 
		private Transform Asteroid; 

		public Rigidbody m_rProjectile;
		
		public static float m_fPuissance; // a passer en private une fois la valeur determinee
		public float m_fSpeed; // a passer en private une fois la valeur determinee
		public float m_fTimebeforedestruction; // a passer en private une fois la valeur determinee
		
		public Rigidbody rb;
		public GameObject m_goWeapon;

		// setup script properties
		private void Awake()
		{
			// get the reference to the aeroplane controller, so we can send move input to it and read its current state.
			m_AeroplaneController = GetComponent<SpaceShipController>();
			
			// pick a random perlin starting point for lateral wandering
			m_RandomPerlin = Random.Range(0f,180f);
			EchangeCible=GameObject.FindWithTag("Player").transform;
			Cible=GameObject.FindWithTag("Base").transform;
			Asteroid=GameObject.FindWithTag("Object").transform;
			myTransform = transform;
			rb = GetComponent<Rigidbody> ();
		}

		
		// reset the object to sensible values
		public void Reset()
		{
			m_TakenOff = false;
		}
		
		
		// fixed update is called in time with the physics system update
		private void FixedUpdate()
		{

			
			if (Vector3.Distance (Asteroid.position, myTransform.position) < 15) {
				Debug.LogWarning("Je Vais me prendre un astéroide");
				m_AeroplaneController.Move(0,20,0,0,false);
				deplacement ();
			}
			if (Vector3.Distance (EchangeCible.position, myTransform.position) < 40) {
				rb.AddForce (0, 0, 40);
				SetTarget (EchangeCible);
				deplacement ();
				if (Vector3.Distance (EchangeCible.position, myTransform.position) < 20) {
					shoot ();
				}
			}
			else
			{
				SetTarget(Cible);
				deplacement ();

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
				//	if( Vector3.Distance (m_Target.position, myTransform.position) < 40)
			{
				Debug.Log("test");
				// make the plane wander from the path, useful for making the AI seem more human, less robotic.
				Vector3 targetPos = Cible.position +
					transform.right*
						(Mathf.PerlinNoise(Time.time*m_LateralWanderSpeed, m_RandomPerlin)*2 - 1)*
						m_LateralWanderDistance;
				
				// adjust the yaw and pitch towards the target
				Vector3 localTarget = transform.InverseTransformPoint(targetPos);
				float targetAngleYaw = Mathf.Atan2(localTarget.x, localTarget.z);
				float targetAnglePitch = -Mathf.Atan2(localTarget.y, localTarget.z);
				
				
				// Set the target for the planes pitch, we check later that this has not passed the maximum threshold
				targetAnglePitch = Mathf.Clamp(targetAnglePitch, -m_MaxClimbAngle*Mathf.Deg2Rad,
				                               m_MaxClimbAngle*Mathf.Deg2Rad);
				
				// calculate the difference between current pitch and desired pitch
				float changePitch = targetAnglePitch - m_AeroplaneController.PitchAngle;
				
				// AI always applies gentle forward throttle
				const float throttleInput = 0.5f;
				
				// AI applies elevator control (pitch, rotation around x) to reach the target angle
				float pitchInput = changePitch*m_PitchSensitivity;
				
				// clamp the planes roll
				float desiredRoll = Mathf.Clamp(targetAngleYaw, -m_MaxRollAngle*Mathf.Deg2Rad, m_MaxRollAngle*Mathf.Deg2Rad);
				float yawInput = 0;
				float rollInput = 0;
				if (!m_TakenOff)
				{
					// If the planes altitude is above m_TakeoffHeight we class this as taken off
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
			m_goWeapon.GetComponent<LaserSaccadeScript> ().Shoot ();
			Debug.Log ("shoot");
		}
		static public float getPuissance()
		{
			return m_fPuissance;
		}
	}
}
/*
using UnityEngine;
using System.Collections;

public class IAEnnemy : MonoBehaviour {
	
	
	public Transform target;
	//public Transform target2;
	public float moveSpeed;
	public int Speed;
	public float rotationSpeed;
	public int maxdistance;
	private Transform myTransform;
	
	
	void Awake()
	{
		myTransform = transform;
	}
	
	void Start ()
	{
		
	}
	
	
	void Update ()
	{
		
		
		move ();
		
		
	}
	void move()
	{
		if( Vector3.Distance (target2.position, myTransform.position) < maxdistance)
		{
			Debug.Log("test");
			Vector3 position =new Vector3(0,0,Random.Range(-100.0f,100.0f));
			transform.Translate (position * Time.deltaTime * moveSpeed);
			/*Vector3 fwd = transform.forward;
			float approachingCornerAngle = Vector3.Angle(target2.forward, fwd);
			transform.Translate(approachingCornerAngle * Time.deltaTime * moveSpeed);
		}
		
		
		if (Vector3.Distance (target.position, myTransform.position) < maxdistance) {

			transform.LookAt (target.position);     
			myTransform.position += myTransform.forward * Speed * Time.deltaTime;
			
		} else {
		transform.Translate (Vector3.forward * Time.deltaTime * moveSpeed);
		/*	yield return new WaitForSeconds(0.2f);
			Vector3 position =new Vector3(Random.Range(-100.0f,100.0f),Random.Range(-50.0f,50.0f),Random.Range(-100.0f,100.0f));
			transform.Translate (position * Time.deltaTime * moveSpeed);
		
		
		}
		
		
	}
}
*/



