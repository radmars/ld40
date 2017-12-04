using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
	public Text scoreText;
	public float massPerBaddie = 1;
	public float scalePerBaddie = 1.15f;
	List<Baddie> attached;
	long score = 0;
    public Text comboText;
    int combo = 0;
	public GameObject geometry;
	public Rigidbody2D body;
	public new BoxCollider2D collider;
	private bool tethered = true;
	public Collider2D boundary;
	public float startingMass = 1;
	private Vector2 startingColliderSize;
    public AudioSource attachSound;
    public AudioSource comboSound;
    public AudioSource pickupSound;
    public AudioSource releaseSound;

	public bool Visible { get; private set; }

	public void Start()
	{
		attached = new List<Baddie>();
		UpdateScore();
		startingColliderSize = collider.size;
		RecomputeSize();
	}

	public void AttachBaddie(Baddie b)
	{
		attached.Add(b);
		b.transform.parent = transform;
		b.transform.position = b.transform.position;

        attachSound.Play();

		RecomputeSize();
		//TODO COME UP WITH REAL SCORING (Score based on pickups on number of baddies attacahed?)
		UpdateScore();
	}

	private void RecomputeSize()
	{
		// TODO THIS LOGIC IS PROBABLY WRONG
		if (attached.Count > 0)
		{
			var scale = new Vector3(1, 1, 1) * (1 + (attached.Count * scalePerBaddie)/100);
			geometry.transform.localScale = scale;
			collider.size = startingColliderSize + new Vector2(1, 1) * (1 + (attached.Count * scalePerBaddie)/100);
			body.mass = startingMass + attached.Count * massPerBaddie;
		}
		else
		{
            geometry.transform.localScale = startingColliderSize;
			collider.size = startingColliderSize;
			body.mass = startingMass;
		}
	}

	public void Tether(Vector3 position, Transform parent)
	{
		body.velocity = Vector2.zero;
		transform.parent = parent;
		transform.localPosition = position;
		tethered = true;
		foreach(var a in attached)
		{
			a.gameObject.SetActive(false);
		}
		attached.Clear();
		RecomputeSize();
        UpdateScore();
    }

	public void Release(Vector3 push)
	{
		body.AddForce(new Vector3(push.x, push.y));
		body.drag = 0;
		transform.parent = null;
        tethered = false;
        releaseSound.Play();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if(collision == boundary)
		{
			Visible = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision == boundary)
		{
			Visible = false;
            foreach(Baddie baddie in attached)
            {
                if(!tethered)
                baddie.Freshen();
            }
		}
	}

	internal IEnumerator WaitUntilOffScreen()
	{
		yield return new WaitUntil(() => { return !Visible; });
	}

    void UpdateScore()
    {
        score += (attached.Count * 14);
        scoreText.text = score.ToString("d10");
        comboText.text = "COMBO x " + attached.Count;

        if (attached.Count > 0 && attached.Count % 5 == 0)
        {
            comboSound.Play();
        }
    }
}
