/*
 * Chunk.cs C#
 * Author : Tony SCHMITT
 * Creation : 15/10/2015
 * Last Modification : 08/01/2016
 * Last Author Modification : Tony SCHMITT
 *
 *********************************************
 *
 * Description :
 *
 * This class contains the chunk's parameters.
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour {

	public static int chunkWidth = 16;
	public static int chunkHeight = 64;
	public Block[,,] blocks = new Block[chunkWidth, chunkHeight, chunkWidth];
	public World world;
	public WorldPos pos;
	public bool update = false;
	public bool rendered = false;
	public bool autorise = true;

	MeshFilter meshFilter;
	public MeshCollider meshCollider;

	public List<Block> SeepBlocks = new List<Block> ();

	// Use this for initialization
	void Start () {
		meshFilter = gameObject.GetComponent<MeshFilter> ();
		meshCollider = gameObject.GetComponent<MeshCollider> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (update) {
			update = false;
			UpdateChunk ();
		}

		/*for (int i = 0; i < SeepBlocks.Count; i++)
			SeepBlocks [i].Seep (Time.deltaTime);*/
	}

	/*
	 * function GetBlock() : Return the block of the position x,y,z
	 * Input : int x, int y, int z
	 * @x, @y, @z : x,y,z coordonate of the block
	 * Output : A block element
	 */
	public Block GetBlock(int x, int y, int z) {
		if (InRange (x, y, z))
			return blocks [x, y, z];
		else {
			return world.GetBlock (pos.x + x, pos.y + y, pos.z + z);
		}
	}

	public static bool InRange(int x, int y, int z) {
		if (x < 0 || x >= chunkWidth || y < 0 || y >= chunkHeight || z < 0 || z >= chunkWidth)
			return false;

		return true;
	}

	public void SetBlock(int x, int y, int z, Block block) {
			if(InRange(x, y, z))
				blocks [x, y, z] = block;
			else
			world.SetBlock (pos.x + x, pos.y + y, pos.z + z, block);
	}

	/*
	 * function UpdateChunk() : Updates the chunk
	 * Input : none
	 * Output : void
	 */
	void UpdateChunk() {
		MeshData meshData = new MeshData ();

		for (int x = 0; x < chunkWidth; x++) {
			for (int y = 0; y < chunkHeight; y++) {
				for (int z = 0; z < chunkWidth; z++) {
					meshData = blocks [x, y, z].GetBlockdata (this, x, y, z, meshData);
				}
			}
		}

		RenderMesh (meshData);
		rendered = true;
	}


	/*
	 * function RenderMesh() : Send the calculated mesh information to the mesh and collision components
	 * Input : meshData
	 * @meshData : The data of the chunk mesh
	 * Output : void
	 */
	void RenderMesh(MeshData meshData) {

		meshFilter.mesh.Clear ();
		meshFilter.mesh.vertices = meshData.vertices.ToArray ();
		meshFilter.mesh.uv = meshData.uv.ToArray ();
		//filter.mesh.triangles = meshData.triangles.ToArray ();
		meshFilter.mesh.subMeshCount = 12;
		for (int i = 0; i < 12; i++) {
			meshFilter.mesh.SetTriangles (meshData.triangles[i].ToArray (), i);
		}
		meshFilter.mesh.RecalculateNormals ();

		//collision
		meshCollider.sharedMesh = null;
		Mesh mesh = new Mesh ();
		mesh.vertices = meshData.colVertices.ToArray ();
		mesh.triangles = meshData.colTriangles.ToArray ();
		mesh.RecalculateNormals ();

		meshCollider.sharedMesh = mesh;
	}

	public void SetBlockUnModified() {
		foreach (Block block in blocks)
			block.changed = false;
	}
}
