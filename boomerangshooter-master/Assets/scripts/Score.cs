using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public TextMeshProUGUI combo;
	public TextMeshProUGUI score;
	public TextMeshProUGUI ammo;

	public float scoreNum = 0f;
	private PlayerManager pm;
	private ShootManager sm;
	private Shoot shootScript;
	private Combo comboManager;
	
	
	// Use this for initialization
	void Start ()
	{
		shootScript = GetComponent<Shoot>();
		pm = GetComponent<PlayerManager>();
		sm = GetComponent<ShootManager>();
		comboManager = GetComponent<Combo>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		combo.text = "COMBO " + comboManager.comboNum;
		String ammoString = Math.Abs(sm.allShots.Count - shootScript.ammo).ToString();
		ammo.text = "SHOTS " + ammoString;
		score.text = "SCORE " + scoreNum;
	}
}
