using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Modifiy : MonoBehaviour {

	Vector2 rot;
	Block block = new BlockGrass();

	private int scrollPosition = 0;

	public Renderer cube;
	public Material dirt;
	public Material stone;
	public Material stoneBricks;
	public Material tiles;
	public Material water;
	public Material woodPlanks;
	public Material glass;
	public Material grass;
	public Material leaves;
	public Material wood;
	bool recuperateTown = false;
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1 && !CharacterEvent.armed && CharacterEvent.unsheatle) {
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

		/*if (!recuperateTown && Input.GetButtonDown ("R")) {
			recuperateTown = true;
			RecuperateTown rec = new RecuperateTown ();
			rec.LoadTown ();
			recuperateTown = false;
		}*/

		if (!CharacterEvent.armed)
			scrollPosition = (scrollPosition - Mathf.CeilToInt (Input.mouseScrollDelta [1])) % 10;
		
		if (scrollPosition < 0)
			scrollPosition = 9;

		switch (scrollPosition) 
		{
		case 0:
			block = new Block ();
			cube.sharedMaterial = stone;
			break;
		case 2:
			block = new BlockDirt ();
			cube.sharedMaterial = dirt;
			break;
		case 1:
			block = new BlockGrass ();
			cube.sharedMaterial = grass;
			break;
		case 3:
			block = new BlockWood ();
			cube.sharedMaterial = wood;
			break;
		case 4:
			block = new BlockLeaves ();
			cube.sharedMaterial = leaves;
			break;
		case 5:
			block = new BlockWater ();
			cube.sharedMaterial = water;
			break;
		case 6:
			block = new BlockWoodPlanks ();
			cube.sharedMaterial = woodPlanks;
			break;
		case 7:
			block = new BlockStoneBricks ();
			cube.sharedMaterial = stoneBricks;
			break;
		case 8:
			block = new BlockGlass ();
			cube.sharedMaterial = glass;
			break;
		case 9:
			block = new BlockTile ();
			cube.sharedMaterial = tiles;
			break;
		}
	}
}
