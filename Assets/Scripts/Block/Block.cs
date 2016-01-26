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

	public struct Tile {public int x; public int y;}
	const float tileSize = 0.25f;

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

		meshData.AddQuadTriangles ();
		meshData.uv.AddRange (FaceUVs (Direction.up));

		return meshData;
	}

	protected virtual MeshData FaceDataDown (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

		meshData.AddQuadTriangles ();
		meshData.uv.AddRange (FaceUVs (Direction.down));

		return meshData;
	}

	protected virtual MeshData FaceDataNorth (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

		meshData.AddQuadTriangles ();
		meshData.uv.AddRange (FaceUVs (Direction.north));

		return meshData;
	}

	protected virtual MeshData FaceDataSouth (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

		meshData.AddQuadTriangles ();
		meshData.uv.AddRange (FaceUVs (Direction.south));

		return meshData;
	}

	protected virtual MeshData FaceDataWest (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

		meshData.AddQuadTriangles ();
		meshData.uv.AddRange (FaceUVs (Direction.west));

		return meshData;
	}

	protected virtual MeshData FaceDataEast (Chunk chunk, int x, int y, int z, MeshData meshData) {
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
		meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

		meshData.AddQuadTriangles ();
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

	public virtual Tile TexturePosition(Direction direction)
	{
		Tile tile = new Tile ();
		tile.x = 0;
		tile.y = 0;

		return tile;
	}

	public virtual Vector2[] FaceUVs(Direction direction) {
		Vector2[] UVs = new Vector2[4];
		Tile tilePos = TexturePosition (direction);

		UVs [0] = new Vector2 (tileSize * tilePos.x + tileSize, tileSize * tilePos.y);
		UVs [1] = new Vector2 (tileSize * tilePos.x + tileSize, tileSize * tilePos.y + tileSize);
		UVs [2] = new Vector2 (tileSize * tilePos.x, tileSize * tilePos.y + tileSize);
		UVs [3] = new Vector2 (tileSize * tilePos.x, tileSize * tilePos.y);

		return UVs;
	}
}
