﻿using UnityEngine;
using System.Collections;

public class eventButtons : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadGame()
    {
        Application.LoadLevel("game");
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
        Application.LoadLevel("menu");
    }

    public void Resume()
    {
        GameObject.Find("GameManager").GetComponent<PauseMenuScript>().m_bIsPaused = false;
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
        Application.LoadLevel("Menu_Solo");

    }

    public void LoadMapSelection()
    {
        Application.LoadLevel("map");

    }
}
