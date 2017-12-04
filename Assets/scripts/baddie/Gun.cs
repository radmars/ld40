using System.Collections;
using UnityEngine;

public class Gun : Baddie
{

	public BulletPool bulletPool;
	public Bullet bulletPrefab;
	public PlayerInputController target;
	public float rateOfFire = 1.5f;
	public float lastShot = 0;
    public AudioSource audioSource;

	public override void Attach()
	{
		base.Attach();
	}

	public void Update()
	{
		if (lastShot + rateOfFire < Time.time && isActive)
		{
			lastShot = Time.time;
			Shoot();
		}
	}

	public void Shoot()
	{
        if (this.Mover.finishedRoute)
        {
            this.gameObject.SetActive(false);
        }

		if (bulletPool == null)
		{
			bulletPool = GameObject.FindObjectOfType<BulletPool>();
		}

		var bullet = bulletPool.GetInstance(bulletPrefab);
		bullet.transform.position = transform.position;
		// TODO: Should probably inject different logics here, for now shoot straight at our hero.
		if (target)
		{
			bullet.direction = (target.transform.position - bullet.transform.position).normalized;
		}
		else
		{
			bullet.direction = -(Vector3.up);
		}

        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.Play();
	}
}
