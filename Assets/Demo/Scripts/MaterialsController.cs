using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pottery.Demo
{
	public sealed class MaterialsController : MonoBehaviour
	{
		public new MeshRenderer renderer;

		public Dropdown dropdown;

		public Material[] materials;

		public void SetMaterial()
		{
			int i = dropdown.value;
			renderer.sharedMaterial = materials[i];
		}
	}
}