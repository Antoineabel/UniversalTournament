﻿using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace SpaceShip{
	[RequireComponent(typeof (SpaceShipController))]
	public class SpaceShipUserControl4Axis : MonoBehaviour {

		// these max angles are only used on mobile, due to the way pitch and roll input are handled
		public float maxRollAngle = 80;
		public float maxPitchAngle = 80;
		
		// reference to the aeroplane that we're controlling
		private SpaceShipController m_Aeroplane;
		private float m_Throttle;
		private bool m_AirBrakes;
		private float m_Yaw;

        private string[] m_sSceneMode;
		
		private void Start()
        {
			// Set up the reference to the aeroplane controller.
			m_Aeroplane = GetComponent<SpaceShipController>();

            m_sSceneMode = Application.loadedLevelName.Split('_');
		}
		
		
		private void FixedUpdate()
		{
            if (m_sSceneMode[0] != "Menu")
            {
             	    // Read input for the pitch, yaw, roll and throttle of the aeroplane.
                    float roll = CrossPlatformInputManager.GetAxis("Horizontal");
                    float pitch = CrossPlatformInputManager.GetAxis("Mouse Y");
                    m_AirBrakes = (CrossPlatformInputManager.GetButton("Jump"));
                    m_Yaw = CrossPlatformInputManager.GetAxis("Mouse X");
                    m_Throttle = CrossPlatformInputManager.GetAxis("Vertical");
#if MOBILE_INPUT
			        AdjustInputForMobileControls(ref roll, ref pitch, ref m_Throttle);
#endif
               
                	m_Aeroplane.Move(roll, pitch, m_Yaw, m_Throttle, m_AirBrakes);
             }
		}
		
		
		private void AdjustInputForMobileControls(ref float roll, ref float pitch, ref float throttle)
		{
			// because mobile tilt is used for roll and pitch, we help out by
			// assuming that a centered level device means the user
			// wants to fly straight and level!
			
			// this means on mobile, the input represents the *desired* roll angle of the aeroplane,
			// and the roll input is calculated to achieve that.
			// whereas on non-mobile, the input directly controls the roll of the aeroplane.
			
			float intendedRollAngle = roll*maxRollAngle*Mathf.Deg2Rad;
			float intendedPitchAngle = pitch*maxPitchAngle*Mathf.Deg2Rad;
			roll = Mathf.Clamp((intendedRollAngle - m_Aeroplane.RollAngle), -1, 1);
			pitch = Mathf.Clamp((intendedPitchAngle - m_Aeroplane.PitchAngle), -1, 1);
		}
	}
}