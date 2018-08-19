﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public int lastStandingCount = -1;
    public float distanceToRaise = 500.0f;

    private Ball ball;
    private float lastChangeTime;
    private bool ballEnteredBox = false;

    private void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
    }

    void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballEnteredBox)
        {
            CheckStandingCount();
        }
	}

    public void RaisePins()
    {
        Debug.Log("Raising pins");
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                //pin.transform.Translate(new Vector3(0, distanceToRaise, 0));
                //pin.transform.position = new Vector3(pin.transform.position.x, 40, pin.transform.position.z);
                //pin.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    public void LowerPins()
    {
        Debug.Log("Lowering pins");
    }

    public void RenewPins()
    {
        Debug.Log("Renewing pins");
    }

    void CheckStandingCount()
    {
        // Update the last standing count
        int currentStandingCount = CountStanding();

        if (currentStandingCount != lastStandingCount)
        {
            lastStandingCount = currentStandingCount;
            lastChangeTime = Time.time;
            return;
        }

        float settleTime = 3f; 

        // Call PinsHaveSettled()
        if (Time.time - lastChangeTime >= settleTime)
        {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled()
    {
        ball.Reset();
        lastStandingCount = -1; // Indicates pins have settled, and ball back in box.
        ballEnteredBox = false;
        standingDisplay.color = Color.green;
    }

    int CountStanding()
    {
        int countStanding = 0;

        // Keep track of the number of standing pins
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                countStanding++;
            }
        }

        // Return the number of standing pins
        return countStanding;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject thingHit = other.gameObject;

        // Ball enters play box
        if (thingHit.GetComponent<Ball>())
        {
            ballEnteredBox = true;
            standingDisplay.color = Color.red;
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject thingLeft = other.gameObject;
        if (thingLeft.GetComponent<Pin>()) { 
            Destroy(thingLeft);
        }
    }
}