using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public bool m_bIsPaused;
    public Canvas m_cCanvas;

    public RawImage m_riTexturePauseMenu;
     
    // Use this for initialization
    void Start ()
    {
        m_bIsPaused = false;
        m_cCanvas.gameObject.SetActive(false);

        m_riTexturePauseMenu.texture = GameObject.Find("GameSettings").GetComponent<GameSettingsManager>().GetTexturePauseMenu();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if (m_bIsPaused)
            {
                m_bIsPaused = false;
                m_cCanvas.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                m_bIsPaused = true;
                m_cCanvas.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }
}
