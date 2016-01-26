﻿using UnityEngine;
using System.Collections;
using SimplexNoise;

public class TerrainGen {

	float stoneBaseHeight = 0;
	float stoneBaseNoise = 0.05f;
	float stoneBaseNoiseHeight = 4;

	float stoneMountainHeight = 1;
	float stoneMountainFrequency = 0.008f;
	float stoneMinHeight = -1;

	float dirtBaseHeight = 3;
	float dirtNoise = 0.04f;
	float dirtNoiseHeight = 3;

	float treeFrequency = 0.2f;
	int treeDensity = 3;

	float lakesFrequency = 0.010f;
	int lakesSize = 5;

	public Chunk ChunkGen(Chunk chunk, int posx, int posy, int posz){
		for (int x = posx - 3; x < posx + 3 + Chunk.chunkWidth; x++) {
			for (int z = posz - 3; z < posz + 3 + Chunk.chunkWidth; z++) {
				chunk = ChunkGenColumn (chunk, x, z, posx, posy, posz);
			}
		}
		return chunk;
	}

	public Chunk ChunkGenColumn (Chunk chunk, int x, int z, int posx, int posy, int posz) {

		int stoneHeight = Mathf.FloorToInt (stoneBaseHeight);
		stoneHeight += GetNoise (x, 0, z, stoneMountainFrequency, Mathf.FloorToInt (stoneMountainHeight), chunk.world.GetGrainOffset(0));

		if (stoneHeight < stoneMinHeight)
			stoneHeight = Mathf.FloorToInt (stoneMinHeight);

		stoneHeight += GetNoise (x, 0, z, stoneBaseNoise, Mathf.FloorToInt (stoneBaseNoiseHeight), chunk.world.GetGrainOffset(1));

		int dirtHeight = stoneHeight + Mathf.FloorToInt (dirtBaseHeight);
		dirtHeight += GetNoise (x, 100, z, dirtNoise, Mathf.FloorToInt (dirtNoiseHeight), chunk.world.GetGrainOffset(2));


		int lakesChance = GetNoise(x, 0, z, lakesFrequency, 100, chunk.world.GetGrainOffset(3));

		int treeChance = GetNoise (x, 0, z, treeFrequency, 100, chunk.world.GetGrainOffset (4));

		for(int y = posy; y < posy + Chunk.chunkHeight; y++) {

			if (y <= stoneHeight)
				SetBlock (x - posx, y - posy, z - posz, new Block (), chunk);
			else if (y <= dirtHeight) {
				if (lakesSize < lakesChance) {
					if (y == dirtHeight)
						SetBlock (x - posx, y - posy, z - posz, new BlockGrass (), chunk);
					else
						SetBlock (x - posx, y - posy, z - posz, new BlockDirt (), chunk);
					if (y == dirtHeight && treeChance < treeDensity)
						CreateTree (x - posx, y - posy + 1, z - posz, chunk);
				}
				else {

					if (y > stoneBaseHeight + stoneBaseNoiseHeight) {
						SetBlock (x - posx, y - posy, z - posz, new BlockAir (), chunk);
					} else {
						/*SetBlock (x, y, z, new BlockWater (), chunk);
						BlockWater newBlock = chunk.world.GetBlock (x, y, z) as BlockWater;
						newBlock.SetWorld (chunk.world, x, y, z);
						newBlock.RegisterForUpdate ();*/

						BlockWater newBlock = new BlockWater ();
						newBlock.SetWorld (chunk.world, x, y, z);
						//newBlock.RegisterForUpdate ();
						chunk.SeepBlocks.Add(newBlock);
						SetBlock (x - posx, y - posy, z - posz, newBlock, chunk);
					}

				}
			}
			else {
				if (lakesSize < lakesChance || y > stoneBaseHeight + stoneBaseNoiseHeight) {
					SetBlock (x - posx, y - posy, z - posz, new BlockAir (), chunk);
				}
				else {
					BlockWater newBlock = new BlockWater ();
					newBlock.SetWorld (chunk.world, x, y, z);
					chunk.SeepBlocks.Add(newBlock);
					SetBlock (x - posx, y - posy, z - posz, newBlock, chunk);
				}
			}
		}

		return chunk;
	}

	public static int GetNoise(int x, int y, int z, float scale, int max, Vector3 offset){
		return Mathf.FloorToInt ((Noise.Generate ((x + offset.x) * scale, (y + offset.y) * scale, (z + offset.z) * scale) + 1f) * (max / 2f));
	}

	public static void SetBlock (int x, int y, int z, Block block, Chunk chunk, bool replaceBlocks= false)
	{
		/*x -= chunk.pos.x;
		y -= chunk.pos.y;
		z -= chunk.pos.z;*/

		if (Chunk.InRange (x, y, z)) {
			if (replaceBlocks || chunk.blocks [x, y, z] == null)
				//chunk.SetBlock (x, y, z, block);
				chunk.blocks[x,y,z] = block;
		}
	}

