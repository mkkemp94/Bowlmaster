using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

    // If pin leaves box collider for pin setter, destroy it
    void OnTriggerExit(Collider other)
    {
        GameObject thingLeft = other.gameObject;
        if (thingLeft.GetComponent<Pin>())
        {
            Destroy(thingLeft);
        }
    }
}
