using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashHandler : MonoBehaviour
{
	public string nextScene;
    public AudioSource titleSound;
    public AudioSource titleBoom;
    private bool pressed = false;

	void Update()
	{
		if (Input.anyKey && !pressed)
		{
            StartCoroutine(StartGame());
            pressed = true;
		}
	}

    private IEnumerator StartGame() {
        titleBoom.Play();
        titleSound.Play(40000);

        while (titleSound.isPlaying)
        {
            yield return null;
        }

		SceneManager.LoadScene("emarcotte-test");
    }
}
