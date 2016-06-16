using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Graph {

	public bool[,,] graphVerity;
	public int x,y,z,vision;
	public int width;
	public int height;

	static List<Vector3> voisin = new List<Vector3>() {
		new Vector3 (0f, -1f, 0f),
		new Vector3 (-1f, -1f -1f),
		new Vector3 (-1f, -1f, 0f),
		new Vector3 (-1f, -1f, 1f),
		new Vector3 (0f, -1f, -1f),
		new Vector3 (0f, -1f, 1f),
		new Vector3 (1f, -1f, -1f),
		new Vector3 (1f, -1f, 0f),
		new Vector3 (1f, -1f, 1f),
		new Vector3 (-1f, -1f -1f),
		new Vector3 (-1f, -1f, 0f),
		new Vector3 (-1f, 0f, 1f),
		new Vector3 (0f, 0f, -1f),
		new Vector3 (0f, 0f, 1f),
		new Vector3 (1f, 0f, -1f),
		new Vector3 (1f, 0f, 0f),
		new Vector3 (1f, 0f, 1f),
		new Vector3 (0f, 1f, 0f),
		new Vector3 (-1f, 1f -1f),
		new Vector3 (-1f, 1f, 0f),
		new Vector3 (-1f, 1f, 1f),
		new Vector3 (0f, 1f, -1f),
		new Vector3 (0f, 1f, 1f),
		new Vector3 (1f, 1f, -1f),
		new Vector3 (1f, 1f, 0f),
		new Vector3 (1f, 1f, 1f)
	};

	public Graph(int x, int y, int z, int vision) {
		this.vision = vision;
		this.x = x - vision;
		this.y = y - Mathf.FloorToInt(vision/2);
		this.z = z - vision;
		this.graphVerity = new bool[2 * vision + 1, 2*Mathf.FloorToInt(vision/2) + 1, 2 * vision + 1];
		this.width = 3;
		this.height = 3;
	}

	public void createGraph(World world) {
		for (int xx = 0; xx < 2 * this.vision + 1; xx++) {
			for (int yy = 0; yy < 2*Mathf.FloorToInt(this.vision/2) + 1; yy++) {
				for (int zz = 0; zz < 2 * this.vision + 1; zz++) {
					if (yy + y <= 0)
						this.graphVerity [xx, yy, zz] = false;
					Block block = world.GetBlock (xx + x, yy + y, zz + z);
					if (verifyMoveOnBlock (world, xx + x, yy + y, zz + z))
						this.graphVerity [xx, yy, zz] = true;
					else
						this.graphVerity [xx, yy, zz] = false;
				}
			}
		}
	}

	public bool verifyMoveOnBlock (World world, int x, int y, int z) {
		int blockW = Mathf.FloorToInt (width / 2);
		int blockH = height;


		if (world.GetBlock (x, y - 1, z) is BlockAir)
			return false;

		for (int j = 0; j <= 2 * blockW; j++) {
			for (int k = 0; k <= 2 * blockW; k++) {
				for (int i = 0; i < blockH; i++) {
					if (!(world.GetBlock (x - 1 + j, y + i, z - 1 + k) is BlockAir))
						return false;
				}
			}
		}

		return true;
	}

	public Node[] shortestWay(Node objectif, Node start) {
		NodeCompare nc = new NodeCompare ();
		/*List<Node> sort = new List<Node> ();
		sort.Add (new Node (5, 6, 0, 1));
		sort.Add (new Node (5, 7, 0, 2));
		sort.Sort (nc);*/
		Node[] nodeF = new Node[1];
		nodeF [0] = new Node(0,0,0);
		List<Node> closedList = new List<Node> ();
		List<Node> openList = new List<Node> ();
		openList.Add (start);
		openList.Sort (nc);
		while (openList.Count != 0) {
			Node u = openList [0];
			openList.RemoveAt (0);
			if (u.x == objectif.x && u.y == objectif.y && u.z == objectif.z) {
				nodeF = new Node[u.cost+1];
				nodeF = reconstitutePath (closedList, objectif, u.cost);
				break;
			} else {
				foreach (Vector3 v in voisin) {
					if (v.x + u.x >= start.x - vision && v.x + u.x <= start.x + vision && v.y + u.y >= start.y - Mathf.FloorToInt(vision/2) && v.y + u.y <= start.y + Mathf.FloorToInt(vision/2) && v.z + u.z >= start.z - vision && v.z + u.z <= start.z + vision) {
						if (graphVerity [(int)v.x + u.x - start.x + vision, (int)v.y + u.y - start.y + Mathf.FloorToInt(vision/2), (int)v.z + u.z - start.z + vision]) {
							Node vnode = new Node ((int)v.x + u.x, (int) v.y +u.y, (int)v.z + u.z, u.cost + 1, (int)u.cost + 1 + Mathf.FloorToInt (Mathf.Sqrt (Mathf.Pow (u.x + v.x - objectif.x, 2) + Mathf.Pow (u.y + v.y - objectif.y, 2) + Mathf.Pow (u.z + v.z - objectif.z, 2))));
							if (nodeExistAndHasSmallCost (vnode, openList) && nodeExistAndHasSmallHeuristic (vnode, closedList)) {
								deleteDuplicateNode (vnode.x, vnode.y, vnode.z, openList);
								openList.Add (vnode);
								openList.Sort (nc);
							}
						}
					}
				}
			}
			closedList.Add (u);
		}

		/*int[,] tab = new int[2 * vision + 1, 2 * vision + 1];

		for (int x = 0; x < 2 * vision + 1; x++) {
			for (int y = 0; y < 2 * vision + 1; y++) {
				tab [x, y] = -1;
			}
		}

		foreach (Node n in closedList) {
			if(tab [n.x - start.x + vision, n.z - start.z + vision] == -1 || tab [n.x - start.x + vision, n.z - start.z + vision] > n.heuristic)
				tab [n.x - start.x + vision, n.z - start.z + vision] = n.heuristic;
		}

		string lines = "";
		string lines2 = "";

		for (int x = 0; x < 2 * vision + 1; x++) {
			for (int y = 0; y < 2 * vision + 1; y++) {
				if (graphVerity [x, y]) {
					if (tab [x, y] < 10 && tab [x, y] >= 0)
						lines += " " + tab [x, y] + ", ";
					else
						lines += tab [x, y] + ", ";
				}
				else
					lines += "-5, ";
			}
			lines += "\r\n";
		}

		System.IO.StreamWriter file = new System.IO.StreamWriter("closedList.txt");
		file.WriteLine(lines);

		file.Close();*/

		return nodeF;
	}

	public bool nodeExistAndHasSmallCost (Node node, List<Node> list) {
		foreach (Node n in list) {
			if (n.x == node.x && n.y == node.y && n.z == node.z && n.cost < node.cost)
				return false;
		}
		return true;
	}

	public bool nodeExistAndHasSmallHeuristic (Node node, List<Node> list) {
		foreach (Node n in list) {
			if (n.x == node.x && n.y == node.y && n.z == node.z && n.heuristic < node.heuristic)
				return false;
		}
		return true;
	}

	public void deleteDuplicateNode (int x, int y, int z, List<Node> list) {
		List<Node> nodeToDelete = new List<Node> ();
		foreach (Node n in list) {
			if (n.x == x && n.y == y && n.z == z)
				nodeToDelete.Add (n);
		}
		foreach (Node n in nodeToDelete) {
			list.Remove (n);
		}
		return;
	}

	public Node[] reconstitutePath(List<Node> list, Node objectif, int cost) {
		//Debug.Log (list.Find (t => t.cost.Equals (12)).z + "objectif reelle : "+objectif.z);
		//Debug.Log (list.FindAll (t => t.x.Equals (objectif.x)).Find(t => t.z.Equals(objectif.z)).z);
		int c = cost;
		List<Node> listI = new List<Node> ();
		List<Node> listII = new List<Node> ();
		Node[] nodeF = new Node[c+1];
		nodeF[c] = new Node (objectif.x, objectif.y, objectif.z, cost, 0);
		int min = 0;
		int xN = 0, yN=0, zN = 0;

		while (c != 0) {
			c--;
			listI = list.FindAll (t => t.cost.Equals (c));

			min = -1;
			foreach (Node n in listI) {
				if (n.x >= nodeF [c + 1].x - 1 && n.x <= nodeF [c + 1].x + 1 && n.y >= nodeF [c + 1].y - 1 && n.y <= nodeF [c + 1].y + 1 && n.z >= nodeF [c + 1].z - 1 && n.z <= nodeF [c + 1].z + 1) {
					if (min <= -1 || min > n.heuristic) {
						min = n.heuristic;
						xN = n.x;
						yN = n.y;
						zN = n.z;
					}
				}
			}
			nodeF [c] = listI.FindAll (t => t.x.Equals (xN)).FindAll(t => t.y.Equals (yN)).Find (t => t.z.Equals (zN));
		}

		/*for (int i = 0; i < nodeF.Length; i++) {
			Debug.Log (i + " => X : " + nodeF [i].x + ", Y : " + nodeF [i].z);
		}*/
		return nodeF;

	}
}