	void CreateTree(int x, int y, int z, Chunk chunk) {
		// Create Leaves
		/*for (int xx = -2; xx <= 2; xx++) {
			for (int yy = 4; yy <= 8; yy++) {
				for (int zz = -2; zz <= 2; zz++) {
					SetBlock (x + xx, y + yy, z + zz, new BlockLeaves (), chunk, true);
				}
			}
		}*/

		for (int i = 0; i < 7; i++) {
			SetBlock (x - 1, y + 7 + i, z, new BlockLeaves (), chunk, true);
			SetBlock (x + 1, y + 7 + i, z, new BlockLeaves (), chunk, true);
			SetBlock (x, y + 7 + i, z - 1, new BlockLeaves (), chunk, true);
			SetBlock (x, y + 7 + i, z + 1, new BlockLeaves (), chunk, true);
		}

		for (int i = 0; i < 5; i++) {
			SetBlock (x - 1, y + 8 + i, z - 1, new BlockLeaves (), chunk, true);
			SetBlock (x + 1, y + 8 + i, z + 1, new BlockLeaves (), chunk, true);
			SetBlock (x + 1, y + 8 + i, z - 1, new BlockLeaves (), chunk, true);
			SetBlock (x - 1, y + 8 + i, z + 1, new BlockLeaves (), chunk, true);
		}

		for (int i = 0; i < 4; i++) {
			SetBlock (x, y + 10 + i, z, new BlockLeaves (), chunk, true);

			SetBlock (x - 1, y + 8 + i, z + 2, new BlockLeaves (), chunk, true);
			SetBlock (x, y + 8 + i, z + 2, new BlockLeaves (), chunk, true);
			SetBlock (x + 1, y + 8 + i, z + 2, new BlockLeaves (), chunk, true);
			SetBlock (x - 1, y + 8 + i, z - 2, new BlockLeaves (), chunk, true);
			SetBlock (x, y + 8 + i, z - 2, new BlockLeaves (), chunk, true);
			SetBlock (x + 1, y + 8 + i, z - 2, new BlockLeaves (), chunk, true);

			SetBlock (x + 2, y + 8 + i, z + 1, new BlockLeaves (), chunk, true);
			SetBlock (x + 2, y + 8 + i, z, new BlockLeaves (), chunk, true);
			SetBlock (x + 2, y + 8 + i, z - 1, new BlockLeaves (), chunk, true);
			SetBlock (x - 2, y + 8 + i, z + 1, new BlockLeaves (), chunk, true);
			SetBlock (x - 2, y + 8 + i, z, new BlockLeaves (), chunk, true);
			SetBlock (x - 2, y + 8 + i, z - 1, new BlockLeaves (), chunk, true);
		}

		for (int i = 0; i < 2; i++) {
			SetBlock (x - 1, y + 9 + i, z + 3, new BlockLeaves (), chunk, true);
			SetBlock (x, y + 9 + i, z + 3, new BlockLeaves (), chunk, true);
			SetBlock (x + 1, y + 9 + i, z + 3, new BlockLeaves (), chunk, true);
			SetBlock (x + 2, y + 9 + i, z + 2, new BlockLeaves (), chunk, true);

			SetBlock (x - 1, y + 9 + i, z - 3, new BlockLeaves (), chunk, true);
			SetBlock (x, y + 9 + i, z - 3, new BlockLeaves (), chunk, true);
			SetBlock (x + 1, y + 9 + i, z - 3, new BlockLeaves (), chunk, true);
			SetBlock (x - 2, y + 9 + i, z - 2, new BlockLeaves (), chunk, true);

			SetBlock (x + 3, y + 9 + i, z - 1, new BlockLeaves (), chunk, true);
			SetBlock (x + 3, y + 9 + i, z, new BlockLeaves (), chunk, true);
			SetBlock (x + 3, y + 9 + i, z + 1, new BlockLeaves (), chunk, true);
			SetBlock (x - 2, y + 9 + i, z + 2, new BlockLeaves (), chunk, true);

			SetBlock (x - 3, y + 9 + i, z - 1, new BlockLeaves (), chunk, true);
			SetBlock (x - 3, y + 9 + i, z, new BlockLeaves (), chunk, true);
			SetBlock (x - 3, y + 9 + i, z + 1, new BlockLeaves (), chunk, true);
			SetBlock (x + 2, y + 9 + i, z - 2, new BlockLeaves (), chunk, true);
		}

		//Create Trunk
		for (int yy = 0; yy < 10; yy++) {
			SetBlock (x, y + yy, z, new BlockWood (), chunk, true);
		}
	}
}
