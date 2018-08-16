using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

	private Animator animator;

	private Transform player;
	public bool animatorBool = false;
	private bool isStunned;
	private bool isDead;
	private float stunnedTimer;
	private float deathTimer;

	public float minDist;
	public float movementSpeed;

	private Rigidbody2D rb;

	private Vector2 frozenPos;
	
	
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		stunnedTimer = 5f;
		deathTimer = 2f;
		
		// if the enemy is in a stunned state
		isStunned = false;
		isDead = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isStunned)
		{
			Vector2 distance = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
			rb.velocity = distance;
		}
		
		if (isStunned)
		{
			transform.position = frozenPos;
			
			stunnedTimer -= Time.deltaTime;
			
			if (stunnedTimer < 0)
			{
				isStunned = false;
				GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 255f);
				stunnedTimer = 5f;
			}
		}
		
		if (isDead)
		{
			deathTimer -= Time.deltaTime;
			if (deathTimer < 0)
			{
				Destroy(this.gameObject);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (isDead)
		{
			return;
		}
		if (col.gameObject.tag == "bullet")
		{
			if (!col.gameObject.GetComponent<State>().state)
			{
			
				GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 0f);
				isStunned = true;
				frozenPos = transform.position;
			}
			else if (col.gameObject.GetComponent<State>().state && isStunned)
			{
				
				isDead = true;
				GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
			}
			else
			{	

				GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 255f);

			}
		}
	}
}
