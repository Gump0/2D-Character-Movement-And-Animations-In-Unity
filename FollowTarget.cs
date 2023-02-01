using UnityEngine;

public class FollowTarget : MonoBehaviour
{
		public Transform target;

		// Update is called once per frame
		void LateUpdate ()
		{
                // use LateUpdate to update the position of the camera
				transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);
		}
}
