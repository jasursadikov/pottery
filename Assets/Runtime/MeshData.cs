// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Jasur "vmp1r3" Sadikov
// E-mail: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/vmp1r3/Pottery)
// ----------------------------------------------------------------------- 

using System;
using UnityEngine;

namespace vmp1r3.Pottery
{
	/// <summary>
	/// Pottery data container.
	/// </summary>
	[Serializable]
	public sealed class MeshData : ISerializationCallbackReceiver
	{
		/// <summary>
		/// Pottery data container.
		/// </summary>
		public MeshData() { }

		/// <summary>
		/// Pottery data container.
		/// </summary>
		public MeshData(int faces, int heightSegments, float height)
		{
			// Constructor validation.
			if (faces <= 1 || heightSegments <= 1)
				throw new IndexOutOfRangeException();

			this.heightSegments = heightSegments;
			this.faces = faces;
			this.height = height;

			// Creating an instance of matrix.
			Vertices = new Vertex[faces, heightSegments];
			radius = new float[heightSegments];

			// Setting minimal radius.
			for (int index = 0; index < radius.Length; index++)
				radius[index] = 1f;
		}

		public float[] radius;

		public float height;

		public int heightSegments;

		public int faces;

		/// <summary>
		/// Vertices 2D array. Contains all vertices data.
		/// </summary>
		public Vertex[,] Vertices { get; private set; }
		
		/// <summary>
		/// Updating all vertices. Generating by radius.
		/// </summary>
		public void UpdateVertices()
		{
			// Filling vertices 2D array
			for (int i = 0, y = 0; y < heightSegments; y++)
				for (int x = 0; x < faces; i++, x++)
				{
					// Building circle by using Cos and Sin, for posX and posZ.
					var posX = Mathf.Cos(Mathf.PI * 2f / (faces - 1) * x);
					var posY = y * height;
					var posZ = Mathf.Sin(Mathf.PI * 2f / (faces - 1) * x);

					var position = new Vector3(posX * radius[y], posY, posZ * radius[y]);
					var normal = position.normalized;

					Vertices[x, y] = new Vertex(position, normal, i);
				}
		}

		/// <summary>
		/// Converting vertices to Vector3 array.
		/// </summary>
		/// <returns>Converted vertices array.</returns>
		public Vector3[] VerticesToPositionArray()
		{
			// Getting value from vertices[x, y].position.
			Vector3[] result = new Vector3[heightSegments * faces];
			for (int i = 0, y = 0; y < heightSegments; y++)
				for (int x = 0; x < faces; i++, x++)
					result[i] = Vertices[x, y].position;

			return result;
		}

		/// <summary>
		/// Converts vertices into <see cref="Vector3"/> array.
		/// </summary>
		/// <returns>Converted vertices array.</returns>
		public Vector3[] VerticesToNormalsArray()
		{
			// Getting value from vertices[x, y].normal. 
			Vector3[] result = new Vector3[heightSegments * faces];
			for (int i = 0, y = 0; y < heightSegments; y++)
				for (int x = 0; x < faces; i++, x++)
					result[i] = Vertices[x, y].position + Vertices[x, y].normal;

			return result;
		}

		/// <inheritdoc />
		void ISerializationCallbackReceiver.OnBeforeSerialize() { }

		/// <inheritdoc />
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			Vertices = new Vertex[faces, heightSegments];
			UpdateVertices();
		}
	}
}