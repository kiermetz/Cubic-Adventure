using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockStoneBricks : Block {

	public BlockStoneBricks() :base() {
		SetMaterial (9);
	}

	public override bool IsTransparent(Direction direction)
	{
		return true;
	}

}
