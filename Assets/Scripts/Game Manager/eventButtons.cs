using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class eventButtons : NetworkBehaviour
{

    private string[] m_sSceneMode;

    void Awake()
    {
        Time.timeScale = 1;
    }

    // Use this for initialization
    void Start()
    {
        m_sSceneMode = Application.loadedLevelName.Split('_');
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadGame()
    {
        Application.LoadLevel("game_Solo");
    }

    public void loadOptions()
    {
        Application.LoadLevel("options");
    }

    public void loadScores()
    {
        Application.LoadLevel("scores");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        if (m_sSceneMode[1] == "Multi")
        {
            if (isServer)
            {
                GameObject.Find("GO_NetworkManager").GetComponent<NetworkManager>().StopHost();
            }
        }
        Application.LoadLevel("menu");
    }

    public void MenuPlay()
    {
        Camera.main.transform.Rotate(0, 90, 0);
    }

    public void ReturnMenu()
    {
        Camera.main.transform.Rotate(0, -90, 0);
    }

    public void MenuMulti()
    {
        Application.LoadLevel("Menu_Multi");
    }

    public void MenuSolo()
    {
        Application.LoadLevel("game_Solo");

    }

}
