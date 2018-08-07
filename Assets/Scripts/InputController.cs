// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: plasticblock
// Skype: plasticblock, email: contact@plasticblock.xyz
// Project: Pottery. (https://github.com/plasticblock/Pottery)
// ----------------------------------------------------------------------- 

using UnityEngine;

// TODO: change from moving from center to delta.

namespace PlasticBlock.Pottery
{
	/// <summary>
	/// Input controller. Converts touches and cursor into raycast, and edits your Pottery.
	/// </summary>
	public sealed class InputController : MonoBehaviour
	{
		/// <summary>
		/// Input camera.
		/// </summary>
		public new Camera camera;

		/// <summary>
		/// Pottery.
		/// </summary>
		public PotteryGenerator pottery;

		/// <summary>
		/// Selector. Shows what segment you are selected.
		/// </summary>
		public GameObject selector;

		private Ray _ray;

		private RaycastHit _hit;

		private bool _isEnabled;

		private int _selectedSegment;

		private void FixedUpdate()
		{
			var prevPosition = _hit.point;

#if UNITY_ANDROID
			// For mobile platforms.
			if (Input.touchCount == 0)
			{
				_isEnabled = false;
				return;
			}
			_ray = camera.ScreenPointToRay(Input.touches[0].position);
#else
			// For standalone and web.

			// Converting mouse position into ray.
			_ray = camera.ScreenPointToRay(Input.mousePosition);
			// Editing on holding mouse button.
			if (!Input.GetMouseButton(0))
				_isEnabled = false;
#endif
			if (Physics.Raycast(_ray, out _hit))
			{
				if (_hit.collider.gameObject.layer == 5)
					return;

				// Showing selector.
				selector.SetActive(true);

				// Calculating direction.
				var delta = _isEnabled ? _hit.point.x - prevPosition.x : 0;

				if (!_isEnabled)
				{
					// Finding selected segment. 
					for (int i = 0; i < pottery.meshData.HeightSegments; i++)
					{
						if (!(_hit.point.y > i * pottery.Height - pottery.Height / 2) || !(_hit.point.y < (i + 1) * pottery.Height - pottery.Height / 2))
							continue;

						_selectedSegment = i;
						break;
					}
					_isEnabled = true;
				}

				// Selector controlling.
				selector.transform.position = new Vector3(0, pottery.Height * _selectedSegment, 0);
				selector.transform.localScale = new Vector3(2.25f, pottery.Height / 2f, 2.25f);
				
				// Encapsulating radius[n] array element.
				pottery.meshData.Radius[_selectedSegment] += delta;
				pottery.meshData.Radius[_selectedSegment] = pottery.meshData.Radius[_selectedSegment] > 0f ? pottery.meshData.Radius[_selectedSegment] : 0f;
				pottery.meshData.Radius[_selectedSegment] = pottery.meshData.Radius[_selectedSegment] < 1f ? pottery.meshData.Radius[_selectedSegment] : 1f;

				return;
			}

			// Hidding selector.
			selector.SetActive(false);
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