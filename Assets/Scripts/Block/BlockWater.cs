using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockWater : Block {

	public int DistanceFromOrigin = 0;

	float seepInterval = 2f;
	float timer;

	public BlockWater() :base() {
	}

	public override void Seep(float deltaTime) {
		timer += deltaTime;

		if (timer > seepInterval) {
			timer = 0;

			if (DistanceFromOrigin < 8) {
				bool continueSideways = true;

				Block bottomBlock = world.GetBlock (blockX, blockY - 1, blockZ);

				if (bottomBlock != null) {
					if (bottomBlock.GetType ().Equals (typeof(BlockAir))) {
						continueSideways = false;
						world.SetBlock (blockX, blockY - 1, blockZ, new BlockWater ());

						BlockWater newBlock = world.GetBlock (blockX, blockY - 1, blockZ)  as BlockWater;

						if (newBlock != null) {
							newBlock.SetWorld (world, blockX, blockY - 1, blockZ);
							newBlock.RegisterForUpdate ();
						}
					}
					//else if(bottomBlock.GetType().Equals(typeof(BlockWater)))
						//continueSideways = false;
				}

				if(continueSideways == true) {
					Block northBlock = world.GetBlock(blockX, blockY, blockZ +1);

					if(northBlock != null) {
						if(northBlock.GetType().Equals(typeof(BlockAir))) {
							world.SetBlock(blockX, blockY, blockZ + 1, new BlockWater());
							BlockWater newBlock = world.GetBlock(blockX, blockY, blockZ + 1) as BlockWater;

							if(newBlock != null) {
								newBlock.SetWorld(world, blockX, blockY, blockZ + 1);
								newBlock.RegisterForUpdate();
								newBlock.DistanceFromOrigin = DistanceFromOrigin +1;
							}
						}
					}

					Block southBlock = world.GetBlock(blockX, blockY, blockZ - 1);

					if(southBlock != null) {
						if(southBlock.GetType().Equals(typeof(BlockAir))) {
							world.SetBlock(blockX, blockY, blockZ - 1, new BlockWater());
							BlockWater newBlock = world.GetBlock(blockX, blockY, blockZ - 1) as BlockWater;

							if(newBlock != null) {
								newBlock.SetWorld(world, blockX, blockY, blockZ - 1);
								newBlock.RegisterForUpdate();
								newBlock.DistanceFromOrigin = DistanceFromOrigin +1;
							}
						}
					}

					Block westBlock = world.GetBlock(blockX + 1, blockY, blockZ);

					if(westBlock != null) {
						if(westBlock.GetType().Equals(typeof(BlockAir))) {
							world.SetBlock(blockX + 1, blockY, blockZ, new BlockWater());
							BlockWater newBlock = world.GetBlock(blockX + 1, blockY, blockZ) as BlockWater;

							if(newBlock != null) {
								newBlock.SetWorld(world, blockX + 1, blockY, blockZ);
								newBlock.RegisterForUpdate();
								newBlock.DistanceFromOrigin = DistanceFromOrigin +1;
							}
						}
					}

					Block eastBlock = world.GetBlock(blockX - 1, blockY, blockZ);

					if(eastBlock != null) {
						if(eastBlock.GetType().Equals(typeof(BlockAir))) {
							world.SetBlock(blockX - 1, blockY, blockZ, new BlockWater());
							BlockWater newBlock = world.GetBlock(blockX - 1, blockY, blockZ) as BlockWater;

							if(newBlock != null) {
								newBlock.SetWorld(world, blockX - 1, blockY, blockZ);
								newBlock.RegisterForUpdate();
								newBlock.DistanceFromOrigin = DistanceFromOrigin +1;
							}
						}
					}
				}

			}

		}
	}

	public override MeshData GetBlockdata(Chunk chunk, int x, int y, int z, MeshData meshData) {

		meshData.useRenderDataForCol = false;

		if (!chunk.GetBlock (x, y + 1, z).IsTransparent (Direction.down)) {
			meshData = FaceDataUp (chunk, x, y, z, meshData);
		}
		if (!chunk.GetBlock (x, y - 1, z).IsTransparent (Direction.up)) {
			meshData = FaceDataDown (chunk, x, y, z, meshData);
		}
		if (!chunk.GetBlock (x, y, z + 1).IsTransparent (Direction.south)) {
			meshData = FaceDataNorth (chunk, x, y, z, meshData);
		}
		if (!chunk.GetBlock (x, y, z - 1).IsTransparent (Direction.north)) {
			meshData = FaceDataSouth (chunk, x, y, z, meshData);
		}
		if (!chunk.GetBlock (x + 1, y, z).IsTransparent (Direction.west)) {
			meshData = FaceDataEast (chunk, x, y, z, meshData);
		}
		if (!chunk.GetBlock (x - 1, y, z).IsTransparent (Direction.east)) {
			meshData = FaceDataWest (chunk, x, y, z, meshData);
		}
		return meshData;
	}

	public override Tile TexturePosition(Direction direction)
	{
		Tile tile = new Tile ();

		tile.x = 3;
		tile.y = 1;
		return tile;
	}

	public override bool IsTransparent(Direction direction)
	{
		return false;
	}
}
