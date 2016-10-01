// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Plastic Block <admin@plasticblock.xyz>
// Skype: plasticblock, email: support@plasticblock.xyz
// Project: Pottery. (https://github.com/PlasticBlock/Pottery)
// ----------------------------------------------------------------------- 

using System;
using UnityEngine;

namespace Pottery
{
	/// <summary>
	/// Pottery runtime generator.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public sealed class PotteryGenerator : GeneratorBase
	{
		private int _heightSegments;

		/// <summary>
		/// Count of height segments.
		/// </summary>
		public int HeightSegments { get { return body.heightSegments; } set { _heightSegments = value; } }

		private int _faces;

		/// <summary>
		/// Faces.
		/// </summary>
		public int Faces { get { return body.faces; } set { _faces = value; } }

		private float _height;

		/// <summary>
		/// Distance between two height segments.
		/// </summary>
		public float Height { get { return body.height; } set { _height = value; } }

		/// <summary>
		/// Default radius of the pot.
		/// </summary>
		public float defaultRadius;

		/// <summary>
		/// Pot change event.
		/// </summary>
		public event Action OnPotteryChange = delegate { };

		/// <summary>
		/// Pottery body instance.
		/// </summary>
		[HideInInspector]
		public Body body;

		/// <summary>
		/// Mesh.
		/// </summary>
		private Mesh _mesh;
		
		private void Start()
		{
			// Setting default values.
			_faces = 11;
			_heightSegments = 10;
			_height = 0.25f;

			Assemble();
		}

		private void Update()
		{
			Generate();
		}

		/// <summary>
		/// Assemble basic components.
		/// </summary>
		public void Assemble()
		{
			_mesh = new Mesh();
			_mesh.MarkDynamic();
			_mesh.Optimize();
			_mesh.Clear();

			var prevBody = body;

			body = new Body(_faces, _heightSegments, _height);

			if (prevBody != null)
				if (HeightSegments == prevBody.heightSegments)
					body.radius = prevBody.radius;
				else
					for (int index = 0; index < body.radius.Length; index++)
						body.radius[index] = defaultRadius;

			_mesh = new Mesh {name = "Procedural mesh"};
		}

		/// <summary>
		/// Generating mesh by <seealso cref="Body"/>.
		/// </summary>
		private void Generate()
		{
			body.UpdateVertices();
			_mesh.Clear();
			_mesh.vertices = body.VerticesToPositionArray();

			int[] triangles = new int[6*body.vertices.GetLength(1) * body.vertices.GetLength(0)];

			for (int t = 0, y = 0; y < body.vertices.GetLength(1) - 1; y++)
				for (int x = 0; x < body.vertices.GetLength(0) - 1; x++)
				{
					// First right triangle.
					triangles[t++] = body.vertices[x, y].index;
					triangles[t++] = body.vertices[x, y + 1].index;
					triangles[t++] = body.vertices[x + 1, y + 1].index;
					// Second left triangle.
					triangles[t++] = body.vertices[x, y].index;
					triangles[t++] = body.vertices[x + 1, y + 1].index;
					triangles[t++] = body.vertices[x + 1, y].index;
				}

			Vector2[] uvs = new Vector2[body.vertices.GetLength(0) * body.vertices.GetLength(1)];

			for (int t = 0, y = 0; y < body.vertices.GetLength(1); y++)
				for (int x = 0; x < body.vertices.GetLength(0); x++, t++)
				{
					float uvX = 1f/body.vertices.GetLength(0) * x;
					float uvY = 1f / body.vertices.GetLength(1) * y;

					uvs[t] = new Vector2(uvX, uvY);
				}

			_mesh.normals = body.VerticesToNormalsArray();
			_mesh.triangles = triangles;
			_mesh.uv = uvs;

			Filter.mesh = _mesh;

			OnPotteryChange();
		}

#if DEBUG
		private void OnDrawGizmos()
		{
			if (_mesh == null)
				return;

			Gizmos.color = Color.red;
			Gizmos.DrawWireMesh(_mesh, transform.position, transform.rotation, transform.localScale);

			Gizmos.color = Color.cyan;

			foreach (var vertex in body.vertices)
				Gizmos.DrawSphere(vertex.position + transform.position, 0.01f * transform.localScale.magnitude);

			Gizmos.color = Color.yellow;

			foreach (var vertex in body.vertices)
				Gizmos.DrawLine(vertex.position + transform.position, vertex.normal + new Vector3(0, vertex.position.y, 0) + transform.position);
		}
#endif
	}
}