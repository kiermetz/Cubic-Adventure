using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class Serialization{

	public static string saveFolderName = "saves";

	public static string SaveLocation(string worldname) {
		string saveLocation = saveFolderName + "/" + worldname + "/world/";

		if (!Directory.Exists (saveLocation))
			Directory.CreateDirectory (saveLocation);

		return saveLocation;
	}

	public static string FileName(WorldPos chunkLocation) {
		string fileName = chunkLocation.x + "," + chunkLocation.y + "," + chunkLocation.z + ".bin";
		return fileName;
	}

	public static void SaveChunk(Chunk chunk) {
		Save save = new Save (chunk);
		if (save.blocks.Count == 0)
			return;
		
		string saveFile = SaveLocation (chunk.world.worldName);
		saveFile += FileName (chunk.pos);

		IFormatter formatter = new BinaryFormatter ();
		Stream stream = new FileStream (saveFile, FileMode.Create, FileAccess.Write, FileShare.None);
		formatter.Serialize (stream, save);
		stream.Close ();
	}

	public static bool Load(Chunk chunk) {
		string saveFile = SaveLocation (chunk.world.worldName);
		saveFile += FileName (chunk.pos);

		if (!File.Exists (saveFile))
			return false;

		IFormatter formatter = new BinaryFormatter ();
		FileStream stream = new FileStream (saveFile, FileMode.Open);

		//chunk.blocks = (Block[,,])formatter.Deserialize (stream);

		Save save = (Save)formatter.Deserialize (stream);
		foreach (var block in save.blocks) {
			chunk.blocks [block.Key.x, block.Key.y, block.Key.z] = block.Value;
		}
		
		stream.Close ();
		return true;
	}

	public static bool SaveWorld(World world) {
		StreamWriter file = new StreamWriter (saveFolderName + "/" + world.worldName + "/world.txt");
		file.WriteLine ("World : " + world.worldName);
		file.WriteLine ("Seed : " + world.seed);
		file.WriteLine ("Plane : " + world.plane);
		file.Close();
		Debug.Log ("coucou");
		return true;
	}

	public static string[] LoadWorld(String location) {
		int counter = 0;
		string line;
		string[] linesFile =  new string[3];

		StreamReader file = new StreamReader (location);
		while ((line = file.ReadLine ()) != null) {
			linesFile [counter] = line;
			counter++;
		}
		file.Close ();
		string[] returnLoad = {linesFile[0].Substring(linesFile[0].LastIndexOf(": ") + 2),
				linesFile[1].Substring(linesFile[1].LastIndexOf(": ") + 2),
			linesFile[2].Substring(linesFile[2].LastIndexOf(": ") + 2)};
		return returnLoad;
	}
}
