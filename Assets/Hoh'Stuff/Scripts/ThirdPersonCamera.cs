﻿using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	public bool lockCursor;
	public float mouseSensitivity = 10;
	public Transform target;
	public float dstFromTarget = 2;
	public Vector2 yawMinMax = new Vector2 (-90, 90);
	public Vector2 pitchMinMax = new Vector2 (-45, 35);

	public float rotationSmoothTime = .12f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw;
	float pitch;

	void Start() {
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	void LateUpdate () {
		yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
		yaw = Mathf.Clamp (yaw, yawMinMax.x, yawMinMax.y);
		pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp (pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp (currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;

		transform.position = target.position - transform.forward * dstFromTarget;

	}

}