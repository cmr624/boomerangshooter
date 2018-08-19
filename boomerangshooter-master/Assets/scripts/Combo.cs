using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
	[HideInInspector] 
	public float comboTimer = 3f;
	public int comboNum = 0;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		comboTimer -= Time.deltaTime;
		if (comboTimer < 0)
		{
			comboNum = 0;
		}
	}
}
