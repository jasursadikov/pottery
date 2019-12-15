// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Jasur "vmp1r3" Sadikov
// E-mail: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/vmp1r3/Pottery)
// ----------------------------------------------------------------------- 

using UnityEngine;

// TODO: change from moving from center to delta.

namespace vmp1r3.Pottery.Demo
{
	/// <summary>
	/// Controls pottery from player's input
	/// </summary>
	public sealed class InputController : MonoBehaviour
	{
		[SerializeField]
		private Camera _camera;

		[SerializeField]
		private PotteryGenerator _pottery;

		[SerializeField]
		private GameObject _selector;

		private Ray _ray;

		private RaycastHit _hit;

		private bool _isEnabled;

		private int _selectedSegment;

		private void FixedUpdate()
		{
			var prevPosition = _hit.point;

#if UNITY_ANDROID
			// for mobile platforms
			if (Input.touchCount == 0)
			{
				_isEnabled = false;
				return;
			}
			_ray = camera.ScreenPointToRay(Input.touches[0].position);
#else
			// for standalone and web

			// converting mouse position into ray
			_ray = _camera.ScreenPointToRay(Input.mousePosition);
			// editing on holding mouse button
			if (!Input.GetMouseButton(0))
				_isEnabled = false;
#endif
			if (Physics.Raycast(_ray, out _hit))
			{
				if (_hit.collider.gameObject.layer == 5)
					return;

				// showing selector
				_selector.SetActive(true);

				// calculating direction
				var delta = _isEnabled ? _hit.point.x - prevPosition.x : 0;

				if (!_isEnabled)
				{
					// finding selected segment 
					for (int i = 0; i < _pottery.meshData.heightSegments; i++)
					{
						if (!(_hit.point.y > i * _pottery.meshData.height - _pottery.meshData.height / 2) || !(_hit.point.y < (i + 1) * _pottery.meshData.height - _pottery.meshData.height / 2))
							continue;

						_selectedSegment = i;
						break;
					}
					_isEnabled = true;
				}

				_selector.transform.position = new Vector3(0, _pottery.meshData.height * _selectedSegment, 0);
				_selector.transform.localScale = new Vector3(2.25f, _pottery.meshData.height / 2f, 2.25f);
				
				_pottery.meshData.radius[_selectedSegment] += delta;
				_pottery.meshData.radius[_selectedSegment] = Mathf.Clamp01(_pottery.meshData.radius[_selectedSegment]);

				return;
			}

			// hiding selector
			_selector.SetActive(false);
			_isEnabled = false;
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawRay(_ray);

			Gizmos.color = Color.green;
			Gizmos.DrawSphere(_hit.point, 0.2f);
		}
	}
}