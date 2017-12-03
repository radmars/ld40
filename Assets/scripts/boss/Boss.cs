using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Boss : MonoBehaviour
{

	protected List<Collider2D> weakSpots;

	public void Start()
	{
		weakSpots = new List<Collider2D>();
	}
}
