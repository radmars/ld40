using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

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
        if(state == SpawnState.WAITING)
        {
            return;
        }

        if (waveCountDown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave (Wave wave)
    { 
        state = SpawnState.SPAWNING;

        for(int i = 0; i< wave.amount; i++)
        {
            SpawnBaddie(wave.baddie);

            // When last baddie spawns
            if (i == wave.amount - 1)
                state = SpawnState.COUNTING;

            yield return new WaitForSeconds(1f / wave.rate);
        }

        yield break;
    }

    void SpawnBaddie(Transform baddie)
    {
        // TODO: Replace with pulling from baddie pool
        Instantiate(baddie, transform.position, transform.rotation);
    }
}