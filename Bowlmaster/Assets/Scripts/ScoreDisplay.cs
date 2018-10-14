using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] rollTexts, frameTexts;

    // Calls format rolls and iterates the returned string.
    public void FillRolls(List<int> rolls)
    {
        string formattedRolls = FormatRolls(rolls);
        
        // My code here = three lines
        for (int i = 0; i < formattedRolls.Length; i++)
        {
            rollTexts[i].text = formattedRolls[i].ToString();
        }
    }

    public void FillFrames(List<int> frames)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            frameTexts[i].text = frames[i].ToString();
        }
    }

    // Takes in a list of rolls and returns a string, a 1:1 mapping with ui boxes
    public static string FormatRolls(List<int> rolls)
    {
        string output = "";

        for (int i = 0; i < rolls.Count; i++)
        {
            int box = output.Length + 1;   // Score box 1 to 21

            if (rolls[i] == 0)                                      // Always enter - for 0
            {
                output += "-";
            }
            else if ((box % 2 == 0 || box == 21) && rolls[i - 1] + rolls[i] == 10) // Spare ... even on last frame
            {
                output += "/";
            }
            else if (box >= 19 && rolls[i] == 10)                   // Strike frame 10
            {
                output += "X";
            } 
            else if (rolls[i] == 10)                                // Strike fram 1-9
            {
                output += "X ";
            }
            else                                                    // Normal bowl
            {
                output += rolls[i].ToString();
            }
        }

        // My code
        return output;
    }
}
