using System;
using UnityEngine;

namespace vmp1r3.Pottery
{
	public sealed class Shape
	{
		private readonly float[] rings;
		private readonly float ringHeight;
		private readonly int ringsCount;
		private readonly int faces;
		
		public readonly Vertex[,] vertices;

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
					var posX = Mathf.Cos(Mathf.PI * 2f / (faces - 1) * x);
					var posY = y * ringHeight;
					var posZ = Mathf.Sin(Mathf.PI * 2f / (faces - 1) * x);

					var position = new Vector3(posX * rings[y], posY, posZ * rings[y]);
					var normal = position.normalized;

					vertices[x, y] = new Vertex(position, normal, i);
				}
		}

		public Vector3[] VerticesToPositionArray()
		{
			var result = new Vector3[ringsCount * faces];
			
			for (int i = 0, y = 0; y < ringsCount; y++)
				for (int x = 0; x < faces; i++, x++)
					result[i] = vertices[x, y].position;

			return result;
		}
		
		public Vector3[] VerticesToNormalsArray()
		{
			var result = new Vector3[ringsCount * faces];
			
			for (int i = 0, y = 0; y < ringsCount; y++)
				for (int x = 0; x < faces; i++, x++)
					result[i] = vertices[x, y].position + vertices[x, y].normal;

			return result;
		}
	}
}