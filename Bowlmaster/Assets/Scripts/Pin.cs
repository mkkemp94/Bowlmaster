using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3f;

    public bool IsStanding()
    {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        float eulerRotX = rotationInEuler.x;
        float eulerRotZ = rotationInEuler.z;

        // Unity 5.3+ does not return negative values
        float tiltInX = (eulerRotX < 180) ? eulerRotX : 360 - 270 - eulerRotX;
        float tiltInZ = (eulerRotZ < 180) ? eulerRotZ : 360 - eulerRotZ;

        // We're only interested in whether x or z is differed far enough
        if (tiltInX < standingThreshold && tiltInZ < standingThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
