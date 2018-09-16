using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3f;
    public float distanceToRaise = 60.0f;

    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public Rigidbody getRigidbody()
    {
        return rigidBody;
    }

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

    public void RaiseIfStanding()
    {
        if (this.IsStanding())
        { 
            transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);

            //pin.transform.position = new Vector3(pin.transform.position.x, pin.transform.position.y + distanceToRaise, pin.transform.position.z);
            //transform.position += new Vector3(0, distanceToRaise, 0);
            rigidBody.useGravity = false;
            transform.rotation = Quaternion.Euler(270, 0, 0);
        }
    }

    public void Lower()
    {
        transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
        rigidBody.useGravity = true;
    }
}
