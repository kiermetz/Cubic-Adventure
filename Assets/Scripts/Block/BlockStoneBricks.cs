﻿using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockStoneBricks : Block {

	public BlockStoneBricks() :base() {
	}

	public override Tile TexturePosition(Direction direction)
	{
		Tile tile = new Tile ();

		tile.x = 2;
		tile.y = 2;
		return tile;
	}

	public override bool IsTransparent(Direction direction)
	{
		return true;
	}

}
