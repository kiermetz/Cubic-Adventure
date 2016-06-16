using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenu : MonoBehaviour {
	
	public GameObject buttonPanel;
	public GameObject panel;
	public GameObject loadPanel;
	public GameObject content;
	public GameObject symbolsError;
	public GameObject nameError;
	public GameObject noNameError;
	public GameObject pressedInformation;
	public GameObject spaceError;
	public GameObject optionPanel;
	public GameObject canvasVideoIntro;
	public Text worldName;
	public Text gameName;

	public static String pressed;

	private String saveFolder = "saves/";
	private List<Char> list = new List<Char>();

	private int creativeMode;
	private bool intro = false;

    void Awake()
    {
        VRSettings.enabled = false;
    }

	void Start() {
		list.Add ('?');
		list.Add('/');
		list.Add('\\');
		list.Add(':');
		list.Add('<');
		list.Add('>');
		list.Add('*');
		list.Add('|');
		list.Add('"');

		pressed = null;
		creativeMode = 0;

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		buttonPanel.SetActive(true);
		panel.SetActive(false);
		loadPanel.SetActive(false);
		optionPanel.SetActive(false);
		pressedInformation.SetActive (false);
	}

	void Update() {
		if (pressed != null) {
			gameName.text = "Selected game : " + pressed;
			pressedInformation.SetActive (false);
		} else
			gameName.text = "";

		if (Input.GetKeyDown (KeyCode.Return)) {
			if (worldName.text != "")
				Launch ();
		}

		if(intro && !canvasVideoIntro.GetComponent<VideoIntro>().movie.isPlaying)
			SceneManager.LoadScene ("Jeu");
	}

	public void CreativeMode () {
		creativeMode = creativeMode == 0 ? 1 : 0;
		PlayerPrefs.SetInt ("CreativeMode", creativeMode);
	}

	public void NewGame () {
		panel.SetActive(true);
		buttonPanel.SetActive(false);
	}

	public void LoadGame () {
		loadPanel.SetActive(true);
		buttonPanel.SetActive(false);
	}

	public void Option () {
		optionPanel.SetActive(true);
		buttonPanel.SetActive(false);
	}

	public void ReturnFromNewGamePanel () {
		panel.SetActive (false);
		buttonPanel.SetActive(true);
		worldName.text = "";
	}

	public void ReturnFromLoadGamePanel () {
		pressed = null;
		buttonPanel.SetActive(true);
		loadPanel.SetActive (false);
		pressedInformation.SetActive (false);
	}

	public void  ReturnFromOptionPanel () {
		optionPanel.SetActive (false);
		buttonPanel.SetActive (true);
	}

	public void Play () {
		if (pressed != null) {
			SceneManager.LoadScene ("Jeu");
			pressedInformation.SetActive (false);
		}
		else
			pressedInformation.SetActive (true);
	}

	public void Launch () {
		foreach (char c in list) {
			for (int i = 0; i < worldName.text.Length; i++) {
				if (worldName.text [i] == c) {
					symbolsError.SetActive(true);
					noNameError.SetActive (false);
					nameError.SetActive(false);
					spaceError.SetActive (false);
					return;
				}
			}
		}
		if (Directory.Exists (saveFolder)) {
			foreach (string s in Directory.GetDirectories(saveFolder))
			{
				String world = s.Substring (saveFolder.Length);
				if (world == worldName.text) {
					nameError.SetActive(true);
					noNameError.SetActive (false);
					symbolsError.SetActive(false);
					spaceError.SetActive (false);
					return;
				}
			}
		}

		if (worldName.text == "") {
			noNameError.SetActive (true);
			symbolsError.SetActive(false);
			nameError.SetActive(false);
			spaceError.SetActive (false);
			return;
		}
		if (worldName.text [0] == ' ') {
			noNameError.SetActive (false);
			symbolsError.SetActive(false);
			nameError.SetActive(false);
			spaceError.SetActive (true);
			return;
		}

		PlayerPrefs.SetString ("World Name", worldName.text);
		PlayerPrefs.SetString ("Seed", "0");
		PlayerPrefs.SetString ("Plane", "False");
		PlayerPrefs.SetString ("Story Event", "0");

		Serialization.SaveWorldBegin (worldName.text, 0, false);

		GetComponent<AudioSource> ().Stop ();
		canvasVideoIntro.SetActive (true);
		intro = true;
	}

	public void Remove() {
		if (pressed != null) {
			String removeFolder = saveFolder + PlayerPrefs.GetString ("World Name");
			Directory.Delete (removeFolder, true);
			pressed = null;
			pressedInformation.SetActive (false);
		}
		else
			pressedInformation.SetActive (true);
	}

	public void Quit() {
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}