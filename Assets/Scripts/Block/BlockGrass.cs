using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BlockGrass : Block {

	public BlockGrass() : base() {
		SetMaterial (1, 2, 3);
	}

}
