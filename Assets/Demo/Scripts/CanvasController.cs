// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: plasticblock
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/plasticblock/Pottery)
// ----------------------------------------------------------------------- 

// NOTE: placeholder script for Demo

using System.Collections.Generic;
using UnityEngine;

namespace PlasticBlock.Pottery.Demo
{
	public sealed class CanvasController : MonoBehaviour
	{
		public static CanvasController Instance { get; private set; }

		[SerializeField]
		private int _defaultElement;

		[SerializeField]
		private GameObject[] _canvasElements;
		
		private Stack<int> _elements;

		private void Start()
		{
			_elements = new Stack<int>();
			Instance = this;
			ShowElement(_defaultElement);
		}

		public void ShowElement(int index)
		{
			_elements.Push(index);
			foreach (var element in _canvasElements)
				element.SetActive(false);

			_canvasElements[index].SetActive(true);
		}

		public void Back()
		{
			_elements.Pop();
			ShowElement(_elements.Pop());
		}
	}
}
