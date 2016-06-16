using UnityEngine;
using System.Collections;

public struct Node {

	public int x, y, z, cost, heuristic;

	public Node(int x_, int y_, int z_) {
		x = x_;
		y = y_;
		z = z_;
		cost = 0;
		heuristic = 0;
	}

	public Node(int x_, int y_, int z_, int cost_, int heuristic_) {
		x = x_;
		y = y_;
		z = z_;
		cost = cost_;
		heuristic = heuristic_;
	}
}
