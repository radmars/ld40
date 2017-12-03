using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
	public BaddiePool baddiePool;
	public BossPool bossPool;

	public Wave[] waves;
	public float timeBetweenWaves = 5f;
	public int currentLevel = 1;
	public int waveCountRemaining = 10;
	public int bossesPerLevel = 3;

	void Start()
	{
		foreach (var wave in waves)
		{
			wave.gameObject.SetActive(true);
		}
		StartCoroutine(SpawnWaves());
	}

	private IEnumerator SpawnWaves()
	{
		yield return new WaitUntil(() =>
		{
			foreach (var wave in waves)
			{
				if (!wave.HasRails())
				{
					return false;
				}
			}
			return true;
		});

		while (gameObject.activeSelf)
		{
			if (waveCountRemaining-- > 0)
			{
				int index = Random.Range(0, waves.Length);
				StartCoroutine(SpawnWave(waves[index]));
			}
			else
			{
				var boss = RunBossEncounter();
				yield return new WaitUntil(() => { return !boss.gameObject.activeSelf; });
				waveCountRemaining = 10;
			}
			yield return new WaitForSeconds(timeBetweenWaves);
		}
	}

	private Boss RunBossEncounter()
	{
		var newBoss = bossPool.GetRandom();
		newBoss.StartAttack();
		return newBoss;
	}

	IEnumerator SpawnWave(Wave wave)
	{
		foreach (var result in wave.Spawn(baddiePool))
		{
			yield return result;
		}
	}
}