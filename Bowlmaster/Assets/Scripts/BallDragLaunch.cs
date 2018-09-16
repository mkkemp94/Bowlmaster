using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Ball))]
public class BallDragLaunch : MonoBehaviour {

    private Ball ball;
    private Vector3 dragStart, dragEnd;
    private float startTime, endTime;
    
	void Start () {
        ball = GetComponent<Ball>();
	}

    /**
     * Capture the time and position of mouse click and hold.
     * Used to calculate the velocity ball is launched at.
     */
    public void CaptureDragStart()
    {
        if (!ball.inPlay)
        {
            dragStart = Input.mousePosition;
            startTime = Time.time;
        }
    }

    /**
     * Capture the time and position of mouse release.
     * Used to calculate the velocity ball is launched at.
     * Also launch the ball.
     */
    public void CaptureDragEndAndLaunch()
    {
        if (!ball.inPlay)
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
    }

    /**
     * Nudge the ball to either side before launching.
     */
    public void MoveStart(float xNudge)
    {
        if (ball.inPlay == false)
        {
            ball.transform.Translate(new Vector3(xNudge, 0, 0));

            // Clamp to lane
            Vector3 pos = ball.transform.position;
            pos.x = Mathf.Clamp(transform.position.x, -50, 50);
            ball.transform.position = pos;
        }
    }
}
