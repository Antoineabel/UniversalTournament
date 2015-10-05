using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour
{
    private static bool m_bIsPaused;
    public Canvas m_cCanvas;
    public GameObject m_goDisableScript;
     
    // Use this for initialization
    void Start ()
    {
        m_bIsPaused = false;
        m_cCanvas.gameObject.SetActive(false);
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

                m_goDisableScript.SetActive(true);
                
            }
            else
            {
                m_bIsPaused = true;
                m_cCanvas.gameObject.SetActive(true);
                Time.timeScale = 0.0f;

                m_goDisableScript.SetActive(false);
            }
        }
    }

	public bool GetIfIsPause()
	{
		return m_bIsPaused;
	}
}
