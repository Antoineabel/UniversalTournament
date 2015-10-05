using UnityEngine;
using System.Collections;

public class eventButtons : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
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
		Application.LoadLevel ("menu");
	}
}
