using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class World : MonoBehaviour {

	public Dictionary<WorldPos, Chunk> chunks = new Dictionary<WorldPos, Chunk> ();
	public GameObject chunkPrefab;
	public static int chunkWidth = 16;
	public static int chunkHeight = 64;
	public static bool saving = false;
	public string worldName = "world";
	public int seed;
	public static Vector3[] grainOffset;
	public bool plane = false;
	public int SE = StoryEvent.getIntroEvent();

	void Awake ()
	{
		worldName = PlayerPrefs.GetString ("World Name");
		seed = Int32.Parse(PlayerPrefs.GetString ("Seed"));
		plane = Convert.ToBoolean(PlayerPrefs.GetString ("Plane"));
		StoryEvent.setIntroEvent(Int32.Parse(PlayerPrefs.GetString ("Story Event")));
		if (seed == 0) // if seed ==0 then seed is random
			seed = UnityEngine.Random.Range(0, int.MaxValue);

		SetGrainOffset (6, seed);

        VRSettings.enabled = !VRSettings.enabled;
	}

	void Start() {
		
	}

	void Update() {
		SE = StoryEvent.getIntroEvent ();
		if (saving) {
			ManualSave (this);
			Serialization.SaveWorld(this);
			saving = false;
		}
	}

	public void ManualSave(World world) {
		foreach (Chunk chunk in world.chunks.Values)
		{
			Serialization.SaveChunk (chunk);
		}
	}

	public static void SetGrainOffset(int nb, int seed) {
		grainOffset = new Vector3[nb];
		UnityEngine.Random.seed = seed;

		for(int i=0; i<nb; i++)
			grainOffset[i] = new Vector3 (UnityEngine.Random.value * 10000, UnityEngine.Random.value * 10000, UnityEngine.Random.value * 10000);
	}

	public Vector3 GetGrainOffset(int i) {
		return grainOffset [i];
	}

	public void CreateChunk(int x, int y, int z) {
		WorldPos worldPos = new WorldPos (x, y, z);

		GameObject newChunkObject = Instantiate (chunkPrefab, new Vector3 (x, y, z), Quaternion.Euler (Vector3.zero)) as GameObject;

		Chunk newChunk = newChunkObject.GetComponent<Chunk> ();

		newChunk.pos = worldPos;
		newChunk.world = this;

		chunks.Add (worldPos, newChunk);

		/*bool loaded = Serialization.Load (newChunk);
		if (loaded)
			return;*/

/*		for (int xi = 0; xi < chunkWidth; xi++) {
			for (int yi = 0; yi < chunkHeight; yi++) {
				for (int zi = 0; zi < chunkWidth; zi++) {
					if (yi <= 7) {
						SetBlockChunk (newChunk, xi, yi, zi, new BlockGrass ());
					} else {
						SetBlockChunk (newChunk, xi, yi, zi, new BlockAir ());
					}
				}
			}
		}
	*/
		var terrainGen = new TerrainGen ();
		newChunk = terrainGen.ChunkGen (newChunk, newChunk.pos.x, newChunk.pos.y, newChunk.pos.z);

		newChunk.SetBlockUnModified ();
		Serialization.Load (newChunk);
	}

	public Chunk GetChunk(int x, int y, int z) {
		WorldPos pos = new WorldPos ();
		int multipleWidth = chunkWidth;
		int multipleHeight = chunkHeight;
		pos.x = Mathf.FloorToInt ((float)x / (float)multipleWidth) * multipleWidth;
		pos.y = Mathf.FloorToInt ((float)y / (float)multipleHeight) * multipleHeight;
		pos.z = Mathf.FloorToInt ((float)z / (float)multipleWidth) * multipleWidth;

		Chunk containerChunk = null;

		chunks.TryGetValue (pos, out containerChunk);

		return containerChunk;
	}

	public Block GetBlock(int x, int y, int z) {
		Chunk containerChunk = GetChunk (x, y, z);

		if (containerChunk != null) {
			Block block = containerChunk.GetBlock ((x - containerChunk.pos.x),
				(y - containerChunk.pos.y),
				(z - containerChunk.pos.z));
			return block;
		} else {
			return new BlockAir ();
		}
	}

	public void SetBlock (int x, int y, int z, Block block) {
		Chunk chunk = GetChunk (x, y, z);

		if (chunk != null) {
			int xblock = x - chunk.pos.x;
			int yblock = y - chunk.pos.y;
			int zblock = z - chunk.pos.z;
			chunk.SetBlock (xblock, yblock, zblock, block);
			chunk.update = true;

			UpdateIfEqual(x - chunk.pos.x, 0, new WorldPos(x - 1, y, z));
			UpdateIfEqual(x - chunk.pos.x, chunkWidth- 1, new WorldPos(x + 1, y, z));
			UpdateIfEqual(y - chunk.pos.y, 0, new WorldPos(x, y - 1, z));
			UpdateIfEqual(y - chunk.pos.y, chunkHeight - 1, new WorldPos(x, y + 1, z));
			UpdateIfEqual(z - chunk.pos.z, 0, new WorldPos(x, y, z - 1));
			UpdateIfEqual(z - chunk.pos.z, chunkWidth - 1, new WorldPos(x, y, z + 1));
		}
	}

	public void SetBlockChunk (Chunk chunk, int x, int y, int z, Block block) {
		chunk.SetBlock (x,y,z, block);
	}

	public void DestroyChunk(int x, int y, int z)
	{
		Chunk chunk = null;
		if (chunks.TryGetValue(new WorldPos(x, y, z), out chunk))
		{
			Serialization.SaveChunk (chunk);
			UnityEngine.Object.Destroy(chunk.gameObject);
			chunks.Remove(new WorldPos(x, y, z));
		}
	}

	void UpdateIfEqual(int value1, int value2, WorldPos pos)
	{
		if (value1 == value2)
		{
			Chunk chunk = GetChunk(pos.x, pos.y, pos.z);
			if (chunk != null)
				chunk.update = true;
		}
	}
}