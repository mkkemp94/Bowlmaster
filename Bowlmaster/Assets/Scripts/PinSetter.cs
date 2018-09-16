using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    
    public GameObject pinset;
    private Animator animator;
    private PinCounter pinCounter;

    private void Start()
    {
        animator = GetComponent<Animator>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
    }

    public void PerformAction(ActionMaster.Action action)
    {
        switch (action)
        {
            // Pass action to animator
            case ActionMaster.Action.Tidy:
                TidyPins();
                break;
            case ActionMaster.Action.EndTurn:
            case ActionMaster.Action.Reset:
                ResetPins();
                pinCounter.Reset();
                break;
            case ActionMaster.Action.EndGame:
                throw new UnityException("Don't know how to end game!");
        }
    }

    public void TidyPins()
    {
        animator.SetTrigger("tidyTrigger");
    }

    public void ResetPins()
    {
        animator.SetTrigger("resetTrigger");
    }

    // Reply from animator
    public void RaisePins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.getRigidbody().velocity = Vector3.zero;
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
}