﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	//the higher the movement, the faster the camera will find player
	public float smoothSpeed = 0.125f;

	public Vector3 offset;

	void LateUpdate()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	}
}
