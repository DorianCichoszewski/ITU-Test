using System;
using UnityEngine;

namespace ITUTest
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField]
		private float speed = 1;

		[SerializeField]
		private float mouseSensitivity = 1;

		[SerializeField]
		private float rotationLimit = 80f;
		
		private bool isRotating = false;
		Vector2 currentRotation = Vector2.zero;

		private void Start()
		{
			var eulerRotation = transform.rotation.eulerAngles;
			currentRotation = new Vector2(eulerRotation.y, eulerRotation.x);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				isRotating = false;
				SetCursor();
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				isRotating = !isRotating;
				SetCursor();
			}
			
			Move();
			if (isRotating)
				Rotate();
		}

		private void SetCursor()
		{
			Cursor.lockState = isRotating ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = !isRotating;
		}

		private void Move()
		{
			Vector3 input = new(
				Input.GetAxis("Horizontal"),
				Input.GetAxis("Elevation"),
				Input.GetAxis("Vertical")
			);
			Vector3 forward = transform.forward;
			forward.y = 0;
			forward.Normalize();
			forward *= input.z * speed * Time.deltaTime;
			
			Vector3 right = transform.right;
			right.y = 0;
			right.Normalize();
			right *= input.x * speed * Time.deltaTime;
			
			Vector3 up = input.y * speed * Time.deltaTime * Vector3.up;
			
			transform.Translate(forward + right + up, Space.World);
		}

		private void Rotate()
		{
			Vector2 input = new(
				Input.GetAxis("Mouse X"),
				Input.GetAxis("Mouse Y")
			);
			
			currentRotation.x += input.x * mouseSensitivity;
			currentRotation.y += input.y * mouseSensitivity;
			currentRotation.y = Mathf.Clamp(currentRotation.y, -rotationLimit, rotationLimit);
			
			var xQuat = Quaternion.AngleAxis(currentRotation.x, Vector3.up);
			var yQuat = Quaternion.AngleAxis(currentRotation.y, Vector3.left);
			
			transform.localRotation = xQuat * yQuat;
		}
	}
}