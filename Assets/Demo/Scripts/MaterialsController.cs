using UnityEngine;

namespace Pottery.Demo
{
	public sealed class MaterialsController : MonoBehaviour
	{
		public new MeshRenderer renderer;

		public Material[] materials;

		public void SetMaterial(int index)
		{
			renderer.sharedMaterial = materials[index];
		}
	}
}