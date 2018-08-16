using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject bulletPrefab;
	public float speed;
	public float backSpeed;

	private bool ableToBePulled;

	private Animator animator;

	public bool animatorBool = false;
	// Use this for initialization
	void Start () 
	{
		if (animatorBool)
		{
			animator = GetComponent<Animator>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) 
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			FireBullet(mousePosition);
			if (animatorBool)
			{
				animator.SetTrigger("shoot");
			}
		}
		if (Input.GetButtonDown("Fire2")) {

            ShootManager.Instance.Recall();
		}
	}


	public void FireBullet(Vector3 mousePosition)
	{
		GameObject bullet = (Instantiate(bulletPrefab, transform.position, transform.rotation)) as GameObject;
		bullet.GetComponent<State> ().state = false;
		bullet.GetComponent<Rigidbody2D> ().velocity = (mousePosition - transform.position).normalized * speed;
        bullet.GetComponent<Collider2D>().isTrigger = true;
		
		Destroy(bullet, 5f);

        //ShootManager is a manager for every bullet that is fired
        //The instance allows us to access the current ShootManager from anywhere in the project
        //This line adds the current instantiated bullet to the list of allShots
        ShootManager.Instance.allShots.Add(bullet);
    }
}
