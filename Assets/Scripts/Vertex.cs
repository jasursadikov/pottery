// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Jasur "vmp1r3" Sadikov
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/vmp1r3/Pottery)
// ----------------------------------------------------------------------- 

using UnityEngine;

namespace vmp1r3.Pottery
{
	/// <summary>
	/// Vertex.
	/// </summary>
	public struct Vertex
	{
		/// <summary>
		/// Vertex.
		/// </summary>
		public Vertex(Vector3 position, Vector3 normal, int index)
		{
			this.position = position;
			this.normal = normal;
			this.index = index;
		}

		/// <summary>
		/// Vertex position in space.
		/// </summary>
		public Vector3 position;

		/// <summary>
		/// Vertex normal.
		/// </summary>
		public Vector3 normal;

		/// <summary>
		/// Index in mesh.
		/// </summary>
		public int index;
	}
}