using System.Collections;
using UnityEngine;

public class Gun : Baddie {

	public BulletPool bulletPool;
	public Bullet bulletPrefab;
	public PlayerInputController target;
	public float lastShot = 0;

	void Start()
	{
	}

	public void Update()
	{
		if(lastShot + .5 < Time.time)
		{
			lastShot = Time.time;
			Shoot();
		}
	}

	public void Shoot()
	{
		var bullet = bulletPool.GetInstance(bulletPrefab);
		bullet.transform.position = transform.position;
		// TODO: Should probably inject different logics here, for now shoot straight at our hero.
		bullet.direction = (target.transform.position - bullet.transform.position).normalized;
	}
}
