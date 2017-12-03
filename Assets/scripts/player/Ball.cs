using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Text scoreText;
	List<Baddie> attached;
    long score = 0;
    
	public void Start()
	{
		attached = new List<Baddie>();
        UpdateScore();
	}

	public void AttachBaddie(Baddie b)
	{
		attached.Add(b);
		b.transform.parent = transform;
		b.transform.position = b.transform.position;

        //TODO COME UP WITH REAL SCORING (Score based on pickups on number of baddies attacahed?)
        UpdateScore();
	}

    void UpdateScore()
    {
        score += (attached.Count * 14);
        scoreText.text = score.ToString("d10");
    }
}
