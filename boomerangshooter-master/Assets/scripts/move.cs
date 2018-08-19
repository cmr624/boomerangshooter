using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	public float speed;

	private Animator animator;

	private bool isMoving;
	public bool animatorBool = false;

	private Rigidbody2D rb;

	private Vector3 pos;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		pos.z = -1f;
	}

	// Update is called once per frame
	void Update () 
	{
		/*
		pos = transform.position;
        if (Input.GetAxis("Horizontal") != 0)
        {
            isMoving = true;
            pos.x += speed * Time.deltaTime * Input.GetAxis("Horizontal");
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;
            pos.y += speed * Time.deltaTime * Input.GetAxis("Vertical");
        }*/

		/*float h, v;
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		if (Mathf.Abs(h) > 0.01f)
		{
			pos.x = speed;
		}
		else
		{
			pos.x = 0.0f;
		}
		if (Mathf.Abs(v) > 0.01f)
		{
			pos.y = speed;
		}
		else
		{
			pos.y = 0.0f;
		}*/
		pos.x = speed * Input.GetAxis("Horizontal");
		pos.y = speed * Input.GetAxis("Vertical");
		//rb.AddForce(pos, ForceMode2D.);
		//rb.AddForce(pos, ForceMode.VelocityChange);
		rb.velocity = pos;

		//Debug.Log(pos);

		//rb.velocity += Vector2(speed * Time.deltaTime * Input.GetAxis("horizontal"), speed * Time.deltaTime * Input.GetAxis("Vertical"));

		//FaceMouse ();
	}
	
	void FaceMouse()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
		Vector2 direction = new Vector2 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
		transform.right = direction;	
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "bullet") 
		{
			if (col.gameObject.GetComponent<State> ().state) 
			{
				//col.gameObject.GetComponent<State>().state = false;
				Destroy (col.gameObject);
			}
		}
	}
}
