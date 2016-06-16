using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PausedManager : MonoBehaviour {

	Canvas canvas;
	public GameObject buttonPanel;
	public GameObject optionPanel;

	void Start()
	{
		canvas = GetComponent<Canvas>();
		canvas.enabled = false;
		optionPanel.SetActive (false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F10))
		{
			Pause();
		}
	}

	void Pause()
	{
		canvas.enabled = !canvas.enabled;
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Cursor.visible = !Cursor.visible;
		Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
	}

	public void Save()
	{
		World.saving = true;
		Pause ();
	}

	public void Option ()
	{
		buttonPanel.SetActive (false);
		optionPanel.SetActive (true);
	}

	public void Return ()
	{
		buttonPanel.SetActive (true);
		optionPanel.SetActive (false);
	}

	public void Quit()
	{
		World.saving = true;
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainMenu");
	}
}