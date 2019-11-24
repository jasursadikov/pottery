// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Jasur "vmp1r3" Sadikov
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/vmp1r3/Pottery)
// ----------------------------------------------------------------------- 

// NOTE: placeholder script for Demo

using System;
using UnityEngine;
using UnityEngine.UI;

namespace vmp1r3.Pottery.Demo
{
	public sealed class Message : MonoBehaviour
	{
		public static Message Instance { get; private set; }

		[SerializeField]
		private Text _message;

		private Action _onClose = delegate { };

		private void Awake()
		{
			Instance = this;
		}

		public void PopUp(string message, Action onClose = null)
		{
			onClose = onClose ?? delegate { };
			CanvasController.Instance.ShowElement(3);
			_message.text = message;
			_onClose = onClose;
		}

		public void Close()
		{
			CanvasController.Instance.Back();
			_message.text = "Null";
			_onClose();
			_onClose = delegate { };
		}
	}
}
