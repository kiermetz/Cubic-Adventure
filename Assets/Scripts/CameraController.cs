/*
 * CameraController.cs C#
 * Author : Kiéran METZ
 * Creation : 26/10/2015
 * Last Modification : 26/10/2015
 * Last Author Modification : Kiéran METZ
 *
 *********************************************
 *
 * Description :
 *
 * This class contains the parameters of the player.
 *
 */
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float rotateSpeed = 60.0f;
	public GameObject player;
	private Vector3 offset;
	private Vector3 xRotate;
	private Vector3 cameraAngles;
	private Vector3 cameraPlayer;
	private Vector3 newposition;
	private float time;


	// Use this for initialization
	void Start () {

		offset = transform.position - player.transform.position;
		xRotate = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {
		time = Mathf.Min (Time.deltaTime, 0.04f);
	
		cameraAngles = transform.rotation.eulerAngles;
		cameraPlayer = player.transform.rotation.eulerAngles;

		xRotate = new Vector3 (-Input.GetAxis ("Mouse Y"), 0.0f, 0.0f);

		if (cameraAngles.x + xRotate.x * rotateSpeed * time >= 271f || cameraAngles.x + xRotate.x * rotateSpeed * time <= 89f )
			transform.Rotate (xRotate * rotateSpeed * time);

		newposition = new Vector3(0.5f * Mathf.Sin(cameraPlayer.y * 2f * Mathf.PI / 360f), 0f, 0.5f * Mathf.Cos(cameraPlayer.y * 2f * Mathf.PI / 360f));

		offset.x = newposition.x;
		offset.z = newposition.z;

		transform.position = player.transform.position + offset;
	}
}
