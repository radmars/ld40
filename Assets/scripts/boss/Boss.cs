using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Boss : MonoBehaviour
{

	public int hitPoints;
	public Ball theBall;

	protected List<Collider2D> weakSpots;

	public void Start()
	{
		weakSpots = new List<Collider2D>();
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject == theBall.gameObject)
		{
			Debug.Log("BOss hit!!");
			hitPoints--;
		}
	}
}
