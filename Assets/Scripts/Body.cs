/* 
 * Runtime Pottery creation kit.
 * by PlasticBlock.
 * https://github.com/PlasticBlock
 * Skype: PlasticBlock
 * E-mail: contact@plasticblock.xyz, support@plasticblock.xyz
 */

using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Pottery
{
	/// <summary>
	/// Body. Contains mesh data.
	/// </summary>
	[DataContract]
	[Serializable]
	public sealed class Body
	{
		/// <summary>
		/// Body. Contains mesh data.
		/// </summary>
		public Body(int x, int y, float height)
		{
			// Constructor validation.
			if (x <= 1 || y <= 1)
				throw new IndexOutOfRangeException();
			// Creating an instance of matrix.
			vertices = new Vertex[x,y];
			radius = new float[y];
			this.height = height;
			x++;

			// Setting minimal radius.
			for (int index = 0; index < radius.Length; index++)
				radius[index] = 1f;
		}

		/// <summary>
		/// Vertices.
		/// </summary>
		[DataMember]
		public readonly Vertex[,] vertices;

		/// <summary>
		/// Radius of each segment.
		/// </summary>
		[DataMember]
		public readonly float[] radius; // TODO: encapsulate array

		/// <summary>
		/// Distance between two height segments.
		/// </summary>
		[DataMember]
		public readonly float height;

		/// <summary>
		/// Count of HeightSegments.
		/// </summary>
		public int HeightSegments { get { return vertices.GetLength(1); } }

		/// <summary>
		/// Faces.
		/// </summary>
		public int Faces { get { return vertices.GetLength(0); } }
		
		/// <summary>
		/// Updating all vertices. Generating by radius.
		/// </summary>
		public void UpdateVertices()
		{
			for (int i = 0, y = 0; y < vertices.GetLength(1); y++)
				for (int x = 0; x < vertices.GetLength(0); i++, x++)
				{
					float posX = Mathf.Cos(Mathf.PI * 2f / (Faces - 1) * x);
					float posY = y * height;
					float posZ = Mathf.Sin(Mathf.PI * 2f / (Faces - 1) * x);

					Vector3 position = new Vector3(posX * radius[y], posY, posZ * radius[y]);
					Vector3 normal = new Vector3(posX * (radius[y] + 1), 0, posZ * (radius[y] + 1));

					vertices[x,y] = new Vertex(position, normal, i);
				}
		}

		/// <summary>
		/// Converting vertices to Vector3 array.
		/// </summary>
		/// <returns>Converted vertices array.</returns>
		public Vector3[] VerticesToPositionArray()
		{
			Vector3[] result = new Vector3[HeightSegments * Faces];
			for (int i = 0, y = 0; y < vertices.GetLength(1); y++)
				for (int x = 0; x < vertices.GetLength(0); i++, x++)
				{
					var vertex = vertices[x, y];
					result[i] = vertex.position;
				}

			return result;
		}

		/// <summary>
		/// Converting vertices to Vector3 array.
		/// </summary>
		/// <returns>Converted vertices array.</returns>
		public Vector3[] VerticesToNormalsArray()
		{
			Vector3[] result = new Vector3[HeightSegments * Faces];
			for (int i = 0, y = 0; y < vertices.GetLength(1); y++)
				for (int x = 0; x < vertices.GetLength(0); i++, x++)
				{
					var vertex = vertices[x, y];
					result[i] = vertex.normal;
				}

			return result;
		}
	}
}