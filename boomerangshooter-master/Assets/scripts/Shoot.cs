using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject bulletPrefab;
	public float speed;
	public float backSpeed;

	private bool ableToBePulled;

	public Transform player;

	public int ammo = 8; 

	private ShootManager sm;
	
	// Use this for initialization
	void Start ()
	{
		sm = GetComponent<ShootManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (sm.allShots.Count < ammo)
		{
			if (Input.GetButtonDown("Fire1")) 
			{
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				FireBullet(mousePosition);
			}
		}
		if (Input.GetButtonDown("Fire2")) 
		{
			ShootManager.Instance.Recall();
		}
	}
		


	public void FireBullet(Vector3 mousePosition)
	{
		GameObject bullet = (Instantiate(bulletPrefab, player.transform.position, player.transform.rotation)) as GameObject;
		bullet.GetComponent<State> ().state = false;
		bullet.GetComponent<Rigidbody2D> ().velocity = (mousePosition - player.transform.position).normalized * speed;
        bullet.GetComponent<Collider2D>().isTrigger = true;
		
		Destroy(bullet, 5f);

        // ShootManager is a manager for every bullet that is fired
        // The instance allows us to access the current ShootManager from anywhere in the project
        // This line adds the current instantiated bullet to the list of allShots
        ShootManager.Instance.allShots.Add(bullet);
    }
}
