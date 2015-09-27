using UnityEngine;
using System.Collections;

public class CursorLock : MonoBehaviour {
	CursorLockMode lockMode;
	// Use this for initialization
	void Start () {
		lockMode = CursorLockMode.Locked;
		Cursor.lockState = lockMode;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
