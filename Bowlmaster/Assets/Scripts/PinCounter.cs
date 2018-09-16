using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour
{
    public Text standingDisplay;

    private GameManager gameManager;
    private int lastStandingCount = -1;
    private int lastSettledCount = 10;
    private float lastChangeTime;
    private bool ballOutOfPlay = false;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Show current standing pins
        standingDisplay.text = CountStanding().ToString();

        if (ballOutOfPlay)
        { 
            UpdateStandingCountAndSettle();
            standingDisplay.color = Color.red;
        }
    }

    public int CountStanding()
    {
        int standingPins = 0;

        // Keep track of the number of standing pins
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                standingPins++;
            }
        }

        // Return the number of standing pins
        return standingPins;
    }

    public void UpdateStandingCountAndSettle()
    {
        // Update the last standing count
        int currentStandingCount = CountStanding();

        if (currentStandingCount != lastStandingCount)
        {
            lastStandingCount = currentStandingCount;
            lastChangeTime = Time.time;
            return;
        }

        float settleTime = 3f; // hardcoded bad

        // Pins have remained settled for the time
        if (Time.time - lastChangeTime >= settleTime)
        {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled()
    {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        // Calculate bowl
        gameManager.Bowl(pinFall);

        standingDisplay.color = Color.green;
        lastStandingCount = -1; // Indicates pins have settled, and ball back in box.
        ballOutOfPlay = false;
    }

    public void Reset()
    {
        lastSettledCount = 10;
    }

    //private void ResetPlayspace()
    //{
    //    // Reset ball
    //    ball.Reset();
    //    lastStandingCount = -1; // Indicates pins have settled, and ball back in box.
    //    ballOutOfPlay = false;
    //    standingDisplay.color = Color.green;
    //}

    void OnTriggerExit(Collider other)
    {
        // Sends ball out signal to pinsetter
        if (other.gameObject.name == "Ball")
        {
            ballOutOfPlay = true;
        }
    }
}
