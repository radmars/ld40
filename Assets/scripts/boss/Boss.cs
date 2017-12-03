using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Boss : MonoBehaviour
{

	public int hitPoints;
	public Ball theBall;
    public Text bossName;
    public Slider healthSlider;

	protected List<Collider2D> weakSpots;

	public void Start()
	{
		weakSpots = new List<Collider2D>();
        healthSlider.maxValue = hitPoints;
        healthSlider.value = hitPoints;
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject == theBall.gameObject)
		{
			Debug.Log("BOss hit!!");
			hitPoints--;
            healthSlider.value = hitPoints;
			if(hitPoints <= 0 )
			{
				Die();
			}
		}
	}

	public void Die()
	{
		gameObject.SetActive(false);
		healthSlider.gameObject.SetActive(false);
        bossName.gameObject.SetActive(false);
	}
}
