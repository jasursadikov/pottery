// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Plastic Block <admin@plasticblock.xyz>
// Skype: plasticblock, email: support@plasticblock.xyz
// Project: Pottery. (https://github.com/PlasticBlock/Pottery)
// ----------------------------------------------------------------------- 

using System.Collections.Generic;
using UnityEngine;

namespace Pottery.Demo
{
	public sealed class CanvasController : MonoBehaviour
	{
		private static CanvasController _instance;

		public static CanvasController GetInstance()
		{
			return _instance;
		}

		public GameObject[] canvasElements;
		
		private Stack<int> _elements;

		private void Awake()
		{
			_elements = new Stack<int>();
			_elements.Push(0);
			_instance = this; 
		}

		public void ShowElement(int index)
		{
			_elements.Push(index);
			foreach (var element in canvasElements)
				element.SetActive(false);

			canvasElements[index].SetActive(true);
		}

		public void Back()
		{
			_elements.Pop();
			ShowElement(_elements.Pop());
		}
	}
}
