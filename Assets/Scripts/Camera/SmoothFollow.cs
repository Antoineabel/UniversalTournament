using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Utility;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
		
	// The target we are following
	[SerializeField]
	private Transform target;
	// The distance in the x-z plane to the target
	[SerializeField]
	private float distance = 10.0f;
	// the height we want the camera to be above the target
	[SerializeField]
	private float height = 5.0f;
	[SerializeField]
	private float damping;
		
	private Vector3 shakeOffset = new Vector3(0,0,0);
	
	/*public float duration = 0.5f;
	public float speed = 1.0f;
	public float magnitude = 0.1f;*/

	// Use this for initialization
	void Update() {
        if (!target)
        {
            Debug.Log("pas de target");
            GameObject goTarget = GameObject.FindGameObjectWithTag("CameraTarget");
            if (goTarget)
            {
                target = goTarget.transform;
                Debug.Log("target found");
            }
            /* 
             GameObject []go = GameObject.FindGameObjectsWithTag("Player");
             foreach (GameObject it in go)
             {
                 if (it.GetComponent<NetworkIdentity>().isLocalPlayer)
                     target = it.transform.GetChild(1).transform;
                 else
                 {
                     it.tag = "Ennemy";
                 }
             }*/
        }
    }
		
	/*public void PlayShake() {	
		StopAllCoroutines();
		Debug.Log("Don't rattle me bones");
		StartCoroutine("Shake");
	}*/

	public void SetOffset(Vector3 offset){
		shakeOffset = offset;
	}

	// FixedUpdate(), becausue we are targetting a physical object
	void FixedUpdate()
	{
		// Early out if we don't have a target
		if (!target)
			return;
				
		//Vector3 currentPosition = transform.position;

		//calculate wanted position by setting it to the offset, then rotating it, then translating it to the right position
		Vector3 wantedPosition = (new Vector3 (0, height, -distance));
		wantedPosition = target.rotation * wantedPosition;
		wantedPosition += target.position;

		//interpolate current position and wanted position.
		wantedPosition = Vector3.Lerp (transform.position, wantedPosition, damping*Time.deltaTime);

		transform.position = wantedPosition+shakeOffset;

			
		// Always look at the target
		transform.LookAt(target, target.up);
	}

	public void SetDistanceAndHeight(float _fDist, float _fHeight)
	{
		distance = _fDist;
		height = _fHeight;
	}

	public float GetDistance()
	{
		return distance;
	}

	public float GetHeight()
	{
		return height;
	}

	/*IEnumerator Shake() {
		
		float elapsed = 0.0f;
		
		//Vector3 originalCamPos = Camera.main.transform.position;
		float randomStart = Random.Range(-1000.0f, 1000.0f);
		
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;			
			
			float percentComplete = elapsed / duration;			
			
			// We want to reduce the shake from full power to 0 starting half way through
			float damper = 1.0f - Mathf.Clamp(2.0f * percentComplete - 1.0f, 0.0f, 1.0f);
			
			// Calculate the noise parameter starting randomly and going as fast as speed allows
			float alpha = randomStart + speed * percentComplete;
			
			// map noise to [-1, 1]
			float x = Util.Noise.GetNoise(alpha, 0.0f, 0.0f) * 2.0f - 1.0f;
			float y = Util.Noise.GetNoise(0.0f, alpha, 0.0f) * 2.0f - 1.0f;
			float z = Util.Noise.GetNoise(0.0f, 0.0f, alpha) * 2.0f - 1.0f;
			
			x *= magnitude * damper;
			y *= magnitude * damper;
			z *= magnitude * damper;
			
			shakeOffset = new Vector3(x,y,z);
			
			yield return null;
			
		}	
		//Camera.main.transform.position = originalCamPos;
	}*/
}