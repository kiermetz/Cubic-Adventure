using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour {

	public int startHealth = 100;
	public int currentHealth;
	public AudioClip deathClip;
	public AudioClip AieClip;
	public Material healthMaterial;
	public float timeBetweenHeal = 5f;

	Color color;
	bool isDead;
	private int rgb  = 255;
	private float healTimer;
	Animator anim;
	AudioSource playerAudio;
	FirstPersonController playerMovement;
	CharacterEvent playerEvent;
	FadeToScene fadeToScene;

	void Awake(){
		anim = GetComponent<Animator> ();
		playerAudio = GetComponent<AudioSource> ();
		playerEvent = GetComponent<CharacterEvent> ();
		playerMovement = GetComponent<FirstPersonController> ();
		fadeToScene = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<FadeToScene>();

		currentHealth = startHealth;

		color.b = 0;
		color.r = (((float)currentHealth * (-(float)rgb / (float)startHealth) + (float)rgb)/(float) rgb);
		color.g = ((float)currentHealth / (float)startHealth);
		healthMaterial.SetColor ("_Color", color);
	}
	
	// Update is called once per frame
	void Update () {
		healTimer += Time.deltaTime;

		if (!playerEvent.isInFight && healTimer >= timeBetweenHeal) {
			healTimer = 0;
			if (currentHealth <= startHealth) {
				currentHealth += 10;
				color.r = (((float)currentHealth * (-(float)rgb / (float)startHealth) + (float)rgb)/(float) rgb);
				color.g = ((float)currentHealth / (float)startHealth);
				healthMaterial.SetColor ("_Color", color);
			}
		}
	}

	public void TakeDamage (int amount) {
		currentHealth -= amount;

		if (currentHealth <= 0 && !isDead) {
			Death ();
			return;
		}
		color.r = (((float)currentHealth * (-(float)rgb / (float)startHealth) + (float)rgb)/(float) rgb);
		color.g = ((float)currentHealth / (float)startHealth);
		healthMaterial.SetColor ("_Color", color);

		playerAudio.clip = AieClip;
		playerAudio.Play ();
	}

	void Death () {
		isDead = true;

		playerEvent.Storing ();

		anim.SetTrigger ("Die");

		playerAudio.clip = deathClip;
		playerAudio.Play ();

		playerEvent.enabled = false;
		playerMovement.enabled = false;

		fadeToScene.EndScene ();
	}
}
