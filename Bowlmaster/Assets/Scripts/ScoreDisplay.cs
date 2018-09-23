using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] rollTexts, frameTexts;

	// Use this for initialization
	void Start () {
		for (int i = 1; i <= rollTexts.Length; i++)
        {
            rollTexts[i - 1].text = "1"; ;
        }

        for (int i = 1; i <= frameTexts.Length; i++)
        {
            frameTexts[i - 1].text = i + ""; ;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
