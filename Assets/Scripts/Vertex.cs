/* 
 * Runtime Pottery creation kit.
 * by PlasticBlock.
 * https://github.com/PlasticBlock
 * Skype: PlasticBlock
 * E-mail: contact@plasticblock.xyz, support@plasticblock.xyz
 */

using UnityEngine;

namespace Pottery
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