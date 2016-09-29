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
			generator.faces = (int) faces.value;
			facesText.text = string.Format("Faces: {0}", (int) faces.value);
			generator.Assemble();
		}
		
		public void SetHeightSegments()
		{
			generator.heightSegments = (int) heightSegments.value;
			heightSegmentsText.text = string.Format("Height Segments: {0}", (int)heightSegments.value);
			generator.Assemble();
		}

		public void SetHieght()
		{
			generator.height = height.value;
			hieghtText.text = string.Format("Height: {0}", height.value);
			generator.Assemble();
		}
	}
}
