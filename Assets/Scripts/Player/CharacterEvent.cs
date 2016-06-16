using UnityEngine;
using System.Collections;

public class CharacterEvent : MonoBehaviour {

	public bool isInFight = false;
	public bool protection = false;
	public static bool unsheatle = false;
	public static bool armed = false;
	public GameObject weapon;
	public GameObject stick;

	private float inactif = 0;
	private int attack = 0;
	private int attackIterator = 0;
	private bool isArmed = false;
	private bool storing = false;
    private Animator anim;
	private Vector3 labyrinthPlace = new Vector3 (72f, 16.5f, 99f);

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
		weapon.SetActive(false);
		stick.SetActive (false);

		if (PlayerPrefs.GetInt ("CreativeMode") == 0)
			armed = true;
	}
	
	// Update is called once per frame
	void Update () { 
		if (!isInFight && StoryEvent.getIntroEvent() >= 2){
			if (PlayerPrefs.GetInt ("CreativeMode") == 0 && StoryEvent.getIntroEvent() >= 6)
				ChangeArmed ();
			Unsheatled ();
		}
		else {
			armed = true;
			Unsheatled ();
		}
		Attack ();
		if (armed) {
			Protection ();
		}
		Inactif ();
		Animation ();

		if ((transform.position - labyrinthPlace).magnitude <= 3 && StoryEvent.getIntroEvent () == 2)
			StoryEvent.IncrementIntroEvent ();
	}

	public void InFight() {
		isInFight = true;
	}

	public void OutFight(){
		isInFight = false;
	}

	void ChangeArmed()
	{
		if (Input.GetMouseButtonDown (2)) {
			armed = !armed;
			weapon.SetActive(!weapon.activeSelf);
			stick.SetActive (!stick.activeSelf);
		}
	}

	void Unsheatled()
	{
		if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(1)) && !unsheatle)
		{
			unsheatle = true;
			inactif = Time.realtimeSinceStartup;
		}
	}

    void Attack()
    {
		if (Input.GetMouseButtonDown(0) && armed && unsheatle)
        {
            attack = attackIterator;
            inactif = Time.realtimeSinceStartup;
        }
    }

    void Protection()
    {
        if (Input.GetMouseButton(1))
        {
            protection = true;
            inactif = Time.realtimeSinceStartup;
        }
		else
			protection = false;
    }

    void Inactif()
    {
		if (Time.realtimeSinceStartup >= inactif + 8 && armed)
        {
            unsheatle = false;
        }
		else if (Time.realtimeSinceStartup >= inactif + 0.6 && armed)
        {
            attackIterator = 1;
        }
    }

    void Animation ()
    {
        anim.SetBool("Unsheatle", unsheatle);
        anim.SetBool("Protected", protection);
        anim.SetInteger("Attack", attack);
    }

    void Unsheatle()
    {
		if (armed)
        	weapon.SetActive(true);
		else
			stick.SetActive (true);
			
    }

    public void Storing()
    {
        weapon.SetActive(false);
		stick.SetActive (false);
    }

    void BasicAttack(int basicAttack)
    {
        attack = 0;
        if(basicAttack == 3)
        {
            attackIterator = 1;
        }
        else
        {
            attackIterator = basicAttack + 1;
        }
    }
}
