using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {

    // Action enum
    public enum Action { Tidy, Reset, EndTurn, EndGame };

    // This should change
    private int[] bowls = new int[21];

    private int currentBowl = 1;
    private bool gameIsOver = false;

    public static Action NextAction (List<int> pinFalls)
    {
        ActionMaster actionMaster = new ActionMaster();
        Action action = new Action();

        foreach (int pinFall in pinFalls)
        {
            action = actionMaster.Bowl(pinFall);
        }

        return action;
    }

	private Action Bowl (int pinsKnockedDown)
    {

        if (pinsKnockedDown < 0 || pinsKnockedDown > 10) { throw new UnityException("Invalid number of pins!"); }
        if (gameIsOver) { throw new UnityException("Game already over!"); }

        // Assign bowl to right spot.
        SetCurrentTurnScore(pinsKnockedDown);

        if (currentBowl < 19)
        {
            // First ball of frame.
            if (currentBowl % 2 != 0)
            {
                // Strike
                if (GotASrike(pinsKnockedDown))
                {
                    currentBowl += 2;

                    // Return action to pinsetter
                    return Action.EndTurn;
                }
                else
                {
                    currentBowl++;
                    return Action.Tidy;
                }
            }

            // End of frame
            else
            {
                currentBowl++;
                return Action.EndTurn;
            }
        }

        else if (currentBowl == 19)
        {
            // Strike
            if (GotASrike(pinsKnockedDown))
            {
                currentBowl++;
                return Action.Reset;
            }
            else
            {
                currentBowl++;
                return Action.Tidy;
            }
        }

        else if (currentBowl == 20)
        {
            if (GotASrike(pinsKnockedDown) || (GotASpare(pinsKnockedDown) && pinsKnockedDown != 0))
            {
                currentBowl++;
                return Action.Reset;
            }

            else if (GetLastTurnsScore() == 10)
            {
                currentBowl++;
                return Action.Tidy;
            }

            else
            {
                gameIsOver = true;
                return Action.EndGame;
            }
        }

        // Last frame always end game
        else
        {
            gameIsOver = true;
            return Action.EndGame;
        }



        //// Strike first frame turns 1-9 -> end turn
        //if (GotAStrike(pinsKnockedDown) && currentBowl % 2 != 0 && currentBowl < 19)
        //{
        //    currentBowl += 2;
        //    return Action.EndTurn;
        //}

        //// Strike bowl 19 or 20 -> reset
        //else if (GotAStrike(pinsKnockedDown) && (currentBowl == 19 || currentBowl == 20))
        //{
        //    currentBowl += 1;
        //    return Action.Reset;
        //}

        //// Strike turn 19, not 10 on turn 20
        //else if (currentBowl == 20 && pinsKnockedDown < 10 && GetLastTurnsScore() == 10)
        //{
        //    currentBowl += 1;
        //    return Action.Tidy;
        //}

        //// Turn 10 frame 2 and spare
        //else if (currentBowl == 20 && GotASpare(pinsKnockedDown))
        //{
        //    currentBowl += 1;
        //    return Action.Reset;
        //}

        //// Turn 20 no strike or spare
        //else if (currentBowl == 20)
        //{
        //    gameIsOver = true;
        //    return Action.EndGame;
        //}

        //// Last frame always end game
        //else if (currentBowl == 21)
        //{
        //    gameIsOver = true;
        //    return Action.EndGame;
        //}

        //// First ball of frame.
        //else if (currentBowl % 2 != 0)
        //{
        //    currentBowl++;
        //    return Action.Tidy;
        //}

        //// End of frame
        //else if (currentBowl % 2 == 0)
        //{
        //    currentBowl++;
        //    return Action.EndTurn;
        //}

        throw new UnityException("Not sure what action to return!");
    }

    // Insert current turn into the scoreboard.
    private void SetCurrentTurnScore(int pinsKnockedDown)
    {
        bowls[currentBowl-1] = pinsKnockedDown;
    }

    // Get last turn's score.
    private int GetLastTurnsScore()
    {
        return bowls[currentBowl-1 - 1];
    }
    
    public int GetCurrentBowl()
    {
        return currentBowl;
    }

    private bool GotASpare(int pinsKnockedDown)
    {
        return (GetLastTurnsScore() + pinsKnockedDown == 10);
    }

    private bool GotASrike(int pinsKnockedDown)
    {
        return pinsKnockedDown == 10;
    }
}
