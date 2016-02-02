using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockTile : Block {

	public BlockTile() :base() {
	}

	public override Tile TexturePosition(Direction direction)
	{
		Tile tile = new Tile ();

		tile.x = 0;
		tile.y = 2;
		return tile;
	}

	public override bool IsTransparent(Direction direction)
	{
		return true;
	}

}
