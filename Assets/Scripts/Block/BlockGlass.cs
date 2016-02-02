using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockGlass : Block {

	public BlockGlass() :base() {
	}

	public override Tile TexturePosition(Direction direction)
	{
		Tile tile = new Tile ();

		tile.x = 1;
		tile.y = 2;
		return tile;
	}

	public override bool IsTransparent(Direction direction)
	{
		return false;
	}

}
