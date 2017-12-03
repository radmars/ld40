using System;
using System.Collections;
using UnityEngine;

public class Wave : MonoBehaviour
{
	public Baddie baddieToSpawn;
	private Rail[] rails;
	public float rate;

	public void Start()
	{
		rails = GetComponentsInChildren<Rail>();
	}

	public IEnumerable Spawn(BaddiePool pool)
	{
		foreach (var rail in rails)
		{
			var newBaddie = pool.GetInstance(baddieToSpawn);
			var moveScript = newBaddie.Mover;
			if (moveScript)
			{
				moveScript.Freshen(rail);
                baddieToSpawn.Freshen();
			}
			if (rate > 0)
			{
				yield return new WaitForSeconds(1f / rate);
			}
		}
	}

	internal bool HasRails()
	{
		return rails != null;
	}
}