using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class movement : MonoBehaviour
{


	private Transform player;
	private bool isStunned;
	private bool isDead;
	private float stunnedTimer;
	private float deathTimer;

	public float comboTimer = 4f;
	
	public float score;
	public float minDist;
	public float movementSpeed;

	public GameObject manager;
	
	private Rigidbody2D rb;

	private Vector2 frozenPos;
	
	
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		stunnedTimer = 5f;
		deathTimer = .9f;
		
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
			distance = distance.normalized;
			distance *= (movementSpeed + Mathf.PerlinNoise(distance.x, distance.y));
			rb.velocity = distance;
		}
		
		if (isStunned)
		{
            //freeze the position
            //transform.position = frozenPos;
            rb.velocity = new Vector2(0f, 0f);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            stunnedTimer -= Time.deltaTime;
            
			//check if the enemy should be dead
			if (stunnedTimer < 0)
			{
				if (!isDead)
				{
					isStunned = false;
					//dev art stuff, add animator here
					GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 255f);
					//reset timer
					stunnedTimer = 5f;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
				}
			}
		}
		
		if (isDead)
		{
            deathTimer -= Time.deltaTime;
			if (deathTimer < 0)
            {
                manager.GetComponent<Score>().scoreNum += score;
                manager.GetComponent<Combo>().comboTimer = comboTimer;
                manager.GetComponent<Combo>().comboNum += 1;
                Destroy(this.gameObject);
                GameObject.FindGameObjectWithTag("Player").GetComponent<move>().currentDashCooldownTime = -1f;
                GameObject.FindGameObjectWithTag("Player").GetComponent<move>().currentDashCooldownTime = -1f;
            }
        }
	}

	void OnDestroy()
	{
		
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
				//frozenPos = transform.position;
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
