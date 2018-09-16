using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    private List<int> pinFalls;

    [SetUp]
    public void Setup()
    {
        pinFalls = new List<int>();
    }

    [Test]
    public void T00_TestExample()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_OneStrikeReturnsEndTurn()
    {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T02_Bowl8ReturnsTidy()
    {
        int[] rolls = { 9 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T03_Bowl8then2SpareReturnEndTurn()
    {
        int[] rolls = { 8, 2 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T04_Bowl3then5ReturnEndTurn()
    {
        int[] bowls = { 3,5 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T05_Bowl0ReturnTidy()
    {
        int[] bowls = { 0 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T06_Bowl10StrikesReturnsEndTurn()
    {
        // Bowl strikes 10 times
        int[] bowls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

        // Tenth strike should reset
        Assert.AreEqual(reset, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T07_Bowl11StrikesReturnsEndTurn()
    {
        // Bowl strikes 11 times
        int[] bowls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

        // Eleventh strike should reset
        Assert.AreEqual(reset, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T08_Bowl12StrikesReturnsEndGame()
    {
        // Bowl 12 strikes
        int[] bowls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

        // Twelfth strike should end game
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T09_Bowl7Turn19Then3Turn20ReturnsResetBecauseSpare()
    {
        // Bowl 7 on turn 19, and 3 on turn 20
        int[] bowls = { 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 7,3 };

        // Bowl 20 should return reset -- because it is a spare
        Assert.AreEqual(reset, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T10_BowlAll5sReturnsEndGameOnTurn21()
    {
        // Bowl 21 fives
        int[] bowls = { 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5 };

        // Bowl 21 should end game
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T11_BowlAll5sAndThenStrikeTurn19ReturnsReset()
    {
        // Bowl 18 fives and then strike on turn 19
        int[] bowls = { 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 10 };

        // Strike on turn 19 should reset
        Assert.AreEqual(reset, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T12_BowlAll5sAndThen3Turn21ReturnsEndGame()
    {
        // Bowl 20 fives and 3 on turn 21
        int[] bowls = { 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 3 };

        // 3 on turn 21 should end game
        Assert.AreEqual(endGame, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T13_Bowl0then10ReturnsEndTurn()
    {
        // First bowl 0 then roll 10
        int[] bowls = { 0,10 };

        // Then roll 10. Should end turn.
        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T14_Bowl5Then9NotAllowed()
    {
        // first bowl 5, then roll 9
        int[] bowls = { 5, 9 };

        // Should error
        try
        {
            ActionMaster.NextAction(bowls.ToList());
        }
        catch (UnityException e)
        {
            Assert.AreEqual("Too many pins!", e.Message);
        }
    }

    [Test]
    public void T15_Bowl12ThrowsException()
    {
        int[] bowls = { 12 };
        try
        {
            ActionMaster.NextAction(bowls.ToList());
        }
        catch (UnityException e)
        {
            Assert.AreEqual("Invalid number of pins!", e.Message);
        }
    }

    [Test]
    public void T16_Bowl0FirstRollReturnsTidy()
    {
        int[] bowls = { 0 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T17_Bowl0Then0ReturnsEndTurn()
    {
        int[] bowls = { 0, 0 };

        // Then roll 0. Should end turn.
        Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T18_YoutubeRollsEndInEndGame()
    {
        int[] rolls = { 8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 8,2, 9 }; // 10 turns... awarded final roll

        // Then roll 9. Should end game.
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T19_DarylStrikeTurn19AndNot10Roll20ShouldReturnTidy()
    {
        // 10 roll 19, not 10 roll 20
        int[] rolls = { 8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 10, 2 }; // strike turn 19

        // Then roll not 10. Should return tidy.
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T20_StrikeTurn19And0Roll20ShouldReturnTidy()
    {
        int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 10, 0 }; // strike turn 19

        // Then roll not 10. Should return tidy.
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T21_KnockDown10PinsOnSecondBowlInFrameShouldIncrementBowlBy1()
    {
        int[] rolls = { 0,10, 0,10, 0,10, 0,10, 0,10, 0,10, 0,10, 0,10, 0,0, 0, 0 };

        // Then roll 0. Should end game.
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T22_Bowl13StrikesThrowsExceptionGameAlreadyOver()
    {
        int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

        try {
            ActionMaster.NextAction(rolls.ToList());
            throw new Exception("Test should have failed!");
        }
        catch (UnityException e)
        {
            Assert.AreEqual("Game already over!", e.Message);
        }
    }

    //[Test]
    //public void T23_CurrentBowlStartsAt1()
    //{
    //    Assert.AreEqual(actionMaster.GetCurrentBowl(), 1);
    //}

    //[Test]
    //public void T24_CurrentBowlIncreasesBy2WithStrikeFirstFrame()
    //{
    //    actionMaster.Bowl(10);
    //    Assert.AreEqual(actionMaster.GetCurrentBowl(), 3);
    //}

    //[Test]
    //public void T25_CurrentBowlIncreasesBy1WithStrikeSecondFrame()
    //{
    //    actionMaster.Bowl(0);
    //    actionMaster.Bowl(10);
    //    Assert.AreEqual(actionMaster.GetCurrentBowl(), 3);
    //}

    [Test]
    public void T26_0Then10Then5Then1ShouldEndTurn()
    {
        int[] rolls = { 0, 10, 5, 1 };

        // 1 should now return end turn -- second ball of second frame
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T27_Dondi10thFrameTurkey()
    {
        int[] rolls = { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10, 10, 10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }
}