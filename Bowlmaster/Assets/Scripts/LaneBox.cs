using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBox : MonoBehaviour {

    private PinSetter pinSetter;

    private bool ballLeftBox = false;

    // Use this for initialization
    void Start () {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
	}

    void OnTriggerExit(Collider other)
    {
        // Sends ball out signal to pinsetter
        if (other.gameObject.name == "Ball")
        {
            pinSetter.ballOutOfPlay = true;
        }
    }
}
