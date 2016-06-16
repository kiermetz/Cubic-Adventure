using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public float timeBetweenJumps = 8f;			// The time in seconds between each jump.
	public int attackDamage = 10;               // The amount of health taken away per attack.
	public int jumpDamage = 35;              	// The amount of health taken away per jump.
	public int jumpForce = 5;					// The force apply to the rigidbody per jump.
	public AudioClip attackClip;

	AudioSource audioSource;
	Animator anim;                              // Reference to the animator component.
	GameObject player;                          // Reference to the player GameObject.
	PlayerHealth playerHealth;                  // Reference to the player's health.
	CharacterEvent playerEvent;                 // Reference to the player's event.
	EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	Rigidbody rb;								// Reference to this enemy's rigidbody.
	Monster state;								// Reference to this enemy's state.
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	bool isJumping = false;
	float timerJump;                            // Timer for counting up to the next jump.
	float timerAttack;                          // Timer for counting up to the next attack.


	void Awake ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		playerEvent = player.GetComponent <CharacterEvent> ();
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent <Animator> ();
		state = GetComponent<Monster> ();
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}


	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the player...
		if(other.gameObject == player)
		{
			// ... the player is in range.
			playerInRange = true;
		}
	}


	void OnTriggerExit (Collider other)
	{
		// If the exiting collider is the player...
		if(other.gameObject == player)
		{
			// ... the player is no longer in range.
			playerInRange = false;
		}
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject == player)
		{
			if (isJumping)
				EndJumping ();
		}
	}


	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timerAttack += Time.deltaTime;
		timerJump += Time.deltaTime;

		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if(timerAttack >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
		{
			// ... attack.
			Attack ();
		}
		else if(timerJump >= timeBetweenJumps && state.attackMode && enemyHealth.currentHealth > 0)
		{
			// ... attack.
			Jump ();
		}

		if (isJumping)
			rb.MovePosition (transform.position + (transform.up * jumpForce /2 + transform.forward * jumpForce ) * Time.deltaTime);
	}


	void Attack ()
	{
		// Reset the timer.
		timerAttack = 0f;

		anim.SetTrigger("Attack");

		PlayAttackClip ();

		// If the player has health to lose...
		if(playerHealth.currentHealth > 0 && !playerEvent.protection)
		{
			// ... damage the player.
			playerHealth.TakeDamage (attackDamage);
		}
	}

	void Jump ()
	{
		// Reset the timer.
		timerJump = 0f;

		anim.SetTrigger("Jump");

		isJumping = true;

		PlayAttackClip ();

		// If the player has health to lose...
		if(playerHealth.currentHealth > 0 && !playerEvent.protection)
		{
			// ... damage the player.
			playerHealth.TakeDamage (jumpDamage);
		}
	}

	void EndJumping () {
		isJumping = false;
	}

	void PlayAttackClip (){
		audioSource.clip = attackClip;
		audioSource.Play ();
	}
}