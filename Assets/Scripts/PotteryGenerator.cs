// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Jasur "vmp1r3" Sadikov
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/vmp1r3/Pottery)
// ----------------------------------------------------------------------- 

using System;
using UnityEngine;

namespace vmp1r3.Pottery
{
	/// <summary>
	/// Pottery's mesh runtime generator.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public sealed class PotteryGenerator : MonoBehaviour
	{
		private MeshFilter _filter;

		private MeshRenderer _renderer;

		private Mesh _mesh;

		private int _heightSegments;
		
		private int _faces;

		/// <summary>
		/// Pottery <see cref="MeshData"/> instance.
		/// </summary>
		[HideInInspector]
		public MeshData meshData;

		/// <summary>
		/// <see cref="MeshFilter"/> component attached to gameObject.
		/// </summary>
		public MeshFilter Filter => _filter ? _filter : _filter = GetComponent<MeshFilter>();

		/// <summary>
		/// <see cref="MeshRenderer"/> component attached to gameObject.
		/// </summary>
		public MeshRenderer MeshRenderer => _renderer ? _renderer : _renderer = GetComponent<MeshRenderer>();

		/// <summary>
		/// Count of height segments.
		/// </summary>
		public int HeightSegments
		{
			get => meshData.HeightSegments;
			set
			{
				if (value == meshData.HeightSegments)
					return;

				_heightSegments = value;
				Build();
			}
		}

		/// <summary>
		/// Faces.
		/// </summary>
		public int Faces { get => meshData.Faces;
			set
			{
				if (value == meshData.Faces)
					return;
				
				_faces = value;
				Build();
			}
		}

		/// <summary>
		/// Distance between two height segments.
		/// </summary>
		public float Height
		{
			get => meshData.Height;
			set
			{
				if (value == meshData.Height)
					return;

				meshData.Height = value; 
				Build();
			}
		}

		/// <summary>
		/// On pottery's structure update event.
		/// </summary>
		public event Action OnPotteryChange = delegate { };

		private void Awake()
		{
			// Setting default values.
			_faces = 11;
			_heightSegments = 10;
			meshData.Height = 0.25f;
			Build();

			Filter.sharedMesh = _mesh;
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
			if (_mesh == null)
			{
				_mesh = new Mesh {name = "Procedural mesh"};
				_mesh.MarkDynamic();
			}
			else
			{
				_mesh.Clear();
			}

			var prevBody = meshData;

			meshData = new MeshData(_faces, _heightSegments, prevBody.Height);

			if (prevBody == null)
				return;

			if (HeightSegments == prevBody.HeightSegments)
				meshData.Radius = prevBody.Radius;
			else
				for (int index = 0; index < meshData.Radius.Length; index++)
					meshData.Radius[index] = 0.1f;
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
			
			OnPotteryChange();
		}

#if DEBUG

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

#endif
	}
}