/*
 * PlayerController.cs C#
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
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	//public Text Coords;
	public float speed = 1.0f;
	public float jumpSpeed = 4.0f;
	public float gravity = 20.0f;
	public float rotateSpeed = 60.0f;
	public CharacterController controller;
	private Vector3 moveDirection = new Vector3();
	private Vector3 moveRotate =  new Vector3();
	private float time;

	// Use this for initialization
	void Start () {

		controller = GetComponent<CharacterController>();
		moveDirection = Vector3.zero;
		moveRotate = Vector3.zero;
	
	}
	
	// Update is called once per frame
	void Update () {
		time = Mathf.Min (Time.deltaTime, 0.04f);

		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speed;

			if (Input.GetButtonDown ("Jump"))
				moveDirection.y = jumpSpeed;
		}

		moveRotate = new Vector3 (0.0f, Input.GetAxis ("Mouse X"), 0.0f);

		transform.Rotate (moveRotate * time * rotateSpeed);
		
		moveDirection.y -= gravity * time;

		controller.Move (moveDirection * time * 5);

		//Coords.text = "Coordonnées : " + transform.position.ToString();
	
	}
}
