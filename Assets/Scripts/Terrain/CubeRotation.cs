using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CubeRotation : MonoBehaviour {
	
	void Update () {
		transform.Rotate (new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
