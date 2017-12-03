using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
	public enum SpawnState { SPAWNING, WAITING, COUNTING };
	public BaddiePool baddiePool;

	public Wave[] waves;
	private int nextWave = 0;
	public float timeBetweenWaves = 5f;
	public float waveCountDown = 0f;
	private SpawnState state = SpawnState.COUNTING;
	public int currentLevel = 1;

	void Start()
    {
        waveCountDown = timeBetweenWaves;
		foreach(var wave in waves)
		{
			wave.gameObject.SetActive(true);
		}
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            return;
        }

        if (waveCountDown <= 0)
        {
            // If it's spawning or there are no more waves, just return
            if (state == SpawnState.SPAWNING || waves.Length == nextWave)
            {
				Debug.Log("Spawning? " + (SpawnState.SPAWNING == state));
                return;
            }

			Debug.Log("Spawning?");
            StartCoroutine(SpawnWave(waves[nextWave]));
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

	IEnumerator SpawnWave(Wave wave)
	{
		state = SpawnState.SPAWNING;
		foreach(var result in wave.Spawn(baddiePool))
		{
			yield return result;
		}
		state = SpawnState.COUNTING;
		waveCountDown = timeBetweenWaves;
		nextWave++;
	}
}