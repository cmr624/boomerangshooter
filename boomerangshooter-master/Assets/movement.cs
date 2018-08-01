using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

	private Animator animator;

	private bool isStunned;
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
		isStunned = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "bullet")
		{
			if (!col.gameObject.GetComponent<State>().state)
			{
				animator.SetTrigger("stun");
				isStunned = true;
			}
			else if (col.gameObject.GetComponent<State>().state && isStunned)
			{
				animator.SetTrigger("dead");
			}
			else
			{
				animator.SetTrigger("backToNormal");
			}
		}
	}
}
