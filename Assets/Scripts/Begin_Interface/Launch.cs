using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {

	void OnGUI()
	{
		var style = new GUIStyle ("Button");
		style.fontSize = 30;
		const int buttonWidth = 350;
		const int buttonHeight = 60;

		// Affiche un bouton pour démarrer la partie
		if (
			GUI.Button (
				// Centré en x, 2/3 en y
				new Rect (
					Screen.width / 2 - (buttonWidth / 2),
					(Screen.height / 2) - (buttonHeight / 2),
					buttonWidth,
					buttonHeight
				),
				"Nouvelle Partie", style
			)) {
			// Sur le clic, on démarre le premier niveau
			// "Stage1" est le nom de la première scène que nous avons créés.
			SceneManager.LoadScene ("Jeu");
		}

		if (GUI.Button (
				// Centré en x, 2/3 en y
			    new Rect (
				    Screen.width / 2 - (buttonWidth / 2),
				    (Screen.height / 2) - (buttonHeight / 2) + 80,
				    buttonWidth,
				    buttonHeight
			    ),
			    "Charger Partie", style
		    ))
		{
			//Continue
			SceneManager.LoadScene ("Continue");
		}
	}
}
