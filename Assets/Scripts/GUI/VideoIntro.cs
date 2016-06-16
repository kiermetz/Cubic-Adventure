using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]

public class VideoIntro : MonoBehaviour {
	
	public MovieTexture movie;
	public AudioClip audio;
	private AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = audio;
		audioSource.Play();
		movie.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
