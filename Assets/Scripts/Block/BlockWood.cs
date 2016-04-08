using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockWood : Block {

	public BlockWood() :base() {
		SetMaterial (4, 5, 5);
	}

	public override bool IsTransparent(Direction direction)
	{
		return true;
	}

}
