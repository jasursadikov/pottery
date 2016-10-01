// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Plastic Block <admin@plasticblock.xyz>
// Skype: plasticblock, email: support@plasticblock.xyz
// Project: Pottery. (https://github.com/PlasticBlock/Pottery)
// ----------------------------------------------------------------------- 

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