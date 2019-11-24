// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Jasur "vmp1r3" Sadikov
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/vmp1r3/Pottery)
// ----------------------------------------------------------------------- 

// NOTE: placeholder script for Demo

using System.Collections.Generic;
using UnityEngine;

namespace vmp1r3.Pottery.Demo
{
	public sealed class CanvasController : MonoBehaviour
	{
		public static CanvasController Instance { get; private set; }

		[SerializeField]
		private int _startWindow;

		[SerializeField]
		private GameObject[] _windows;
		
		private Stack<int> _history;

		private void Awake()
		{
			_history = new Stack<int>();

			Instance = this;
			
			ShowElement(_startWindow);
		}

		public void ShowElement(int index)
		{
			_history.Push(index);
			foreach (var element in _windows)
				element.SetActive(false);

			_windows[index].SetActive(true);
		}

		public void Back()
		{
			_history.Pop();
			ShowElement(_history.Pop());
		}
	}
}
