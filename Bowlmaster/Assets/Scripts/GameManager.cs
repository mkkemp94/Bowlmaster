using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    private List<int> allBowls = new List<int>();

    private PinSetter pinSetter;
    private Ball ball;

    void Start ()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
    }

    public void Bowl(int pinFall)
    {
        Debug.Log("Pinfall: " + pinFall);

        allBowls.Add(pinFall);

        ActionMaster.Action action = ActionMaster.NextAction(allBowls);
        pinSetter.PerformAction(action);
        ball.Reset();

        Debug.Log("Action: " + action);
    }
}
