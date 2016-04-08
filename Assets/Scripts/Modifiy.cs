using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Modifiy : MonoBehaviour {

	Vector2 rot;
	Block block = new BlockGrass();

	private int scrollPosition = 0;

	public Sprite dirt;
	public Sprite stone;
	public Sprite stoneBricks;
	public Sprite tiles;
	public Sprite water;
	public Sprite woodPlanks;
	public Sprite glass;
	public Sprite grass;
	public Sprite leaves;
	public Sprite wood;
	public Image cubeImage;
	bool recuperateTown = false;
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1) {
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = GetComponent<Camera> ().ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.5f));
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100)) {
					EditTerrain.SetBlock (hit, new BlockAir ());
				}
			}

			if (Input.GetMouseButtonDown (1)) {
				Ray ray = GetComponent<Camera> ().ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.5f));
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100)) {
					EditTerrain.SetBlock (hit, block, true);
				}
			}
		}

		if (Input.GetButtonDown ("Sav")) {
			World.saving = true;
		}

		/*if (!recuperateTown && Input.GetButtonDown ("R")) {
			recuperateTown = true;
			RecuperateTown rec = new RecuperateTown ();
			rec.LoadTown ();
			recuperateTown = false;
		}*/

		scrollPosition = (scrollPosition - Mathf.CeilToInt(Input.mouseScrollDelta[1])) % 10;
		
		if (scrollPosition < 0)
			scrollPosition = 9;

		switch (scrollPosition) 
		{
		case 0:
			block = new Block ();
			cubeImage.sprite = stone;
			break;
		case 2:
			block = new BlockDirt ();
			cubeImage.sprite = dirt;
			break;
		case 1:
			block = new BlockGrass ();
			cubeImage.sprite = grass;
			break;
		case 3:
			block = new BlockWood ();
			cubeImage.sprite = wood;
			break;
		case 4:
			block = new BlockLeaves ();
			cubeImage.sprite = leaves;
			break;
		case 5:
			block = new BlockWater ();
			cubeImage.sprite = water;
			break;
		case 6:
			block = new BlockWoodPlanks ();
			cubeImage.sprite = woodPlanks;
			break;
		case 7:
			block = new BlockStoneBricks ();
			cubeImage.sprite = stoneBricks;
			break;
		case 8:
			block = new BlockGlass ();
			cubeImage.sprite = glass;
			break;
		case 9:
			block = new BlockTile ();
			cubeImage.sprite = tiles;
			break;
		}
	}
}
