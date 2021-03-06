﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshData {

	public List<Vector3> vertices = new List<Vector3>();
	public List<Vector2> uv = new List<Vector2>();
	public List<int>[] triangles = {new List<int>(), 
		new List<int>(), new List<int>(), new List<int>(),
		new List<int>(), new List<int>(), new List<int>(),
		new List<int>(), new List<int>(), new List<int>(),
		new List<int>(), new List<int>()};

	//Collider
	public List<Vector3> colVertices = new List<Vector3>();
	public List<int> colTriangles = new List<int>();
	public bool useRenderDataForCol;

	//Constructor
	public MeshData() {
	}

	public void AddQuadTriangles(int material) {
		triangles[material].Add (vertices.Count - 4);
		triangles[material].Add (vertices.Count - 3);
		triangles[material].Add (vertices.Count - 2);
		triangles[material].Add (vertices.Count - 4);
		triangles[material].Add (vertices.Count - 2);
		triangles[material].Add (vertices.Count - 1);

		if (useRenderDataForCol) {
			colTriangles.Add (colVertices.Count - 4);
			colTriangles.Add (colVertices.Count - 3);
			colTriangles.Add (colVertices.Count - 2);

			colTriangles.Add (colVertices.Count - 4);
			colTriangles.Add (colVertices.Count - 2);
			colTriangles.Add (colVertices.Count - 1);
		}
	}

	public void AddVertex(Vector3 vertex) {
		vertices.Add (vertex);

		if (useRenderDataForCol)
			colVertices.Add (vertex);
	}
}
