// Licensed under GPLv3 license or under special license
// See the LICENSE file in the project root for more information
// -----------------------------------------------------------------------
// Author: Plastic Block <admin@plasticblock.xyz>
// Skype: plasticblock, email: support@plasticblock.xyz
// Project: Pottery. (https://github.com/PlasticBlock/Pottery)
// ----------------------------------------------------------------------- 

using UnityEngine;

namespace Pottery
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
			Vector3 prevPosition = _hit.point;

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
			_ray = camera.ScreenPointToRay(Input.mousePosition); // Converting mouse position into ray.

			if (!Input.GetMouseButton(0)) // Editing on holding mouse button.
			{
				_isEnabled = false;
				// return;
			}
#endif

			if (Physics.Raycast(_ray, out _hit))
			{
				selector.SetActive(true); // Showing selector.
				float dir = _isEnabled ? _hit.point.x - prevPosition.x : 0; // Calculating direction.

				if (!_isEnabled)
				{
					// Finding selected segment. 
					for (int i = 0; i < pottery.body.heightSegments; i++)
					{
						if (_hit.point.y > i*pottery.Height - pottery.Height/2 && _hit.point.y < (i + 1)*pottery.Height - pottery.Height/2)
						{
							_selectedSegment = i;
							break;
						}
					}
					_isEnabled = true;
				}

				// Selector controlling.
				selector.transform.position = new Vector3(0, pottery.Height*_selectedSegment, 0);
				selector.transform.localScale = new Vector3(2.25f, pottery.Height/2f, 2.25f);

				// Encapsulating radius[n] array element.
				if ((dir < 0 && pottery.body.radius[_selectedSegment] > 0.1f) ||
				    (dir > 0 && pottery.body.radius[_selectedSegment] < 1.0f))
				{
					pottery.body.radius[_selectedSegment] += dir;
					pottery.body.radius[_selectedSegment] = pottery.body.radius[_selectedSegment] > 0.1f
						? pottery.body.radius[_selectedSegment]
						: 0.1f;

					pottery.body.radius[_selectedSegment] = pottery.body.radius[_selectedSegment] < 1.0f
						? pottery.body.radius[_selectedSegment]
						: 1.0f;
				}
				return;
			}

			selector.SetActive(false); // Hidding selector.
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