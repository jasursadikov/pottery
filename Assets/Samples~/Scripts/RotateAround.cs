using UnityEngine;

namespace JasurSadikov.Pottery.Demo
{
	public sealed class RotateAround : MonoBehaviour
	{
		[SerializeField] float speed;

		void Update() => transform.Rotate(Time.deltaTime * speed * Vector3.up);
	}
}