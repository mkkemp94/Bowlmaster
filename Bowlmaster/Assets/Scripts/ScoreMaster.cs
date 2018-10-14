using System;
using System.Collections;
using System.Collections.Generic; // for lists
using UnityEngine;

// Static == stateless
public static class ScoreMaster {

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
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }

        return cumulativeScores;
    }

    /**
     * Returns what can currently be calculated for frames.
     * Returns a list of individual scores without adding them up.
     */
    public static List<int> ScoreFrames(List<int> rolls)
    {
        List<int> frameList = new List<int>();
        int currentBowl = 1;

        for (int i = 0; i < rolls.Count; i++)
        { 
            if (OddTurn(currentBowl))
            {
                // Got a strike
                if (rolls[i] == 10)
                {
                    if (rolls.Count >= 3 && currentBowl <= 19)
                    {
                        frameList.Add(rolls[i] + rolls[i + 1] + rolls[i + 2]);

                        currentBowl += 2;
                        if (currentBowl >= 21) { break; }
                    }
                }

                // Carry on
                else
                {
                    currentBowl++;
                }
            }

            else if (EvenTurn(currentBowl))
            {
                // Got a spare
                if (rolls[i - 1] + rolls[i] == 10)
                {
                    if (rolls.Count >= i + 2)
                    {
                        frameList.Add(rolls[i - 1] + rolls[i] + rolls[i + 1]);
                    }
                }

                // Tally frame as normal
                else
                {
                    frameList.Add(rolls[i - 1] + rolls[i]);
                }

                // Carry on
                currentBowl++;
            }
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































//public static List<int> ScoreCumulative(List<int> rolls)
//{
//    List<int> cumulativeScores = new List<int>();
//    int runningTotal = 0;

//    List<int> frames = ScoreFrames(rolls);
//    for (int i = 1; i <= frames.Count; i++)
//    {
//        int d = frames[i - 1];
//        String s = String.Format("Frame {0} : {1}", i, d);
//        Debug.Log(s);
//    }
//    foreach (int frameScore in frames)
//    {
//        String s = String.Format("Running total == {0} + {1} == {2}", runningTotal, frameScore, (runningTotal + frameScore));
//        Debug.Log(s);
//        runningTotal += frameScore;

//        cumulativeScores.Add(runningTotal);
//    }
//    return cumulativeScores;
//}


//private static bool spareActive = false;
//private static int frameNumber = 1;


//public static List<int> ScoreFrames (List<int> rolls)
//{
//    List<int> frameList = new List<int>();
//    int currentBowl = 1;
//    int frameScore = 0;

//    // For debugging
//    int frameNumber = 1;
//    int rollNumber = 1;
//    bool spareActive = false;
//    bool strikeActive = false;
//    bool gameOver = false;

//    int activeStrikes = 0;

//    // My code here
//    //foreach (int roll in rolls)
//    for (int i = 0; i < rolls.Count; i++)
//    {
//        if (gameOver) break;

//        Debug.Log("ScoreFrames: Bowl " + currentBowl + " score = " + rolls[i]);


//        frameScore += rolls[i];

//        // Fulfilling a spare with a strike. Add 10 to the spare and continue to add up for this turn.
//        if (rolls[i] == 10 && spareActive)
//        {
//            Debug.Log("ScoreFrames: Frame " + frameNumber + " Score = " + (frameScore));
//            Debug.Log("ScoreFrames: Bowl " + currentBowl + " score = " + rolls[i]);
//            frameList.Add(frameScore);
//            //frameScore -= 10;
//            frameScore = rolls[i];
//            //Debug.Log("Pending frame score now " + frameScore);
//            frameNumber++;
//            spareActive = false;
//        }

//        if (rolls[i] == 10 && rolls.Count < 3)
//        {

//        }
//        // Got a strike
//        else if (rolls[i] == 10 && rolls.Count >= 3 && currentBowl <= 19)
//        {
//            int score = 10 + rolls[i + 1] + rolls[i + 2];

//            Debug.Log(String.Format("Strike on frame {0}. Score == 10 + {1} + {2} == {3}", frameNumber, rolls[i + 1], rolls[i + 2], score));
//            //Debug.Log("ScoreFrames: Strike Frame " + frameNumber + " Score = " + score);
//            frameList.Add(score);
//            frameNumber++;

//            if (currentBowl < 19)
//                currentBowl += 2;
//            else
//                gameOver = true;

//            frameScore = 0;
//        }

//        else if (OddTurn(currentBowl))
//        {
//            // First handle spare case
//            if (spareActive)
//            {
//                Debug.Log("ScoreFrames: Frame " + frameNumber + " Score = " + (frameScore));
//                Debug.Log("ScoreFrames: Bowl " + currentBowl + " score = " + rolls[i]);
//                frameList.Add(frameScore);
//                //frameScore -= 10;
//                frameScore = rolls[i];
//                //Debug.Log("Pending frame score now " + frameScore);
//                frameNumber++;
//                spareActive = false;
//            }

//            //// Skip second half of frame
//            //if (rolls[i] == 10)
//            //{
//            //    Debug.Log("BBBBBBBBBBBBBBBBB");
//            //    //frameList.Add(10 + rolls[i + 1] + rolls[i + 2]);
//            //    currentBowl++;
//            //    currentBowl++;
//            //    strikeActive = true;
//            //    //activeStrikes++;
//            //}

//            // Proceed as normal
//            //else
//            //{
//                //Debug.Log("Not strike");
//                currentBowl++;
//            //}

//            //if (i == 20) 
//        }

//        else if (EvenTurn(currentBowl))
//        {
//            //if (strikeActive)
//            //{
//            //    Debug.Log("ScoreFrames: Frame " + frameNumber + " Score = " + (frameScore));
//            //    Debug.Log("ScoreFrames: Bowl " + currentBowl + " score = " + rolls[i]);
//            //    frameList.Add(frameScore);
//            //    frameScore -= 10;
//            //    frameNumber++;
//            //    strikeActive = false;
//            //    //activeStrikes--;
//            //}

//            // Got a spare
//            if (frameScore == 10)
//            {
//                Debug.Log("ScoreFrames: Spare Active");
//                spareActive = true;
//            }

//            // Proceed as normal
//            else
//            {
//                Debug.Log("ScoreFrames: Frame " + frameNumber + " Score = " + (frameScore));

//                frameList.Add(frameScore);

//                // Reset frame score.
//                frameScore = 0;
//                frameNumber++;

//            }

//            // Always go to next bowl here.
//            currentBowl++;
//        }
//        rollNumber++;
//    }


//Debug.Log("Frame list: ");
//foreach (int i in frameList) {
//    Debug.Log(i);
//}
//    return frameList;
//}


