// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: plasticblock
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/plasticblock/Pottery)
// ----------------------------------------------------------------------- 

// NOTE: placeholder script for Demo

using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlasticBlock.Pottery.Demo
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
