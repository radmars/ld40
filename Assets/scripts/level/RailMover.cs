using UnityEngine;
using System.Collections;
using System;

public class RailMover : MonoBehaviour
{
    public Rail rail;

    public PlayMode mode;

    public bool returns;

    public float speed = 2.5f;

    public float minSpeed = 2.5f;

    public float maxSpeed = 5f;

    private int currentSeg;

    private float transition;

    public bool isCompleted;

    public bool finishedRoute;

    public bool grounded;

    public void Update()
    {
        if (!rail)
            return;

        if (!isCompleted && currentSeg < rail.nodes.Length - 1)
            Play();
    }

	internal void Freshen(Rail rail)
	{
		currentSeg = 0;
		this.rail = rail;
		isCompleted = false;
		Update();
	}

	private void Play(bool forward = true)
    {
        //calculate speed and stuff
        var temp = rail.nodes.Length;
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;
        transition += (forward) ? s : -s;
        //check transition
        if (transition > 1)
        {
            transition = 0;
            currentSeg++;
            //if we reached the end
            if (currentSeg == rail.nodes.Length - 1)
            {
                finishedRoute = true;
                isCompleted = true;
                return;
            }
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSeg--;

            if (currentSeg == -1)
            {
                isCompleted = true;
                return;

            }
        }

        transform.position = rail.PositionOnRail(currentSeg, transition, mode, grounded);
        transform.rotation = rail.Orientation(currentSeg, transition);
    }
}