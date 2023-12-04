using System;
using UnityEngine;

namespace JasurSadikov.Pottery
{
	public sealed class Shape
	{
		public readonly Vertex[,] vertices;

		readonly float[] rings;
		readonly float ringHeight;
		readonly int ringsCount;
		readonly int faces;

		public Shape(int faces, int ringsCount, float ringHeight, float[] rings)
		{
			if (faces < 2) throw new ArgumentOutOfRangeException(nameof(faces));
			if (ringsCount < 2) throw new ArgumentOutOfRangeException(nameof(ringsCount));
			if (rings.Length != ringsCount) throw new ArgumentException(nameof(rings));

			this.ringsCount = ringsCount;
			this.ringHeight = Mathf.Abs(ringHeight);
			this.faces = faces;
			this.rings = rings;
			
			vertices = new Vertex[faces, ringsCount];
		}
		public void UpdateVertices()
		{
			for (int i = 0, y = 0; y < ringsCount; y++)
				for (int x = 0; x < faces; i++, x++)
				{
					// Building circle by using Cos and Sin, for posX and posZ.
					float posX = Mathf.Cos(Mathf.PI * 2f / (faces - 1) * x);
					float posY = y * ringHeight;
					float posZ = Mathf.Sin(Mathf.PI * 2f / (faces - 1) * x);

					Vector3 position = new(posX * rings[y], posY, posZ * rings[y]);
					Vector3 normal = new Vector3(position.x, 0, position.z).normalized;

					vertices[x, y] = new Vertex(position, normal, i);
				}
		}
		public Vector3[] VerticesToPositionArray()
		{
			Vector3[] result = new Vector3[ringsCount * faces];
			
			for (int i = 0, y = 0; y < ringsCount; y++)
				for (int x = 0; x < faces; i++, x++)
					result[i] = vertices[x, y].position;

			return result;
		}
		public Vector3[] VerticesToNormalsArray()
		{
			Vector3[] result = new Vector3[ringsCount * faces];
			
			for (int i = 0, y = 0; y < ringsCount; y++)
				for (int x = 0; x < faces; i++, x++)
					result[i] = vertices[x, y].normal;

			return result;
		}
	}
}