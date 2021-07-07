using UnityEngine;

namespace vmp1r3.Pottery.Demo
{
	public sealed class RotateAround : MonoBehaviour
	{
		[SerializeField] private float speed;

		private void Update() => transform.Rotate(Time.deltaTime * speed * Vector3.up);
	}
}