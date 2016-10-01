// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Plastic Block <admin@plasticblock.xyz>
// Skype: plasticblock, email: support@plasticblock.xyz
// Project: Pottery. (https://github.com/PlasticBlock/Pottery)
// ----------------------------------------------------------------------- 

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UnityEngine;

namespace Pottery
{
	/// <summary>
	/// Body. Contains mesh data.
	/// </summary>
	[DataContract] // Attribute for DataContractSerializer.
	[Serializable] // Attribute for Formatters (as XmlFormatter and BinaryFormatter)
	public sealed class Body
	{
		/// <summary>
		/// Body. Contains mesh data.
		/// </summary>
		public Body() { }

		/// <summary>
		/// Body. Contains mesh data.
		/// </summary>
		public Body(int faces, int heightSegments, float height)
		{
			// Constructor validation.
			if (faces <= 1 || heightSegments <= 1)
				throw new IndexOutOfRangeException();

			this.heightSegments = heightSegments;
			this.faces = faces;

			// Creating an instance of matrix.
			vertices = new Vertex[faces,heightSegments];
			radius = new float[heightSegments];
			this.height = height;

			// Setting minimal radius.
			for (int index = 0; index < radius.Length; index++)
				radius[index] = 1f;
		}

		/// <summary>
		/// Vertices.
		/// </summary>
		[NonSerialized]
		[XmlIgnore]
		public Vertex[,] vertices;

		/// <summary>
		/// Radius of each segment.
		/// </summary>
		[DataMember]
		[XmlArray("radius")]
		[XmlArrayItem("element")]
		public float[] radius;

		/// <summary>
		/// Distance between two height segments.
		/// </summary>
		[DataMember]
		[XmlElement("height")]
		public float height;

		/// <summary>
		/// Height segments.
		/// </summary>
		[DataMember]
		[XmlElement("height_segments")]
		public readonly int heightSegments;

		/// <summary>
		/// Faces.
		/// </summary>
		[DataMember]
		[XmlElement("faces")]
		public readonly int faces;

		/// <summary>
		/// Updating all vertices. Generating by radius.
		/// </summary>
		public void UpdateVertices()
		{
			// Filling vertices 2D array (Matrix.)
			for (int i = 0, y = 0; y < vertices.GetLength(1); y++)
				for (int x = 0; x < vertices.GetLength(0); i++, x++)
				{
					// Building circle by using Cos and Sin, for posX and posZ.
					float posX = Mathf.Cos(Mathf.PI * 2f / (vertices.GetLength(0) - 1) * x);
					float posY = y * height;
					float posZ = Mathf.Sin(Mathf.PI * 2f / (vertices.GetLength(0) - 1) * x);
					// Assemling position by given value.
					Vector3 position = new Vector3(posX * radius[y], posY, posZ * radius[y]);
					// Creating normal.
					Vector3 normal = new Vector3(posX * (radius[y] + 1), 0, posZ * (radius[y] + 1));
					// Constructing vertex.
					vertices[x,y] = new Vertex(position, normal, i);
				}
		}

		/// <summary>
		/// Converting vertices to Vector3 array.
		/// </summary>
		/// <returns>Converted vertices array.</returns>
		public Vector3[] VerticesToPositionArray()
		{
			// Getting value from vertices[x, y].position.
			Vector3[] result = new Vector3[vertices.GetLength(1) * vertices.GetLength(0)];
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
			// Getting value from vertices[x, y].normal. 
			Vector3[] result = new Vector3[vertices.GetLength(1) * vertices.GetLength(0)];
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