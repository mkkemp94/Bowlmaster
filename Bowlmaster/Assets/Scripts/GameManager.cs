using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    private List<int> allBowls = new List<int>();

    private PinSetter pinSetter;
    private ScoreDisplay scoreDisplay;
    private Ball ball;

    void Start ()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
    }

    public void Bowl(int pinFall)
    {
        Debug.Log("Pinfall: " + pinFall);

        try
        {
            allBowls.Add(pinFall);
            ball.Reset();
            pinSetter.PerformAction(ActionMaster_OLD.NextAction(allBowls));
        }
        catch
        {
            Debug.LogWarning("Something went wrong in Bowl() performing action");
        }

        try
        {
            print("Null score display: " + (scoreDisplay == null));
            print("Null all bowls: " + (allBowls == null));
            scoreDisplay.FillRolls(allBowls);
            scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(allBowls));
        }
        catch
        {
            Debug.LogWarning("Something went wrong in Bowl() filling score card");
        }
        
    }
}
