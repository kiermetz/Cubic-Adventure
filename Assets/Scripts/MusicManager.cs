using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip fightClip;

	AudioSource audioSource;
	AudioClip generalClip;
	CharacterEvent playerEvent;
	bool switchClip = false;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource> ();
		playerEvent = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterEvent> ();
		generalClip = audioSource.clip;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerEvent.isInFight && !switchClip) {
			audioSource.clip = fightClip;
			audioSource.Play ();
			switchClip = true;
		} else if (!playerEvent.isInFight && switchClip) {
			audioSource.clip = generalClip;
			audioSource.Play ();
			switchClip = false;
		}
	}
}
