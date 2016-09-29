/* 
 * Runtime Pottery creation kit.
 * by PlasticBlock.
 * https://github.com/PlasticBlock
 * Skype: PlasticBlock
 * E-mail: contact@plasticblock.xyz, support@plasticblock.xyz
 */

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
		/// <summary>
		/// Count of height segments.
		/// </summary>
		public int heightSegments;

		/// <summary>
		/// Faces.
		/// </summary>
		public int faces;

		/// <summary>
		/// Distance between two height segments.
		/// </summary>
		public float height;

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
			_mesh.Clear();
			_mesh.MarkDynamic();
			_mesh.Optimize();

			body = new Body(faces, heightSegments, height);
			for (int index = 0; index < body.radius.Length; index++)
			{
				body.radius[index] = defaultRadius;
			}
			_mesh = new Mesh { name = "Procedural mesh" };
		}

		/// <summary>
		/// Generating mesh by <seealso cref="Body"/>.
		/// </summary>
		private void Generate()
		{
			body.UpdateVertices();

			_mesh.vertices = body.VerticesToPositionArray();

			int[] triangles = new int[6*body.HeightSegments*body.Faces];

			for (int t = 0, y = 0; y < body.vertices.GetLength(1) - 1; y++)
				for (int x = 0; x < body.vertices.GetLength(0) - 1; x++)
				{
					triangles[t++] = body.vertices[x, y].index;
					triangles[t++] = body.vertices[x, y + 1].index;
					triangles[t++] = body.vertices[x + 1, y + 1].index;

					triangles[t++] = body.vertices[x, y].index;
					triangles[t++] = body.vertices[x + 1, y + 1].index;
					triangles[t++] = body.vertices[x + 1, y].index;
				}

			_mesh.normals = body.VerticesToNormalsArray();

			_mesh.triangles = triangles;

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

			Gizmos.color = Color.yellow;

			foreach (var vertex in body.vertices)
				Gizmos.DrawLine(vertex.position + transform.position, vertex.normal + new Vector3(0, vertex.position.y, 0) + transform.position);

			Gizmos.color = Color.cyan;

			foreach (var vertex in body.vertices)
				Gizmos.DrawSphere(vertex.position + transform.position, 0.1f * transform.localScale.magnitude);

		}
#endif
	}
}