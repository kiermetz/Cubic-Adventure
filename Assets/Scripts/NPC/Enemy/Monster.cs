using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public int moveSpeed = 5;
	public int rotationSpeed = 5;
	public int targetHeight = -1;
	public int viewRay = 30;
	public float maxViewDistance = 25;
	public float minViewDistance = 4;
	public bool attackMode = false;
	World world;

	public enum StateMonster{Patrol, NoMove, Tobedetermined, Move, CalculatePath};

	protected StateMonster stateMonster = StateMonster.Tobedetermined;
	protected Node[] nodePath;
	protected Vector3 NodePosition;
	protected int INode;

	private Animator anim;
	private Rigidbody rb;
	private Transform target;
	private CharacterEvent playerEvent;


	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		playerEvent = target.GetComponent<CharacterEvent> ();
		world = GameObject.FindGameObjectWithTag ("World").GetComponent<World> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPosition = new Vector3 (target.position.x, target.position.y + 1, target.position.z);
		Vector3 objectPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		float differenceMagnitude = (targetPosition - objectPosition).magnitude;

		Quaternion targetRotation = Quaternion.LookRotation (new Vector3 (targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z));
		Quaternion newRotation = Quaternion.Lerp (rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

		switch (stateMonster) {
		case StateMonster.CalculatePath:
			Graph graph = new Graph ((int)objectPosition.x, (int)objectPosition.y, (int)objectPosition.z, viewRay);
			graph.createGraph (world);
			nodePath = graph.shortestWay (new Node ((int)Mathf.FloorToInt (targetPosition.x), (int)Mathf.FloorToInt (targetPosition.y), (int)Mathf.FloorToInt (targetPosition.z), 0, 0), new Node ((int)Mathf.FloorToInt (objectPosition.x), (int)Mathf.FloorToInt (objectPosition.y), (int)Mathf.FloorToInt (objectPosition.z), 0, 0));
			NodePosition = new Vector3 ((float)nodePath [0].x, (float)nodePath [0].y, (float)nodePath [0].z);
			INode = 0;
			stateMonster = StateMonster.Move;
			break;
		case StateMonster.Move:
			Vector3 NP = new Vector3 ((float)nodePath [nodePath.Length - 1].x, (float)nodePath [nodePath.Length - 1].y, (float)nodePath [nodePath.Length - 1].z);
			if ((targetPosition - NP).magnitude >= minViewDistance) {
				stateMonster = StateMonster.Patrol;
				playerEvent.OutFight ();
			}
			if (differenceMagnitude <= minViewDistance )
				stateMonster = StateMonster.NoMove;
			if (objectPosition.x >= NodePosition.x - 1.5f && objectPosition.x <= NodePosition.x + 1.5f && objectPosition.z >= NodePosition.z - 1.5f && objectPosition.z <= NodePosition.z + 1.5f) {
				INode++;
				if (INode < nodePath.Length) {
					NodePosition = new Vector3 ((float)nodePath [INode].x, (float)nodePath [INode].y, (float)nodePath [INode].z);
				} else {
					stateMonster = StateMonster.NoMove;
				}
			} else {
				targetRotation = Quaternion.LookRotation (new Vector3 (NodePosition.x - transform.position.x, 0, NodePosition.z - transform.position.z));
				if(INode >= nodePath.Length - 1)
					targetRotation = Quaternion.LookRotation (new Vector3 (targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z));
					
				
				newRotation = Quaternion.Lerp (rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
				rb.MoveRotation (newRotation);
				rb.MovePosition (transform.position + transform.forward * moveSpeed * Time.deltaTime);
			}
			break;
		case StateMonster.NoMove:
			rb.MoveRotation (newRotation);
			if(differenceMagnitude >= minViewDistance)
				stateMonster = StateMonster.CalculatePath;
			break;
		case StateMonster.Patrol:
			if (differenceMagnitude <= maxViewDistance) {
				stateMonster = StateMonster.CalculatePath;
				playerEvent.InFight();
			}
			break;
		case StateMonster.Tobedetermined:
			if (StoryEvent.getIntroEvent () <= 1) {
				transform.position = new Vector3 (54, 25, 87);
			}
			if (StoryEvent.getIntroEvent () >= 3) {
				if (differenceMagnitude > maxViewDistance)
					stateMonster = StateMonster.Patrol;
				else if (differenceMagnitude < minViewDistance) {
					playerEvent.InFight ();
					stateMonster = StateMonster.NoMove;
				} else {
					playerEvent.InFight ();
					stateMonster = StateMonster.CalculatePath;
				}
			}
			break;
		default:
			stateMonster = StateMonster.Tobedetermined;
				break;
		}

		if (differenceMagnitude <= minViewDistance)
			attackMode = true;
		else
			attackMode = false;
		Anim ();
	}

	void Anim () {
		anim.SetInteger ("StateMonster",(int)stateMonster);
	}

}
