﻿using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
	public BaddiePool baddiePool;
	public BossPool bossPool;

	public Wave[] waves;
	public float timeBetweenWaves = 5f;

	public static int currentLevel = 1;
    private uint currentMusic = 0;
	public int waveCountRemaining = 10;
	public int bossesPerLevel = 3;

    public AudioSource levelUpSound;
    public AudioSource musicPlayer;
    public AudioClip[] songs;
	public ParallaxScroller scroller;

	void Start()
	{
		foreach (var wave in waves)
		{
			wave.gameObject.SetActive(true);
		}
		StartCoroutine(SpawnWaves());
        StartLevelMusic(5000);
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
                NextLevel();
			}
			yield return new WaitForSeconds(timeBetweenWaves);
		}
	}

    private void NextLevel()
    {
        currentLevel++;
        if ((currentLevel - 1) % 3 == 0)
        {
            StartCoroutine(FadeMusic());
        }
    }

    private IEnumerator FadeMusic()
    {
        float startVolume = musicPlayer.volume;
        float fadeTime = 3.0f;

        while (musicPlayer.volume > 0)
        {
            musicPlayer.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        musicPlayer.Stop();
        musicPlayer.volume = startVolume;

        levelUpSound.Play();
        StartLevelMusic(72000);
    }

    private void StartLevelMusic(ulong delay)
    {
        musicPlayer.clip = songs[currentMusic % songs.Length];
        currentMusic++;
		scroller.StartNewLevel();
        musicPlayer.Play(delay);
    }

	private Boss RunBossEncounter()
	{
		var newBoss = bossPool.GetRandom();
		newBoss.StartAttack();
        newBoss.startingHitPoints = 60 + (currentLevel * 15);
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