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
	public new CircleCollider2D collider;
	private bool tethered = true;
	public Collider2D boundary;
	public float startingMass = 1;
	private float startingColliderSize;
    public AudioSource attachSound;
    public AudioSource comboSound;
    public AudioSource pickupSound;
    public AudioSource releaseSound;
	private Vector3 startingScale;

	public bool Visible { get; private set; }

	public void Start()
	{
		attached = new List<Baddie>();
		UpdateScore();
		startingColliderSize = collider.radius;
		startingScale = geometry.transform.localScale;
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
		var scale = attached.Count * scalePerBaddie;
		geometry.transform.localScale = startingScale + startingScale * scale;
		collider.radius = startingColliderSize + startingColliderSize * scale;
		body.mass = startingMass + attached.Count * massPerBaddie;
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
