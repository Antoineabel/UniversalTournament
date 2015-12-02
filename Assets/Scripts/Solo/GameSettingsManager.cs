using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameSettingsManager : MonoBehaviour
{

    private static GameObject m_goShipPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetShipPrefab(GameObject _goShipPrefab)
    {
        m_goShipPrefab = _goShipPrefab;
    }

    public GameObject GetShipPrefab()
    {
        return m_goShipPrefab;
    }
}
