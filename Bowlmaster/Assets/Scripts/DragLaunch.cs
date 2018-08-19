using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    private Ball ball;

    private Vector3 dragStart, dragEnd;
    private float startTime, endTime;
    
	void Start () {
        ball = GetComponent<Ball>();
	}

    public void DragStart()
    {
        // Capture time & position of drag start / mouse click
        dragStart = Input.mousePosition;
        startTime = Time.time;
    }

    public void DragEnd()
    {
        // Launch ball
        dragEnd = Input.mousePosition;
        endTime = Time.time;

        float dragDuration = endTime - startTime;

        float distanceX = dragEnd.x - dragStart.x;
        float distanceY = dragEnd.y - dragStart.y;

        float launchSpeedX = distanceX / dragDuration;
        float launchSpeedZ = distanceY / dragDuration; // translation from 2d screen (x, y) to 3d world (z)

        Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);
        ball.Launch(launchVelocity);
    }

    public void MoveStart(float xNudge)
    {
        if (ball.inPlay == false)
        {
            ball.transform.Translate(new Vector3(xNudge, 0, 0));

            Vector3 pos = ball.transform.position;
            pos.x = Mathf.Clamp(transform.position.x, -50, 50);
            ball.transform.position = pos;
        }
    }
}
