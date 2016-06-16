using UnityEngine;
using System.Collections;

public class RotateToView : MonoBehaviour {

	Transform target;
	Vector3 offset;

	// Use this for initialization
	void Awake () {
		target = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		offset = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
		transform.localPosition = offset;
	}
}
