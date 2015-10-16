using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Utility;

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
		
		// Use this for initialization
		void Start() {
            if (!target)
            {
                Debug.Log("pas de target");
                GameObject []go = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject it in go)
                {
                    if (it.GetComponent<NetworkIdentity>().isLocalPlayer)
                        target = it.transform.GetChild(1).transform;
                    else
                    {
                        it.tag = "Ennemy";
                    }
                }
            }
        }
		
		// FixedUpdate(), becausue we are targetting a physical object
		void FixedUpdate()
		{
			// Early out if we don't have a target
			if (!target)
				return;
				
			Vector3 currentPosition = transform.position;

			//calculate wanted position by setting it to the offset, then rotating it, then translating it to the right position
			Vector3 wantedPosition = (new Vector3 (0, height, -distance));
			wantedPosition = target.rotation * wantedPosition;
			wantedPosition += target.position;

			//interpolate current position and wanted position.
			wantedPosition = Vector3.Lerp (transform.position, wantedPosition, damping*Time.deltaTime);

			transform.position = wantedPosition;

			
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
}