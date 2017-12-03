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

    void Start()
    {
        waveCountDown = timeBetweenWaves;
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
                return;
            }

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

        for (int i = 0; i < wave.amount; i++)
        {
            SpawnBaddie(wave.baddie);

            // When last baddie spawns
            if (i == wave.amount - 1)
            {
                state = SpawnState.COUNTING;
                waveCountDown = timeBetweenWaves;
                nextWave++;
            }

            yield return new WaitForSeconds(1f / wave.rate);
        }

        yield break;
    }

    void SpawnBaddie(Baddie baddie)
    {
        //TODO (find more machine effecient way to do this)
		var moveScript = baddie.GetComponent<RailMover>();

		if (moveScript != null)
			moveScript.isCompleted = false;

		// TODO: Replace with pulling from baddie pool
		baddiePool.GetInstance(baddie);
    }
}

[System.Serializable]
public class Wave
{
    public Baddie baddie;
    public int amount;
    public float rate;
}