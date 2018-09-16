using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBox : MonoBehaviour {

    //private PinSetter pinSetter;

    private GameManager gameManager;

    private bool ballLeftBox = false;

    // Use this for initialization
    void Start () {
        //pinSetter = GameObject.FindObjectOfType<PinSetter>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}

    //void OnTriggerExit(Collider other)
    //{
    //    // Sends ball out signal to pinsetter
    //    if (other.gameObject.name == "Ball")
    //    {
    //        gameManager.ballOutOfPlay = true;
    //    }
    //}
}
