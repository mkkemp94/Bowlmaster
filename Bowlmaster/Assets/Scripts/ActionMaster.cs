using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {

    public enum Action { Tidy, Reset, EndTurn, EndGame };
    private int[] bowls = new int[21];
    private int bowl = 1;

	public Action Bowl (int pins)
    {

        if (pins < 0 || pins > 10) { throw new UnityException("Invalid number of pins!"); }
        if (bowls[bowl-1] + pins > 10 && bowl < 19) { throw new UnityException("Too many pins!"); }
        
        // Assign bowl to right spot.
        bowls[bowl-1] = pins;

        //for (int i = 1; i <= bowls.Length; i++)
        //{
        //    Debug.Log("Bowl #" + i + " = " + bowls[i - 1]);
        //}
        
        // Strike turns 1-9 -> end turn
        if (pins == 10 && bowl < 19)
        {
            bowl += 2;
            return Action.EndTurn;
        }

        // Strike turn 10 frame 1 or 2-> end turn
        else if (pins == 10 && (bowl == 19 || bowl == 20))
        {
            bowl += 1;
            return Action.Reset;

        }

        // Last frame always end game
        else if (bowl == 21)
        {
            return Action.EndGame;
        }

        // Strike on turn 10 frame 2
        else if (bowl == 20 && pins == 10)
        {
            return Action.Reset;
        }

        // Turn 10 frame 2 and spare
        else if (bowl == 20 && (bowls[bowl - 1] + pins == 10))
        {
            bowl += 1;
            return Action.Reset;
        }

        // First ball of frame.
        else if (bowl % 2 != 0)
        {
            bowl++;
            return Action.Tidy;
        }

        // End of frame
        else if (bowl % 2 == 0)
        {
            bowl++;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure what action to return!");
    }
}
