using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Block {

	protected World world;

	protected int blockX;
	protected int blockY;
	protected int blockZ;

	public enum Direction { north, east, south, west, up, down };

	public int[] material = {0,0,0,0,0,0};

	public bool changed = true;

	//Constructor
	public Block() {
	}

	public void SetWorld(World w, int blockX, int blockY, int blockZ){
		world = w;
		this.blockX = blockX;
		this.blockY = blockY;
		this.blockZ = blockZ;
	}

	public virtual void Seep(float deltaTime) {
		
	}

	public virtual void RegisterForUpdate() {
		if (world != null)
			world.GetChunk (blockX, blockY, blockZ).SeepBlocks.Add (this);
	}

	public virtual void UnregisterForUpdate()
	{
		if (world != null)
			world.GetChunk (blockX, blockY, blockZ).SeepBlocks.Remove (this);
	}

	public virtual MeshData GetBlockdata(Chunk chunk, int x, int y, int z, MeshData meshData) {

		meshData.useRenderDataForCol = true;

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

	protected virtual MeshData FaceDataUp (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

		meshData.AddQuadTriangles (material[0]);
		meshData.uv.AddRange (FaceUVs (Direction.up));

		return meshData;
	}

	protected virtual MeshData FaceDataDown (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

		meshData.AddQuadTriangles (material[1]);
		meshData.uv.AddRange (FaceUVs (Direction.down));

		return meshData;
	}

	protected virtual MeshData FaceDataNorth (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

		meshData.AddQuadTriangles (material[2]);
		meshData.uv.AddRange (FaceUVs (Direction.north));

		return meshData;
	}

	protected virtual MeshData FaceDataSouth (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

		meshData.AddQuadTriangles (material[3]);
		meshData.uv.AddRange (FaceUVs (Direction.south));

		return meshData;
	}

	protected virtual MeshData FaceDataWest (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

		meshData.AddQuadTriangles (material[5]);
		meshData.uv.AddRange (FaceUVs (Direction.west));

		return meshData;
	}

	protected virtual MeshData FaceDataEast (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

		meshData.AddQuadTriangles (material[4]);
		meshData.uv.AddRange (FaceUVs (Direction.east));

		return meshData;
	}

	/*
	 * function bool IsTransparent() : function for getting the transparent of a block's face
	 * Input : Direction direction
	 * @direction : the direction of the face
	 * Output : true if the face is NOT transparent, else false
	 */
	public virtual bool IsTransparent (Direction direction) {
		switch (direction) {
			case Direction.north:
				return true;
			case Direction.east:
				return true;
			case Direction.south:
				return true;
			case Direction.west:
				return true;
			case Direction.up:
				return true;
			case Direction.down:
				return true;
		}

		return false;
	}

	public virtual Vector2[] FaceUVs(Direction direction) {
		Vector2[] UVs = new Vector2[4];

		UVs [0] = new Vector2 (1,0);
		UVs [1] = new Vector2 (1,1);
		UVs [2] = new Vector2 (0,1);
		UVs [3] = new Vector2 (0,0);

		return UVs;
	}

	/*
	 * function void SetMaterial : set the face material of the block to the same material
	 * Input : int materialNumber
	 * @materialNumber : the material number (See Unity)
	 * Output : void
	 * 
	 */
	public void SetMaterial(int materialNumber) {
		for (int i = 0; i < 6; i++) {
			material [i] = materialNumber;
		}
	}

	/*
	 * function void SetMaterial : set all faces material
	 * Input : int materialSide, int materialUp, int materialDown
	 * @materialSide : the material number to the side face
	 * @materialUp : the material number to the up face
	 * @materialDown : the material number to the down face
	 * Output : void
	 * 
	 */
	public void SetMaterial(int materialSide, int materialUp, int materialDown) {
		material [0] = materialUp;
		material [1] = materialDown;
		for (int i = 2; i < 6; i++) {
			material [i] = materialSide;
		}
	}
}
