using UnityEngine;
using System.Collections;


public class PlayerAttack : MonoBehaviour
{
	public int attackDamage = 50;               // The amount of health taken away per attack.
	public float timeBetweenAttacks = 1;

	float timerAttack;                          // Timer for counting up to the next attack.
	Animator anim;                              // Reference to the animator component.
	GameObject player;                          // Reference to the player GameObject.
	PlayerHealth playerHealth;                  // Reference to the player's health.
	CharacterEvent playerEvent;                 // Reference to the player's event.
	AudioSource audioSource;
	public AudioClip attackClip;


	void Awake ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerEvent = player.GetComponent <CharacterEvent> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update() {
		timerAttack += Time.deltaTime;
	}


	void OnTriggerEnter (Collider other)
	{
		EnemyHealth enemyHealth = other.GetComponent<EnemyHealth> ();

		if(enemyHealth != null && timerAttack >= timeBetweenAttacks)
		{
			timerAttack = 0;
			if (playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0 && !playerEvent.protection) {
				PlayAttackClip ();
				enemyHealth.TakeDamage (attackDamage);
				if (enemyHealth.currentHealth <= 0){
					enemyHealth.StartSinking ();
					playerEvent.OutFight ();
				}
			}
		}
	}

	void PlayAttackClip (){
		audioSource.clip = attackClip;
		audioSource.Play ();
	}
}