using UnityEngine;
using System.Collections.Generic;

public class NodeCompare : IComparer<Node> {

	public int Compare(Node n1, Node n2) {
		if (n1.heuristic < n2.heuristic)
			return -1;
		else if (n1.heuristic == n2.heuristic)
			return 0;
		else
			return 1;
	}
}
