/* 
 * Runtime Pottery creation kit.
 * by PlasticBlock.
 * https://github.com/PlasticBlock
 * Skype: PlasticBlock
 * E-mail: contact@plasticblock.xyz, support@plasticblock.xyz
 */

using System;
using System.Runtime.Serialization;

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
		public Body(int x, int y, int heightSegments, int faces)
		{
			// Constructor validation.
			if (x <= 1 || y <= 1 || heightSegments <= 1 || faces <= 3)
				throw new IndexOutOfRangeException();

			// Creating an instance of matrix.
			_vertices = new Vertex[x,y];

			_radius = new float[heightSegments];

			// Setting minimal radius.
			for (int index = 0; index < _radius.Length; index++)
				_radius[index] = 1;
		}

		/// <summary>
		/// Vertices.
		/// </summary>
		[DataMember]
		private readonly Vertex[,] _vertices;

		/// <summary>
		/// Radius of each segment.
		/// </summary>
		[DataMember]
		private readonly float[] _radius;

		/// <summary>
		/// Count of vertical faces.
		/// </summary>
		[DataMember]
		private readonly int _faces;

		/// <summary>
		/// Count of HeightSegments.
		/// </summary>
		public int HeightSegments { get { return _radius.Length; } }
	}
}