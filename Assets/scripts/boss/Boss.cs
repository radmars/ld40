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
    public AudioSource shootSound;

    public BulletPool bulletPool;
    public Bullet bulletPrefab;
    public PlayerInputController target;
    private float rateOfFire = 0.15f;
    public float lastShot = 0;

    private bool isActive = false;
    private uint bulletWaveMax = 25;
    private uint bulletWaveCurrent = 0;
    private bool bulletFocus = true;
    private float sineScaler = 5.0f;

    protected List<Collider2D> weakSpots;

	public void Start()
	{
		weakSpots = new List<Collider2D>();
		healthSlider.maxValue = startingHitPoints;
		healthSlider.value = hitPoints;
	}

    private float GetROF() {
        return bulletFocus ? rateOfFire : rateOfFire / 2.0f;
    }

    public void Update() {
        if (lastShot + GetROF() < Time.time && isActive)
        {
            lastShot = Time.time;
            Shoot();
        }
    }

    public void Shoot() {
        if (bulletPool == null)
        {
            bulletPool = GameObject.FindObjectOfType<BulletPool>();
        }

        if (bulletWaveCurrent >= bulletWaveMax * 1.5f)
        {
            bulletWaveCurrent = 0;
            bulletFocus = !bulletFocus;
        }
        else if (bulletWaveCurrent >= bulletWaveMax)
        {
            bulletWaveCurrent++;
            return;
        }

        var bullet = bulletPool.GetInstance(bulletPrefab);
        bullet.transform.position = transform.position;

        if (target)
        {
            if (bulletFocus)
            {
                bullet.direction = (target.transform.position - bullet.transform.position).normalized;
            }
            else
            {
                double progress = (double)(bulletWaveCurrent) / (double)(bulletWaveMax);
                bullet.direction = new Vector3((float)Math.Sin(progress * sineScaler * Math.PI), (float)Math.Cos(progress * sineScaler - 1.0f * Math.PI), 0);
            }
        }
        else
        {
            bullet.direction = -(Vector3.up);
        }

        shootSound.pitch = UnityEngine.Random.Range(0.55f, 0.75f);
        shootSound.Play();
        bulletWaveCurrent++;
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
        isActive = false;
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
		float slideTime = 10.0f;

        yield return new WaitUntil(() =>
		{
			float t = (Time.time - startTime) / slideTime;
			transform.position = Vector3.Lerp(start, end, t);
			return t >= 1.0f;
		});

        isActive = true;
	}

    private void RefreshHealthBar()
    {
        healthSlider.gameObject.SetActive(true);
        bossName.gameObject.SetActive(true);
        healthSlider.maxValue = startingHitPoints;
        healthSlider.value = hitPoints;
    }
}
