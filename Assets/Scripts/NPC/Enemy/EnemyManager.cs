using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 20f;            // How long between each spawn.
	public World world;

	private Vector3 pos;
	Transform player;
	PlayerHealth playerHealth;       // Reference to the player's heatlh.

	void Awake () {
		if (PlayerPrefs.GetInt ("CreativeMode") == 1) {
			gameObject.SetActive (false);
		}
		Debug.Log (PlayerPrefs.GetInt ("CreativeMode"));
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn ()
	{
		// If the player has no health left...
		if(playerHealth.currentHealth <= 0f || StoryEvent.getIntroEvent() <= 5)
		{
			// ... exit the function.
			return;
		}

		float r = Random.Range (50f,100f);
		float téta = Random.Range (0f,359f);

		pos = new Vector3 (player.position.x + r * Mathf.Sin(téta), player.position.y, player.position.z + r* Mathf.Cos(téta));

		pos.y = world.GetChunk ((int)pos.x, (int)pos.y, (int)pos.z).meshCollider.bounds.size.y + 0.5f;

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (enemy, pos, Quaternion.LookRotation (new Vector3 (Random.rotation.x,0,Random.rotation.z)));
	}
}
