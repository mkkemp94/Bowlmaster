using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public GameObject pinset;

    private int lastStandingCount = -1; 
    
    private float lastChangeTime;
    private int lastSettledCount = 10;
    public bool ballOutOfPlay = false;

    private Ball ball;
    private Animator animator;
    private ActionMaster actionMaster;

    private void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
        actionMaster = new ActionMaster();
    }

    void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballOutOfPlay)
        {
            standingDisplay.color = Color.red;
            UpdateStandingCountAndSettle();
        }
	}

    public void SetBallOutOfPlay()
    {
        ballOutOfPlay = true;
    }

    // Reply from animator
    public void RaisePins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.RaiseIfStanding();
        }
    }

    public void LowerPins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    public void RenewPins()
    {
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
        PerformAction();
        ResetPlayspace();
    }

    void PerformAction()
    {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        // Send pinfall number to action master
        ActionMaster.Action action = actionMaster.Bowl(pinFall);
        Debug.Log("Action to take: " + action);
        switch (action)
        {
            // Pass action to animator
            case ActionMaster.Action.Tidy:
                animator.SetTrigger("tidyTrigger");
                break;
            case ActionMaster.Action.EndTurn:
            case ActionMaster.Action.Reset:
                animator.SetTrigger("resetTrigger");
                lastSettledCount = 10;
                break;
            case ActionMaster.Action.EndGame:
                throw new UnityException("Don't know how to end game!");
        }
    }

    private void ResetPlayspace()
    {
        // Reset ball
        ball.Reset();
        lastStandingCount = -1; // Indicates pins have settled, and ball back in box.
        ballOutOfPlay = false;
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