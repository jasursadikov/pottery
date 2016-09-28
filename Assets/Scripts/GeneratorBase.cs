/* 
 * Runtime Pottery creation kit.
 * by PlasticBlock.
 * https://github.com/PlasticBlock
 * Skype: PlasticBlock
 * E-mail: contact@plasticblock.xyz, support@plasticblock.xyz
 */

using UnityEngine;

// TODO: Generate UVs.

namespace Pottery
{
	/// <summary>
	/// Pottery runtime generator base.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public abstract class GeneratorBase : MonoBehaviour
	{
		/// <summary>
		/// Mesh filter.
		/// </summary>
		private MeshFilter _filter;
		
		/// <summary>
		/// Mesh filter.
		/// </summary>
		public MeshFilter Filter
		{
			get { return _filter ?? (_filter = GetComponent<MeshFilter>()); }
		}

		/// <summary>
		/// Mesh renderer.
		/// </summary>
		private MeshRenderer _renderer;

		/// <summary>
		/// Mesh renderer.
		/// </summary>
		public MeshRenderer MeshRenderer
		{
			get { return _renderer ?? (_renderer = GetComponent<MeshRenderer>()); } 
		}
	}
}
