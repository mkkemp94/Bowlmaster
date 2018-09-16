using System.Collections;
using System.Collections.Generic; // for lists
using UnityEngine;

public class ScoreMaster {

    /** 
      * Keep calling score frames
      * Keep track of cumulative score, like a normal score card.
      * Return list of cumulative scores.
      */
    public static List<int> ScoreCumulative(List<int> rolls)
    {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;
        foreach (int frameScore in ScoreFrames(rolls))
        {
            Debug.Log("This frame: " + frameScore);
            runningTotal += frameScore;
            Debug.Log("Running Total: " + frameScore);
            cumulativeScores.Add(runningTotal);
        }
        return cumulativeScores;
    }

    /**
     * Returns what can currently be calculated for frames.
     * Returns a list of individual scores without adding them up.
     */
    public static List<int> ScoreFrames (List<int> rolls)
    {
        List<int> frameList = new List<int>();
        int currentBowl = 1;
        int frameScore = 0;

        // My code here
        foreach (int roll in rolls)
        {
            Debug.Log("ScoreFrames: Roll score = " + roll);
            frameScore += roll;

            if (OddTurn(currentBowl) && roll == 10)
            {
                currentBowl++;
            }

            if (EvenTurn(currentBowl))
            {
                Debug.Log("ScoreFrames: Frame Score = " + (frameScore));

                frameList.Add(frameScore);
                frameScore = 0;
            }


            currentBowl++;
        }

        return frameList;
    }

    private static bool OddTurn(int currentRoll)
    {
        return currentRoll % 2 != 0;
    }

    private static bool EvenTurn(int currentRoll)
    {
        return currentRoll % 2 == 0;
    }
}
