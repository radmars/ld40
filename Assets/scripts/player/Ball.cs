using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	List<Baddie> attached;
	public void Start()
	{
		attached = new List<Baddie>();
	}

	public void AttachBaddie(Baddie b)
	{
		attached.Add(b);
		b.transform.parent = transform;
		b.transform.position = b.transform.position;
	}
}
