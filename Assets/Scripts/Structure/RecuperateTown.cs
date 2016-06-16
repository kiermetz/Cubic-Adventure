using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

[Serializable]
public class RecuperateTown {

	Dictionary<WorldPos, Chunk> chunksTown = new Dictionary<WorldPos, Chunk> ();
<<<<<<< HEAD
	public Block[,,] blocksTown = new Block[340, 40, 340];
	public int[,,] intBlocksTown = new int[340, 40, 340];

	public void LoadTown() {

		for (int x = 0; x < 320; x += 16) {
			for (int z = 0; z < 320; z += 16) {
=======
	public Block[,,] blocksTown = new Block[320, 40, 320];
	public int[,,] intBlocksTown = new int[320, 40, 320];

	public void LoadTown() {

		for (int x = 0; x < 150; x += 16) {
			for (int z = 0; z < 150; z += 16) {
>>>>>>> origin/master
				CreateChunk (x, 0, z);
			}
		}

		chunkToTown ();

		Debug.Log ("chunksToTown Created");

		Save ();

		Debug.Log ("chunksToTown Saved");
	}

	public void CreateChunk(int x, int y, int z) {
		WorldPos worldPos = new WorldPos (x, y, z);

		Chunk newChunk = new Chunk();

		newChunk.pos = worldPos;

		chunksTown.Add (worldPos, newChunk);

<<<<<<< HEAD
		Serialization.LoadT (newChunk, "saves/laby/world/");
=======
		//Serialization.Load (newChunk, "saves/plane/");
>>>>>>> origin/master
	}

	public Chunk GetChunk(int x, int y, int z) {
		WorldPos pos = new WorldPos ();
		int multipleWidth = 16;
		int multipleHeight = 64;
		pos.x = Mathf.FloorToInt ((float)x / (float)multipleWidth) * multipleWidth;
		pos.y = Mathf.FloorToInt ((float)y / (float)multipleHeight) * multipleHeight;
		pos.z = Mathf.FloorToInt ((float)z / (float)multipleWidth) * multipleWidth;

		Chunk containerChunk = null;

		chunksTown.TryGetValue (pos, out containerChunk);

		return containerChunk;
	}

	public void chunkToTown() {

<<<<<<< HEAD
		for (int x = 0; x < 320; x++) {
			for (int y = 0; y < 39; y++) {
				for (int z = 0; z < 320; z++) {
					Chunk chunk = GetChunk (x, y, z);
					blocksTown [x, y, z] = chunk.blocks [x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z];
					if (chunk.blocks [x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z] is BlockStoneBricks) {
						for (int j = 0; j < 8; j++) {
							intBlocksTown [x, y + j, z] = 8;
						}
					}
					else if (chunk.blocks [x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z] is BlockWoodPlanks)
						intBlocksTown [x, y, z] = 7;
					else if(intBlocksTown [x, y, z] != 8)
						intBlocksTown [x, y, z] = 0;
=======
		for (int x = 0; x < 160; x++) {
			for (int y = 0; y < 39; y++) {
				for (int z = 0; z < 160; z++) {
					Chunk chunk = GetChunk (x, y, z);
					blocksTown [x, y, z] = chunk.blocks [x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z];
>>>>>>> origin/master
				}
			}
		}
	}

	public void Save() {
		string saveFile = SaveLocation ("construction/Town/");
<<<<<<< HEAD
		saveFile += "Towndemo.bin";
		IFormatter formatter = new BinaryFormatter ();
		Stream stream = new FileStream (saveFile, FileMode.Create, FileAccess.Write, FileShare.None);
		formatter.Serialize (stream, intBlocksTown);
=======
		saveFile += "Town1.bin";
		IFormatter formatter = new BinaryFormatter ();
		Stream stream = new FileStream (saveFile, FileMode.Create, FileAccess.Write, FileShare.None);
		formatter.Serialize (stream, blocksTown);
>>>>>>> origin/master
		stream.Close ();
	}

	public string SaveLocation(string path) {
		string saveLocation = path;

		if (!Directory.Exists (saveLocation))
			Directory.CreateDirectory (saveLocation);

		return saveLocation;
	}
}
