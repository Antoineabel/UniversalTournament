using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ShipData : MonoBehaviour {

	private float m_fLife;
//	private float m_fShield;

	private Vector3 m_v3Position;
	private Quaternion m_qRotation;

	private bool m_bIsIA;
	private bool m_bIsLocalPlayer;

	private GameObject m_goShipPrefab;

	private GameObject m_goWeaponPrefab;


	// Use this for initialization
	void Start () {
		m_fLife = GetComponent<LifeManager> ().m_fLife;
//		m_fShield = GetComponent<LifeManager> ().m_fShield;

		m_v3Position = GetComponent<Transform> ().position;
		m_qRotation = GetComponent<Transform> ().rotation;

		m_goWeaponPrefab = transform.GetChild (0).GetChild (0).gameObject;
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		    m_goWeaponPrefab.GetComponent<LaserSaccadeScript>().Shoot();
	}




	/***
	 * 					SET
	 * */
	public void SetShipPrefab(GameObject _goShipPrefab)	{m_goShipPrefab = _goShipPrefab;}

    public void SetIfIsIA(bool _bIsIA) { m_bIsIA = _bIsIA; }

    public void SetIFIsLocalPlayer(bool _bIsLocalPlayer) { m_bIsLocalPlayer = _bIsLocalPlayer; }


	/***
	 * 					GET
	 * */
	public float GetShipLife()	{return m_fLife;}

//	public float GetShipShield(){return m_fShield;}

	public Vector3 GetShipPosition(){return m_v3Position;}

	public Quaternion GetShipRotation(){return m_qRotation;}

	public bool GetIfIsLocalPlayer(){return m_bIsLocalPlayer;}
	
	public bool GetIfIsIA(){return m_bIsIA;}

	public GameObject GetShipPrefab(){return m_goShipPrefab;}



}
