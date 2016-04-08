using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockLeaves : Block {

	public BlockLeaves() :base() {
		SetMaterial (6);
	}

	public override bool IsTransparent(Direction direction)
	{
		return false;
	}
}
