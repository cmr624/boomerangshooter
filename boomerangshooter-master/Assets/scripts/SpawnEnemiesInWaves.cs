using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemiesInWaves : MonoBehaviour
{
    public TextMeshProUGUI nextWaveDescription;
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
		public GameObject enemy;
		public int count;
		public float rate;
        public int ammo;
        public Vector2 speedRange;
        public string nextWaveDescription;
	}

	public Wave[] waves;
    [HideInInspector]
	public int nextWave = 0;

	public float timeBetweenWaves = 5f;

	public float waveCountdown;

	private float searchCountdown = 1f;

    public float bounds = 10f;
    private Shoot shoot;
    private bool start;
    [HideInInspector]
	public SpawnState state = SpawnState.COUNTING;
	// Use this for initialization
	void Start ()
	{
		waveCountdown = timeBetweenWaves;
        start = true;
        shoot = GetComponent<Shoot>();

    }
	
	// Update is called once per frame
	void Update () 
	{
		//kill all, then switch wave
        if (start)
        {
            nextWaveDescription.text = "3 seconds until wave starts";
        }
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
                shoot.ammo = waves[nextWave].ammo;
                nextWaveDescription.enabled = false;
                StartCoroutine(SpawnWave(waves[nextWave]));
                start = false;

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
        nextWaveDescription.enabled = true;
        nextWaveDescription.text = waves[nextWave].nextWaveDescription;
        nextWave += 1;
        transform.position = NewLocation();
		//wtf brackeys
		if (nextWave + 1 > waves.Length - 1)
		{
			nextWave = 0;
			Debug.Log("All Waves Completed");
		}
		Debug.Log("Wave Completed");

	}

    Vector2 NewLocation()
    {

        //get the player position
        // find a random number from outside of the 10x10 range but within the 30x30 (x val) range.
        // take the random number, and add it to the xval of the player transform but 
        Transform player = PlayerManager.Instance.player.transform;
        Vector2 newPos = player.position;
        float xVal = Random.Range(-20f, 20f);
        if (xVal < 0)
        {
            newPos.x -= 10f;
            newPos.x += xVal;
        }
        else
        {
            newPos.x += 10f + xVal;
        }
        float yVal = Random.Range(-20f, 20f);
        if (yVal < 0)
        {
            newPos.y -= 10f;
            newPos.y += yVal;
        }
        else
        {
            newPos.y += 10f + yVal;
        }
        return newPos;
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
			SpawnEnemy(wave.enemy, wave.speedRange);
			yield return new WaitForSeconds(1f / wave.rate);
		}

		//now we're waiting for more enemies
		state = SpawnState.WAITING;
		yield break;
	}

	void SpawnEnemy(GameObject enemy, Vector2 range)
	{
        Vector2 positionClose = transform.position - new Vector3(Random.Range(-bounds, bounds), Random.Range(-bounds, bounds));
        while(Vector2.Distance(positionClose, PlayerManager.Instance.player.transform.position) < 10f)
        {
            positionClose = transform.position - new Vector3(Random.Range(-bounds, bounds), Random.Range(-bounds, bounds));
        }
		GameObject e = Instantiate(enemy, positionClose, transform.rotation);
        float num = Random.Range(range.x, range.y);
        e.GetComponent<movement>().movementSpeed = num;
		e.GetComponent<movement>().manager = this.gameObject;
		Debug.Log("Spawning Enemy: " + enemy.name);
	}
}