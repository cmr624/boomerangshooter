using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour
{
	public TextMeshProUGUI combo;
	public TextMeshProUGUI score;
	public TextMeshProUGUI ammo;
    public TextMeshProUGUI wave;
    public TextMeshProUGUI dash;

    public TextMeshProUGUI start;
    public TextMeshProUGUI description;
    public TextMeshProUGUI restart;
    public TextMeshProUGUI waveNext;

    [HideInInspector]
    public float scoreNum = 0f;
	private PlayerManager pm;
	private ShootManager sm;
	private Shoot shootScript;
	private Combo comboManager;
    private SpawnEnemiesInWaves spawner;
    private bool onStart = true;

    private GameObject[] enemies;
	
	// Use this for initialization
	void Start ()
	{
        if (onStart)
        {
    
            start.enabled = true;
            description.enabled = true;
            ammo.enabled = false;
            combo.enabled = false;
            score.enabled = false;
            wave.enabled = false;
            waveNext.enabled = false;
            dash.enabled = false;
            restart.enabled = false;
            Time.timeScale = 0;

        }
		shootScript = GetComponent<Shoot>();
		pm = GetComponent<PlayerManager>();
		sm = GetComponent<ShootManager>();
		comboManager = GetComponent<Combo>();
        spawner = GetComponent<SpawnEnemiesInWaves>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!onStart)
        {
            wave.text = "WAVE " + (spawner.nextWave + 1);
            combo.text = "COMBO " + comboManager.comboNum;
            String ammoString = Math.Abs(sm.allShots.Count - shootScript.ammo).ToString();
            ammo.text = "SHOTS " + ammoString;
            score.text = "SCORE " + scoreNum;
            if (pm.player.GetComponent<move>().currentDashCooldownTime < 0)
            {
                dash.color = new Color(255f, 0f, 0f);
                dash.text = "DASH READY";
            }
            else
            {
                dash.color = new Color(255f, 255f, 255f);
                dash.text = "READY IN " + Mathf.Round(pm.player.GetComponent<move>().currentDashCooldownTime);
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                Time.timeScale = 1;
                start.enabled = false;
                description.enabled = false;
                waveNext.enabled = true;
                ammo.enabled = true;
                combo.enabled = true;
                score.enabled = true;
                wave.enabled = true;
                dash.enabled = true;
                onStart = false;
            }
        }
        if(pm.player.GetComponent<move>().restartBool)
        {
            SceneManager.LoadScene("newstyletest");
        }
	}
}
