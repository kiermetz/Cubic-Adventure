using UnityEngine;
using System.Collections;

public class OldMan : MonoBehaviour {

	public float playerDistance = 5f;
	public AudioClip secondText;	

	Transform playerPosition;
	AudioSource audioSource;
	public Vector3 labyrinthPosition = new Vector3(6f, 17f, 74f);

	// Use this for initialization
	void Start () {
		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform;
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (playerPosition);
		if (!audioSource.isPlaying && (StoryEvent.getIntroEvent() == 1 || StoryEvent.getIntroEvent() == 5))
			StoryEvent.IncrementIntroEvent ();
		if ((playerPosition.position - transform.position).magnitude <= playerDistance && StoryEvent.getIntroEvent() == 0) {
			audioSource.Play ();
			StoryEvent.IncrementIntroEvent();
		}
		if (StoryEvent.getIntroEvent () == 4) {
			Wolfdead ();
			StoryEvent.IncrementIntroEvent ();
		}
	}

	public void Wolfdead () {
		transform.position = new Vector3(51, 17, 82);
		audioSource.clip = secondText;
		audioSource.Play ();
	}
}
