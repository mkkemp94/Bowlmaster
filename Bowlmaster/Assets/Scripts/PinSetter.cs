using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public int lastStandingCount = -1; // not great. What does -1 mean?
    public GameObject pinset;

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
            UpdateStandingCountAndSettle();
        }
	}

    public void RaisePins()
    {
        Debug.Log("Raising pins");
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.RaiseIfStanding();
        }
    }

    public void LowerPins()
    {
        Debug.Log("Lowering pins");
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    public void RenewPins()
    {
        Debug.Log("Renewing pins");
        GameObject newPins = Instantiate(pinset);
    }

    // If enough time has passed since all pins have been standing, settle and return ball.
    void UpdateStandingCountAndSettle()
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
}




// PinSetter
// >> Call ScoreMaster in PinsSettled()
// >> Pass how many pins were knocked over


// ScoreMaster
// >> Will have access to the current round (private)
// >> TurnTaken() will take how many pins were knocked over
// >> Add score to currentRoundScore
// >> If totalTurns % 2 != 0 && totalTurns != 21 && score == 10 >> strike
// >> If totalTurns % 2 == 0 && score == 10 >> spare
// >> If totalTurns 


/// How it wil actually be
/// ScoreMaster will have an enum called Action with Reset and Tidy items
/// PinSetter will call ScoreMaster each turn and get back one of these two actions
/// ScoreMaster will also provide the frames using GetFrames() method