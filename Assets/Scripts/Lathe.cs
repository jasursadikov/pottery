using UnityEngine;

namespace vmp1r3.Pottery
{
	[RequireComponent(typeof(MeshFilter))]
	public sealed class Lathe : MonoBehaviour
	{
		public int faces;
		public float ringHeight;
		public int ringsCount;
		public float[] rings;

		private Mesh mesh;
		private Shape shape;

		void Awake()
		{
			shape = new Shape(faces, ringsCount, ringHeight, rings);
			GetComponent<MeshFilter>().sharedMesh = mesh = new Mesh();
			mesh.MarkDynamic();
		}

		void Update()
		{
			shape.UpdateVertices();
			
			mesh.vertices = shape.VerticesToPositionArray();

			var triangles = new int[6 * shape.vertices.GetLength(0) * shape.vertices.GetLength(1)];
			var uvs = new Vector2[shape.vertices.GetLength(0) * shape.vertices.GetLength(1)];

			for (int t = 0, y = 0; y < shape.vertices.GetLength(1) - 1; y++)
				for (int x = 0; x < shape.vertices.GetLength(0) - 1; x++)
				{
					triangles[t++] = shape.vertices[x, y].index;
					triangles[t++] = shape.vertices[x, y + 1].index;
					triangles[t++] = shape.vertices[x + 1, y + 1].index;

					triangles[t++] = shape.vertices[x, y].index;
					triangles[t++] = shape.vertices[x + 1, y + 1].index;
					triangles[t++] = shape.vertices[x + 1, y].index;
				}


			for (int t = 0, y = 0; y < shape.vertices.GetLength(1); y++)
				for (int x = 0; x < shape.vertices.GetLength(0); x++, t++)
				{
					var uvX = 1f / shape.vertices.GetLength(0) * x;
					var uvY = 1f / shape.vertices.GetLength(1) * y;

					uvs[t] = new Vector2(uvX, uvY);
				}

			mesh.triangles = triangles;
			mesh.uv = uvs;
			mesh.normals = shape.VerticesToNormalsArray();

			mesh.MarkModified();
		}

#if UNITY_EDITOR

		void OnDrawGizmosSelected()
		{
			if (mesh == null)
				return;

			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.color = Color.red;
			Gizmos.DrawWireMesh(mesh);
			Gizmos.color = Color.cyan;

			foreach (var vertex in shape.vertices)
				Gizmos.DrawSphere(vertex.position, 0.015f);
		}
		
#endif
	}
}