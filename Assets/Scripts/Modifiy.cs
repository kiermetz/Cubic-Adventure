using UnityEngine;
using System.Collections;

public class Modifiy : MonoBehaviour {

	Vector2 rot;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = GetComponent<Camera> ().ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.5f));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				Debug.Log ("point : "+hit.point.x+", "+hit.point.y+", "+hit.point.z);
				Debug.Log ("normal : "+hit.normal.x+", "+hit.normal.y+", "+hit.normal.z);
				EditTerrain.SetBlock (hit, new BlockAir ());
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			Ray ray = GetComponent<Camera> ().ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.5f));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				Debug.Log ("point : "+hit.point.x+", "+hit.point.y+", "+hit.point.z);
				Debug.Log ("normal : "+hit.normal.x+", "+hit.normal.y+", "+hit.normal.z);
				EditTerrain.SetBlock (hit, new BlockLeaves (), true);
			}
		}

//		rot = new Vector2 (rot.x + Input.GetAxis ("Mouse X") * 3,
//			rot.y + Input.GetAxis ("Mouse Y") * 3);
//
//		transform.localRotation = Quaternion.AngleAxis (rot.x, Vector3.up);
//		transform.localRotation *= Quaternion.AngleAxis (rot.y, Vector3.left);
//
//		transform.position += transform.forward * 3 * Input.GetAxis ("Vertical");
//		transform.position += transform.right * 3 * Input.GetAxis ("Horizontal");
	}
}
