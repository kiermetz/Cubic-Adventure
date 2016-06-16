using UnityEngine;
using System.Collections;

public class RenderMode : MonoBehaviour {

	public Renderer wolfRender;
	public Material normalMode;
	public Material rageMode;
	CharacterEvent playerEvent;
	bool switchRender = false;

	// Use this for initialization
	void Awake () {
		playerEvent = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterEvent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerEvent.isInFight && !switchRender) {
			wolfRender.sharedMaterial = rageMode;
			switchRender = true;
		} else if (!playerEvent.isInFight && switchRender) {
			wolfRender.sharedMaterial = normalMode;			
			switchRender = false;
		}
	}
}
