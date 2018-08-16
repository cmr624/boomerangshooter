using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesInWaves : MonoBehaviour
{

	public enum SpawnState
	{
		SPAWNING,
		WAITING,
		COUNTING
	};
	
	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float rate;
	}

	public Wave[] waves;

	private int nextWave = 0;

	public float timeBetweenWaves = 5f;

	public float waveCountdown;

	private float searchCountdown = 1f;

	public SpawnState state = SpawnState.COUNTING;
	// Use this for initialization
	void Start ()
	{
		waveCountdown = timeBetweenWaves;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//kill all, then switch wave

		if (state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive())
			{
				WaveCompleted();
				return;
			}
			else
			{
				return;
			}
		}
		
		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;

		//wtf brackeys
		if (nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
			Debug.Log("All Waves Completed");
		}
		Debug.Log("Wave Completed");
	}
	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}
		return true;
	}
		
	IEnumerator SpawnWave(Wave wave)
	{
		Debug.Log("Spawning Wave" + wave.name);
		state = SpawnState.SPAWNING;

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}

		//now we're waiting for more enemies
		state = SpawnState.WAITING;
		yield break;
	}

	void SpawnEnemy(Transform enemy)
	{
		Instantiate(enemy, transform.position, transform.rotation);
		Debug.Log("Spawning Enemy: " + enemy.name);
	}
}
