using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockAir : Block {

	public BlockAir() : base() {
	}

	public override MeshData GetBlockdata(Chunk chunk, int x, int y, int z, MeshData meshData) {
		return meshData;
	}

	public override bool IsTransparent (Direction direction) {
		// The block is completely transparent
		return false;
	}
}
