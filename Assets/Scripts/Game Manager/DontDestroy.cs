using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour
{
    public AudioClip[] m_acSoundsArray;
    private AudioSource m_asAmbianceSound;
    private bool m_bIsPlaying = false;

	void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        m_asAmbianceSound = GetComponent<AudioSource>();
    }
	// Use this for initialization
	void Start ()
    {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName == "splash" || Application.loadedLevelName == "menu" || Application.loadedLevelName == "Menu_Solo" || Application.loadedLevelName == "map")
        {
            if (!m_bIsPlaying)
            {
                m_asAmbianceSound.clip = m_acSoundsArray[0];
                m_asAmbianceSound.Play();

                m_bIsPlaying = true;
            }
        }
        else if (Application.loadedLevelName == "game_Solo")
        {
            if (m_bIsPlaying)
            {
                m_asAmbianceSound.clip = m_acSoundsArray[1];
                m_asAmbianceSound.Play();

                m_bIsPlaying = false;
            }
        }
        else
        {
            if (m_bIsPlaying)
            {
                m_asAmbianceSound.clip = m_acSoundsArray[2];
                m_asAmbianceSound.Play();

                m_bIsPlaying = false;
            }
        }
    }
}
