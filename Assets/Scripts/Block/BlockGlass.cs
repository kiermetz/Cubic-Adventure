using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockGlass : Block {

	public BlockGlass() :base() {
		SetMaterial (10);
	}

	public override bool IsTransparent(Direction direction)
	{
		return false;
	}

}
