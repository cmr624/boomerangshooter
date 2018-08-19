using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
	
	public float comboTimer = 3f;
    [HideInInspector]
    public int comboNum = 0;
    private SpawnEnemiesInWaves spawner;

	// Use this for initialization
	void Start () 
	{
        spawner = GetComponent<SpawnEnemiesInWaves>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        //if (spawner.state != SpawnEnemiesInWaves.SpawnState.WAITING)
        //{
            comboTimer -= Time.deltaTime;
        //}
		if (comboTimer < 0)
		{
			comboNum = 0;
		}
	}
}
