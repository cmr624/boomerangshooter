using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	//the higher the movement, the faster the camera will find player
	public float smoothSpeed = 0.125f;

	public float followSpeed = 10.0f;
	public Vector3 offset;
	private Rigidbody2D rb;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	void LateUpdate()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, followSpeed * smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
		
		/*Vector2 distance = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
		distance = distance.normalized;
		distance *= (smoothSpeed * followSpeed);
		rb.velocity = distance;*/
	}
}
