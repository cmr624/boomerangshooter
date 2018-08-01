using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	public float speed;

	private Animator animator;

	private bool isMoving;
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;

        if (Input.GetAxis("Horizontal") != 0)
        {
            isMoving = true;
            pos.x = speed * Time.deltaTime * Input.GetAxis("Horizontal");
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;
            pos.y = speed * Time.deltaTime * Input.GetAxis("Vertical");
        }

        
		if (isMoving)
		{
			animator.SetTrigger("move");
		}
		else
		{
			animator.SetTrigger("idle");
		}

		transform.position = pos;
		FaceMouse ();
	}


	void FaceMouse()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
		Vector2 direction = new Vector2 (mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
		transform.right = direction;	
	}


	void OnCollisionEnter2D(Collision2D col)
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
