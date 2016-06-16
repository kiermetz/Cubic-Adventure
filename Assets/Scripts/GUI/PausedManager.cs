using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PausedManager : MonoBehaviour {

	private Animator anim;
	public static bool paused;
	public Transform trans;

	void Start()
	{
		anim = GetComponent<Animator> ();
		trans = GetComponent<Transform> ();
		paused = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F10))
		{
			Pause();
			Animation ();
		}
		if (transform.localPosition == new Vector3 (0, 0, 0) && paused)
			Time.timeScale = 0;
		else {
			Time.timeScale = 1;
		}

		Animation ();
	}

	void Pause()
	{
		paused = !paused;
	}

	public void Save()
	{
		World.saving = true;
		Pause ();
	}

	void Animation() {
		anim.SetBool ("Paused",paused);
	}
}