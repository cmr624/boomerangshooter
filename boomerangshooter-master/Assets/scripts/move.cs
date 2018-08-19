using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class move : MonoBehaviour {

	public float speed;
    public float dash;
    public float dashcooldown = 3f;
    public TextMeshProUGUI restart;
    [HideInInspector]
    public float currentDashCooldownTime;
	private Animator animator;

    public bool restartBool = false;
	private bool isMoving;
    private bool showRestart = false;

	private Rigidbody2D rb;

	private Vector3 pos;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		pos.z = -1f;
        currentDashCooldownTime = 0f;
        restart.enabled = false;

    }

	// Update is called once per frame
	void Update () 
	{
		pos.x = speed * Input.GetAxis("Horizontal");
		pos.y = speed * Input.GetAxis("Vertical");
		rb.velocity = pos;

        currentDashCooldownTime -= Time.deltaTime;
        if((currentDashCooldownTime < 0f) & (Input.GetButtonDown("Jump")))
        {
            pos *= (dash * 100);
            rb.AddForce(pos);
            currentDashCooldownTime = dashcooldown;
        }
        if (restart.enabled & Input.GetKey((KeyCode.R)))
        {
            restart.enabled = false;
            restartBool = true;
        }
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
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Time.timeScale = .1f;
            //GAME OVER
            restart.enabled = true;
        }
    }
}
