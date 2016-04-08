using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockWoodPlanks : Block {

	public BlockWoodPlanks() :base() {
		SetMaterial (8);
	}

	public override bool IsTransparent(Direction direction)
	{
		return true;
	}

}
