// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Jasur "vmp1r3" Sadikov
// E-mail: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/vmp1r3/Pottery)
// ----------------------------------------------------------------------- 

using UnityEngine;

namespace vmp1r3.Pottery
{
	/// <summary>
	/// Pottery's mesh runtime generator.
	/// </summary>
	[RequireComponent(typeof(MeshFilter))]
	public sealed class PotteryGenerator : MonoBehaviour
	{
		private Mesh _mesh;

		/// <summary>
		/// Pottery <see cref="MeshData"/> instance.
		/// </summary>
		public MeshData meshData;

		private void Awake()
		{
			GetComponent<MeshFilter>().sharedMesh = _mesh = new Mesh { name = "Pottery" };

			Build();
		}

		private void Update()
		{
			Generate();
		}

		/// <summary>
		/// Build basic components.
		/// </summary>
		public void Build()
		{
			_mesh.Clear();

			var prevBody = meshData;

			meshData = meshData == null ? new MeshData() : new MeshData(meshData.faces, meshData.heightSegments, meshData.height);

			if (prevBody == null)
				return;

			if (meshData.heightSegments == prevBody.heightSegments)
				meshData.radius = prevBody.radius;
			else
				for (int index = 0; index < meshData.radius.Length; index++)
					meshData.radius[index] = 0.1f;
		}

		/// <summary>
		/// Generating mesh according to <seealso cref="MeshData"/>.
		/// </summary>
		private void Generate()
		{
			meshData.UpdateVertices();
			_mesh.Clear();
			_mesh.vertices = meshData.VerticesToPositionArray();

			int[] triangles = new int[6 * meshData.Vertices.GetLength(1) * meshData.Vertices.GetLength(0)];

			for (int t = 0, y = 0; y < meshData.Vertices.GetLength(1) - 1; y++)
				for (int x = 0; x < meshData.Vertices.GetLength(0) - 1; x++)
				{
					// First right triangle.
					triangles[t++] = meshData.Vertices[x, y].index;
					triangles[t++] = meshData.Vertices[x, y + 1].index;
					triangles[t++] = meshData.Vertices[x + 1, y + 1].index;
					// Second left triangle.
					triangles[t++] = meshData.Vertices[x, y].index;
					triangles[t++] = meshData.Vertices[x + 1, y + 1].index;
					triangles[t++] = meshData.Vertices[x + 1, y].index;
				}

			Vector2[] uvs = new Vector2[meshData.Vertices.GetLength(0) * meshData.Vertices.GetLength(1)];

			for (int t = 0, y = 0; y < meshData.Vertices.GetLength(1); y++)
				for (int x = 0; x < meshData.Vertices.GetLength(0); x++, t++)
				{
					float uvX = 1f / meshData.Vertices.GetLength(0) * x;
					float uvY = 1f / meshData.Vertices.GetLength(1) * y;

					uvs[t] = new Vector2(uvX, uvY);
				}

			_mesh.triangles = triangles;
			_mesh.uv = uvs;
			_mesh.normals = meshData.VerticesToNormalsArray();
		}

#if DEBUG

		// ReSharper disable Unity.InefficientPropertyAccess
		private void OnDrawGizmosSelected()
		{
			if (_mesh == null)
				return;

			Gizmos.color = Color.red;
			Gizmos.DrawWireMesh(_mesh, transform.position, transform.rotation, transform.localScale);
			Gizmos.color = Color.cyan;

			foreach (var vertex in meshData.Vertices)
				Gizmos.DrawSphere(vertex.position + transform.position, 0.01f * transform.localScale.magnitude);

			// TODO: Output normals
			Gizmos.color = Color.yellow;

			foreach (var vertex in meshData.Vertices)
			{
				var start = vertex.position + transform.position;
				var normal = vertex.normal + transform.position;
				Gizmos.DrawLine(start, normal);
			}
		}
		// ReSharper restore Unity.InefficientPropertyAccess

#endif
	}
}