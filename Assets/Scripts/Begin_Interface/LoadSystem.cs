using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System;

public class LoadSystem : MonoBehaviour {

	private Vector2 scrollViewVector = Vector2.zero;
	private String saveFolder = "saves/";
	private int pos = 0;
	private int high = 0;

	void OnGUI () {
		pos = 0;

		// Make a group on the center of the screen
		GUI.BeginGroup (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 85, 1000, 1000));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

		// Make a box so to see where the group is on-screen.
		GUI.Box (new Rect (0,25,700,200), "");

		// Make a scroolView to contains the registered game
		scrollViewVector = GUI.BeginScrollView (new Rect (0, 25, 700, 200), scrollViewVector, new Rect (0, 0, 300, high*50));

		// Start the ScrollView
		high = 0;
		foreach (string s in Directory.GetDirectories(saveFolder))
		{
			high++;
			String world = s.Substring (saveFolder.Length);
			String[] WorldInfo = Serialization.LoadWorld (s + "/world.txt");
			if (GUI.Button (new Rect (0, pos, 700, 50), world)) {
				PlayerPrefs.SetString ("World Name", world);
				PlayerPrefs.SetString ("Seed", WorldInfo[1]);
				PlayerPrefs.SetString ("Plane", WorldInfo[2]);
				StartMenu.pressed = world;
			}
			pos += 50;
		}
		
		// End the ScrollView
		GUI.EndScrollView();

		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();
	}
}
