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

	public override void Attach()
	{
		base.Attach();
		GetComponent<Rigidbody2D>().simulated = false;
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
        if(bulletPool == null)
        {
           bulletPool = GameObject.FindObjectOfType<BulletPool>();
        }

		var bullet = bulletPool.GetInstance(bulletPrefab);
		bullet.transform.position = transform.position;
		// TODO: Should probably inject different logics here, for now shoot straight at our hero.
		bullet.direction = (target.transform.position - bullet.transform.position).normalized;
	}
}
