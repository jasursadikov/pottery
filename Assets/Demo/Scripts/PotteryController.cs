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
	public sealed class PotteryController : MonoBehaviour
	{
		public PotteryGenerator generator;

		public Slider faces;
		public Text facesText;

		public Slider heightSegments;
		public Text heightSegmentsText;

		public Slider height;
		public Text hieghtText;

		private void Start()
		{
			SetFaces();
			SetHeightSegments();
			SetHieght();
		}

		public void SetFaces()
		{
			generator.Faces = (int) faces.value;
			facesText.text = string.Format("Faces: {0}", (int) faces.value);
			generator.Assemble();
		}
		
		public void SetHeightSegments()
		{
			generator.HeightSegments = (int) heightSegments.value;
			heightSegmentsText.text = string.Format("Height Segments: {0}", (int)heightSegments.value);
			generator.Assemble();
		}

		public void SetHieght()
		{
			generator.Height = height.value;
			hieghtText.text = string.Format("Height: {0}", height.value);
			generator.Assemble();
		}
	}
}
