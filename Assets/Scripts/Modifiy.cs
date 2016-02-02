using UnityEngine;
using System.Collections;

public class Modifiy : MonoBehaviour {

	Vector2 rot;
	Block block = new BlockGrass();
	
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
				EditTerrain.SetBlock (hit, block, true);
			}
		}

		if (Input.GetButtonDown ("Sav")) {
			World.saving = true;
		}

		if (Input.GetButtonDown ("Alpha1")) {
			block = new Block ();
		} else if (Input.GetButtonDown ("Alpha3"))
			block = new BlockDirt ();
		else if (Input.GetButtonDown ("Alpha2"))
			block = new BlockGrass ();
		else if (Input.GetButtonDown ("Alpha4"))
			block = new BlockWood ();
		else if (Input.GetButtonDown ("Alpha5"))
			block = new BlockLeaves ();
		else if (Input.GetButtonDown ("Alpha6"))
			block = new BlockWater ();
		else if (Input.GetButtonDown ("Alpha7"))
			block = new BlockWoodPlanks ();
		else if (Input.GetButtonDown ("Alpha8"))
			block = new BlockStoneBricks ();
		else if (Input.GetButtonDown ("Alpha9"))
			block = new BlockGlass ();
		else if (Input.GetButtonDown ("Alpha0"))
			block = new BlockTile ();

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
