using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LaserContinuScriptMulti : NetworkBehaviour
{
    private LineRenderer m_lrLine;
    private Light m_lLight;

    private string[] m_sSceneMode;

    public float m_fPuissance; // a passer en private une fois la valeur determinee
    public GameObject m_goParticuleEffect;

    private float m_fCylindreScaleY;
    public GameObject m_goLaserCylindre;

    private Transform m_tOriginShoot;
    private GameObject go;
    // Use this for initialization
    void Start ()
    {
        m_tOriginShoot = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0);
        m_fCylindreScaleY = m_goLaserCylindre.transform.localScale.y;
        m_sSceneMode = Application.loadedLevelName.Split('_');
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_sSceneMode[0] != "Menu" )
        {
            if (!GameObject.Find("GameManager").GetComponent<PauseMenuScript>().m_bIsPaused)
            {
                if (isLocalPlayer)
                {
                    if (Input.GetButton("Fire2") )
                    {
                        TransmitLaser();
                    }
                    if (Input.GetButtonUp("Fire2"))
                    {
                        m_fCylindreScaleY = 0;
                        TransmitDestroy();
                    }
                }
            }
        }
	}
    [Client]
    void TransmitDestroy()
    {
        CmdDestroyLaserMulti();
    }

    [Command]
    public void CmdDestroyLaserMulti()
    {
        Destroy(go);
        NetworkServer.UnSpawn(go);
    }
    [Client]
    void TransmitLaser()
    {
        CmdLaserMulti();
    }

    [Command]
    public void CmdLaserMulti()
    {
        Vector3 v3Pos = new Vector3(m_tOriginShoot.position.x, m_tOriginShoot.position.y, m_tOriginShoot.position.z);

        if (go == null)
        {
            go = Instantiate(m_goLaserCylindre) as GameObject;
            
        }
        go.transform.GetChild(0).transform.localScale = new Vector3(0.2f, m_fCylindreScaleY, 0.2f);
        go.transform.rotation = transform.rotation;
        go.transform.position = v3Pos;

        if (go.transform.GetChild(0).GetComponent<PuissanceLaserMulti>().m_bHitSmth)
        {
            float fNewdist = Vector3.Distance(transform.position, go.transform.GetChild(0).GetComponent<PuissanceLaserMulti>().m_v3ColisionPoint) / 2;
            float fOldSize = m_fCylindreScaleY;
            m_fCylindreScaleY = fNewdist;

            go.transform.GetChild(0).transform.localScale = new Vector3(0.2f, m_fCylindreScaleY, 0.2f);
            go.transform.GetChild(0).Translate(0, -(fOldSize - fNewdist), 0);
        }

        if (m_fCylindreScaleY < 70 && !go.transform.GetChild(0).GetComponent<PuissanceLaserMulti>().m_bHitSmth)
        {
            m_fCylindreScaleY++;
            go.transform.GetChild(0).Translate(0, 1, 0);
        }
        NetworkServer.Spawn(go);
    }
}
