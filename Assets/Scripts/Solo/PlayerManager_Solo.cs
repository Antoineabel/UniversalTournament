using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerManager_Solo : MonoBehaviour
{
    private GameObject m_goShip;
    private Transform myTransform;
    private AudioSource m_acSpawnSound;

    // Use this for initialization
    void Start()
    {
        m_acSpawnSound = GetComponent<AudioSource>();
        myTransform = GetComponent<Transform>();

        m_goShip = Instantiate(GameObject.Find("GameSettings").GetComponent<GameSettingsManager>().GetShipPrefab(), myTransform.position, myTransform.rotation) as GameObject;
        m_acSpawnSound.Play();

        m_goShip.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
