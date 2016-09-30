/* 
 * Runtime Pottery creation kit.
 * by PlasticBlock.
 * https://github.com/PlasticBlock
 * Skype: PlasticBlock
 * E-mail: contact@plasticblock.xyz, support@plasticblock.xyz
 */

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

		private Ray _ray;
		private RaycastHit _hit;
		private bool _isEnabled;
		private int _selectedSegment;

		private void FixedUpdate()
		{
			Vector3 prevPosition = _hit.point;

#if UNITY_ANDROID
			if (Input.touchCount == 0)
			{
				_isEnabled = false;
				return;
			}
			_ray = camera.ScreenPointToRay(Input.touches[0].position);
#else
			_ray = camera.ScreenPointToRay(Input.mousePosition);
			if (!Input.GetMouseButton(0))
			{
				_isEnabled = false;
				return;
			}
#endif

			if (Physics.Raycast(_ray, out _hit))
			{
				float dir = _isEnabled ? _hit.point.x - prevPosition.x : 0;

				if (!_isEnabled)
				{
					for (int i = 0; i < pottery.heightSegments; i++)
					{
						if (_hit.point.y > i*pottery.height - pottery.height/2 && _hit.point.y < (i + 1)*pottery.height - pottery.height/2)
						{
							_selectedSegment = i;
							break;
						}
					}
					_isEnabled = true;
				}

				if ((dir < 0 && pottery.body.radius[_selectedSegment] > 0.1f) ||
				    (dir > 0 && pottery.body.radius[_selectedSegment] < 1.25f))
				{
					pottery.body.radius[_selectedSegment] += dir;
					pottery.body.radius[_selectedSegment] = pottery.body.radius[_selectedSegment] > 0.1f
						? pottery.body.radius[_selectedSegment]
						: 0.1f;

					pottery.body.radius[_selectedSegment] = pottery.body.radius[_selectedSegment] < 1.25f
						? pottery.body.radius[_selectedSegment]
						: 0.125f;
				}
				return;
			}

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