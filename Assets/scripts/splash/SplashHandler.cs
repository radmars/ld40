using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashHandler : MonoBehaviour
{
	public string nextScene;
    public AudioSource titleSound;

	void Update()
	{
		if (Input.anyKey)
		{
            StartCoroutine(StartGame());
		}
	}

    private IEnumerator StartGame() {
        titleSound.Play();

        while (titleSound.isPlaying)
        {
            yield return null;
        }

		SceneManager.LoadScene("emarcotte-test");
    }
}
