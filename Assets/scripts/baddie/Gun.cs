using System.Collections;
using UnityEngine;

public class Gun : Baddie
{

	public BulletPool bulletPool;
	public Bullet bulletPrefab;
	public PlayerInputController target;
	public float lastShot = 0;

	public override void Attach()
	{
		base.Attach();
	}

	public void Update()
	{
		if (lastShot + .5 < Time.time)
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
	}
}
