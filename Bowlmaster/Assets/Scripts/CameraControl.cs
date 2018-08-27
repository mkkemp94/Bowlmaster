using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject ball;

    // Offset between ball and camera
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = this.transform.position - ball.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // In front of head pin
        if (ball.transform.position.z < 1829f)
        {
            this.transform.position = ball.transform.position + offset;
        }
    }
}
