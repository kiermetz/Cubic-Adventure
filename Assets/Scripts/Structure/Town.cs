using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

[Serializable]
public class Town {

	public Block[,,] blocksTown = new Block[320, 60, 320];

	public Town() {
		Load ();
	}

	public bool Load() {
		string saveFile = "construction/Town/";
		saveFile += "Town1.bin";

		if (!File.Exists (saveFile))
			return false;

		IFormatter formatter = new BinaryFormatter ();
		FileStream stream = new FileStream (saveFile, FileMode.Open);

		//chunk.blocks = (Block[,,])formatter.Deserialize (stream);

		Block[,,] save = (Block[,,])formatter.Deserialize (stream);
		for (int x = 0; x < 320; x++) {
			for (int y = 0; y < 50; y++) {
				for (int z = 0; z < 320; z++) {
					//blocksTown [x,y,z] = save[x,y+7,z];
					if (save[x,y+7,z] is BlockStoneBricks)
						blocksTown[x,y,z] = new BlockStoneBricks();
					else if(save[x,y+7,z] is BlockAir)
						blocksTown[x,y,z] = new BlockAir();
					else if(save[x,y+7,z] is BlockGrass)
						blocksTown[x,y,z] = new BlockGrass();
					else if(save[x,y+7,z] is BlockDirt)
						blocksTown[x,y,z] = new BlockDirt();
					else if(save[x,y+7,z] is BlockGlass)
						blocksTown[x,y,z] = new BlockGlass();
					else if(save[x,y+7,z] is BlockWood)
						blocksTown[x,y,z] = new BlockWood();
					else if(save[x,y+7,z] is BlockLeaves)
						blocksTown[x,y,z] = new BlockLeaves();
					else if(save[x,y+7,z] is BlockWater)
						blocksTown[x,y,z] = new BlockWater();
					else if(save[x,y+7,z] is BlockTile)
						blocksTown[x,y,z] = new BlockTile();
					else if(save[x,y+7,z] is BlockWoodPlanks)
						blocksTown[x,y,z] = new BlockWoodPlanks();
					else if(save[x,y+7,z] is Block)
						blocksTown[x,y,z] = new Block();
				}
			}
		}

		stream.Close ();
		return true;
	}


}
