// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: plasticblock
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/plasticblock/Pottery)
// ----------------------------------------------------------------------- 

using System;
using UnityEngine;

namespace PlasticBlock.Pottery
{
	/// <summary>
	/// Contains mesh data.
	/// </summary>
	[Serializable]
	public sealed class MeshData : ISerializationCallbackReceiver
	{
		/// <summary>
		/// MeshData. Contains mesh data.
		/// </summary>
		public MeshData() { }

		/// <summary>
		/// MeshData. Contains mesh data.
		/// </summary>
		public MeshData(int faces, int heightSegments, float height)
		{
			// Constructor validation.
			if (faces <= 1 || heightSegments <= 1)
				throw new IndexOutOfRangeException();

			_heightSegments = heightSegments;
			_faces = faces;

			// Creating an instance of matrix.
			Vertices = new Vertex[faces,heightSegments];
			_radius = new float[heightSegments];
			_height = height;

			// Setting minimal radius.
			for (int index = 0; index < Radius.Length; index++)
				Radius[index] = 1f;
		}

		[SerializeField]
		private float[] _radius;

		[SerializeField]
		private float _height;

		[SerializeField]
		private int _heightSegments;

		[SerializeField]
		private int _faces;

		/// <summary>
		/// Vertices 2D array. Contains all vertices data.
		/// </summary>
		public Vertex[,] Vertices { get; private set; }

		/// <summary>
		/// Array of radius for each segment.
		/// </summary>
		public float[] Radius { get { return _radius; } set { _radius = value; } }

		/// <summary>
		/// Distance between two height segments.
		/// </summary>
		public float Height { get { return _height; } set { _height = value; } }

		/// <summary>
		/// Height segments count.
		/// </summary>
		public int HeightSegments { get { return _heightSegments; } set { _heightSegments = value; } }

		/// <summary>
		/// Faces count.
		/// </summary>
		public int Faces { get { return _faces; } set { _faces = value; } }

		/// <summary>
		/// Updating all vertices. Generating by radius.
		/// </summary>
		public void UpdateVertices()
		{
			// Filling vertices 2D array (Matrix.)
			for (int i = 0, y = 0; y < Vertices.GetLength(1); y++)
				for (int x = 0; x < Vertices.GetLength(0); i++, x++)
				{
					// Building circle by using Cos and Sin, for posX and posZ.
					var posX = Mathf.Cos(Mathf.PI * 2f / (Vertices.GetLength(0) - 1) * x);
					var posY = y * Height;
					var posZ = Mathf.Sin(Mathf.PI * 2f / (Vertices.GetLength(0) - 1) * x);

					var position = new Vector3(posX * Radius[y], posY, posZ * Radius[y]);
					var normal = new Vector3(posX * (Radius[y] * 2f), posY, posZ * (Radius[y] * 2f));

					Vertices[x,y] = new Vertex(position, normal, i);
				}
		}

		/// <summary>
		/// Converting vertices to Vector3 array.
		/// </summary>
		/// <returns>Converted vertices array.</returns>
		public Vector3[] VerticesToPositionArray()
		{
			// Getting value from vertices[x, y].position.
			Vector3[] result = new Vector3[Vertices.GetLength(1) * Vertices.GetLength(0)];
			for (int i = 0, y = 0; y < Vertices.GetLength(1); y++)
				for (int x = 0; x < Vertices.GetLength(0); i++, x++)
					result[i] = Vertices[x, y].position;

			return result;
		}

		/// <summary>
		/// Converting vertices to Vector3 array.
		/// </summary>
		/// <returns>Converted vertices array.</returns>
		public Vector3[] VerticesToNormalsArray()
		{
			// Getting value from vertices[x, y].normal. 
			Vector3[] result = new Vector3[Vertices.GetLength(1) * Vertices.GetLength(0)];
			for (int i = 0, y = 0; y < Vertices.GetLength(1); y++)
				for (int x = 0; x < Vertices.GetLength(0); i++, x++)
					result[i] = Vertices[x, y].normal;

			return result;
		}

		/// <inheritdoc />
		void ISerializationCallbackReceiver.OnBeforeSerialize() { }

		/// <inheritdoc />
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			Vertices = new Vertex[_faces, _heightSegments];
			UpdateVertices();
		}
	}
}