using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public float sinkSpeed = 0.01f;             // The speed at which the enemy sinks through the floor when dead.
	public AudioClip deathClip;                 // The sound to play when the enemy dies.
	public AudioClip hurtClip;                  // The sound to play when the enemy take dammages.
	public Slider healthSlider;


	Animator anim;                              // Reference to the animator.
	AudioSource enemyAudio;                     // Reference to the audio source.
	Wolf wolf;
	EnemyAttack enemyAttack;
	bool isDead;                                // Whether the enemy is dead.
	bool isSinking;                             // Whether the enemy has started sinking through the floor.


	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent <Animator> ();
		enemyAudio = GetComponent <AudioSource> ();
		wolf = GetComponent<Wolf> ();
		enemyAttack = GetComponent<EnemyAttack> ();

		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}

	void Update ()
	{
		// If the enemy should be sinking...
		if(isSinking)
		{
			// ... move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
		if (transform.position.y <= -100)
			Destroy (gameObject);
	}


	public void TakeDamage (int amount)
	{
		// If the enemy is dead...
		if(isDead)
			// ... no need to take damage so exit the function.
			return;

		// Play the hurt sound effect.
		enemyAudio.clip = hurtClip;
		enemyAudio.Play ();

		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		// If the current health is less than or equal to zero...
		if(currentHealth <= 0)
		{
			// ... the enemy is dead.
			Death ();
		}
	}


	void Death ()
	{
		// The enemy is dead.
		isDead = true;

		// Tell the animator that the enemy is dead.
		anim.SetTrigger ("Dead");

		if(StoryEvent.getIntroEvent() == 3)
			StoryEvent.IncrementIntroEvent ();

		enemyAttack.enabled = false;
		wolf.enabled = false;

		// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
		enemyAudio.clip = deathClip;
		enemyAudio.Play ();
	}


	public void StartSinking ()
	{
		// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		GetComponent <Rigidbody> ().isKinematic = true;

		// The enemy should no sink.
		isSinking = true;

		// After 10 seconds destory the enemy.
		Destroy (gameObject, 10f);
	}
}