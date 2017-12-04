using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Boss : MonoBehaviour
{

	public int hitPoints;
	public Ball theBall;
    public Text bossName;
    public Slider healthSlider;
	public int startingHitPoints;
    public AudioSource explosionSound;

	protected List<Collider2D> weakSpots;

	public void Start()
	{
		weakSpots = new List<Collider2D>();
		healthSlider.maxValue = startingHitPoints;
		healthSlider.value = hitPoints;
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject == theBall.gameObject)
		{
			hitPoints -= Mathf.RoundToInt(theBall.body.mass * 10f);
			healthSlider.value = hitPoints;

			if (hitPoints <= 0 )
			{
                StartCoroutine(Die());
			}
            else
            {
                explosionSound.pitch = UnityEngine.Random.Range(1.4f, 2.5f);
                explosionSound.Play();
            }
		}
	}

	private IEnumerator Die()
	{
        explosionSound.pitch = 0.75f;
        explosionSound.Play();
        healthSlider.gameObject.SetActive(false);
        bossName.gameObject.SetActive(false);
        while (explosionSound.isPlaying)
        {
            yield return null;
        }
        gameObject.SetActive(false);
	}

	internal void StartAttack()
	{
		hitPoints = startingHitPoints;
        RefreshHealthBar();
		StartCoroutine(SlideIn());
	}

	private IEnumerator SlideIn()
	{
		var start = new Vector3(0, 6, 0);
		var end = new Vector3(0, 2.8f, 0);
		float startTime = Time.time;
		float slideTime = 10f;

        yield return new WaitUntil(() =>
		{
			float t = (Time.time - startTime) / slideTime;
			transform.position = Vector3.Lerp(start, end, t);
			return t >= 1.0f;
		});
	}

    private void RefreshHealthBar()
    {
        healthSlider.gameObject.SetActive(true);
        bossName.gameObject.SetActive(true);
        healthSlider.maxValue = startingHitPoints;
        healthSlider.value = hitPoints;
    }
}
