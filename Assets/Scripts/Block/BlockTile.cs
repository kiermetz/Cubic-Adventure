using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockTile : Block {

	public BlockTile() :base() {
		SetMaterial (11);
	}

	public override bool IsTransparent(Direction direction)
	{
		return true;
	}

}
