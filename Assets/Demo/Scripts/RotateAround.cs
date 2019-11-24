// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Jasur "vmp1r3" Sadikov
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/vmp1r3/Pottery)
// ----------------------------------------------------------------------- 

// NOTE: placeholder script for Demo

using UnityEngine;

namespace vmp1r3.Pottery.Demo
{
	public sealed class RotateAround : MonoBehaviour
	{
		[SerializeField]
		private float _speed;

		private void Update()
		{
			transform.Rotate(Vector3.up * _speed * Time.deltaTime);
		}
	}
}