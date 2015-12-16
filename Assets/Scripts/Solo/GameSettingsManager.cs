using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameSettingsManager : MonoBehaviour
{

    private static GameObject m_goShipPrefab;
    private static Texture m_tTexturePauseMenu;

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

    public void SetTexturePauseMenu(Texture _tTexturePauseMenu)
    {
        m_tTexturePauseMenu = _tTexturePauseMenu;
    }

    public Texture GetTexturePauseMenu()
    {
        return m_tTexturePauseMenu;
    }
}
