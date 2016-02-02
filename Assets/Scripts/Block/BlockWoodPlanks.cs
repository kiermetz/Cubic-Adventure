using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockWoodPlanks : Block {

	public BlockWoodPlanks() :base() {
	}

	public override Tile TexturePosition(Direction direction)
	{
		Tile tile = new Tile ();

		tile.x = 3;
		tile.y = 2;
		return tile;
	}

	public override bool IsTransparent(Direction direction)
	{
		return true;
	}

}
