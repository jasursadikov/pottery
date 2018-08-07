// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: plasticblock
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/plasticblock/Pottery)
// ----------------------------------------------------------------------- 

// NOTE: placeholder script for Demo

using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PlasticBlock.Pottery.Demo
{
	/// <summary>
	/// Asigns UI elements value to pottery. 
	/// </summary>
	public sealed class PotteryEditingWindow : MonoBehaviour
	{
		[SerializeField]
		private PotteryGenerator _generator;

		[SerializeField]
		private MeshRenderer _renderer;

		[Header("Faces")]
		[SerializeField]
		private Slider _faces;

		[SerializeField]
		private Text _facesText;

		[Header("Height segments")]
		[SerializeField]
		private Slider _heightSegments;

		[SerializeField]
		private Text _heightSegmentsText;

		[Header("Height")]
		[SerializeField]
		private Slider _height;

		[SerializeField]
		private Text _hieghtText;

		[Header("Materials")]
		[SerializeField]
		private Dropdown _materialsSelector;

		[SerializeField]
		private Material[] _materials;
		
		// Refreshes values of controller when window is opened again.
		private void OnEnable()
		{
			_faces.value = _generator.Faces;
			_facesText.text = $"Faces: {(int) _faces.value}";

			_heightSegments.value = _generator.HeightSegments;
			_heightSegmentsText.text = $"Height Segments: {(int) _heightSegments.value}";

			_height.value = _generator.Height;
			_hieghtText.text = $"Height: {_height.value}";

			_materialsSelector.value = 0;
			_materialsSelector.options = _materials.Select(material => new Dropdown.OptionData(material.name)).ToList();
		}

		// UI control elements behaviour.

		public void SetFaces()
		{
			_generator.Faces = (int) _faces.value;
			_facesText.text = $"Faces: {(int) _faces.value}";
		}
		
		public void SetHeightSegments()
		{
			_generator.HeightSegments = (int) _heightSegments.value;
			_heightSegmentsText.text = $"Height Segments: {(int) _heightSegments.value}";
		}

		public void SetHieght()
		{
			_generator.Height = _height.value;
			_hieghtText.text = $"Height: {_height.value}";
		}

		public void SetMaterial()
		{
			_renderer.sharedMaterial = _materials[_materialsSelector.value];
		}
	}
}
