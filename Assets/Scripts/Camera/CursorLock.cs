﻿using UnityEngine;
using System.Collections;

public class CursorLock : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (gameObject.GetComponent<PauseMenuScript>().m_bIsPaused)
        {
			UpdateCursorStatus (CursorLockMode.None, true);
		}
        else
        {
			UpdateCursorStatus(CursorLockMode.Locked, false);
		}
	}

	void UpdateCursorStatus(CursorLockMode _clmMode, bool _bIsvisible)
	{
		Cursor.lockState = _clmMode;
		Cursor.visible = _bIsvisible;
	}
}
