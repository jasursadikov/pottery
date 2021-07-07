using UnityEngine;

namespace vmp1r3.Pottery
{
	public struct Vertex
	{
		public Vector3 position;
		public Vector3 normal;
		public int index;

		public Vertex(Vector3 position, Vector3 normal, int index)
		{
			this.position = position;
			this.normal = normal;
			this.index = index;
		}
	}
}