﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    private ActionMaster actionMaster;

    [SetUp]
    public void Setup()
    {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T00_TestExample()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_OneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02_Bowl8ReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03_Bowl8then2SpareReturnEndTurn()
    {
        actionMaster.Bowl(8);
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T04_Bowl3then5ReturnEndTurn()
    {
        actionMaster.Bowl(3);
        Assert.AreEqual(endTurn, actionMaster.Bowl(5));
    }

    [Test]
    public void T05_Bowl0ReturnTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T06_Bowl10StrikesReturnsEndTurn()
    {
        // Bowl strikes 9 times
        int[] bowls = { 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        foreach (int bowl in bowls)
        {
            actionMaster.Bowl(bowl);
        }

        // Tenth strike should reset
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T07_Bowl11StrikesReturnsEndTurn()
    {
        // Bowl strikes 10 times
        int[] bowls = new int[10];
        foreach (int bowl in bowls) {
            actionMaster.Bowl(10);
        }

        // Eleventh strike should reset
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T08_Bowl12StrikesReturnsEndGame()
    {
        // Bowl eleven strikes
        int[] bowls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        foreach (int bowl in bowls)
        {
            actionMaster.Bowl(bowl);
        }

        // Twelfth strike should end game
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T09_Bowl7Turn19Then3Turn20ReturnsResetBecauseSpare()
    {
        // Bowl 19 fives
        int[] bowls = new int[18];
        foreach (int bowl in bowls) { 
            actionMaster.Bowl(5);
        }

        actionMaster.Bowl(7);

        // Bowl 20 should return reset -- because it is a spare
        Assert.AreEqual(reset, actionMaster.Bowl(3));
    }

    [Test]
    public void T10_BowlAll5sReturnsEndGameOnTurn21()
    {
        // Bowl 20 fives
        int[] bowls = new int[20];
        foreach (int bowl in bowls)
        {
            actionMaster.Bowl(5);
        }

        // Bowl 21 should end game
        Assert.AreEqual(endGame, actionMaster.Bowl(5));
    }

    [Test]
    public void T11_BowlAll5sAndThenStrikeTurn19ReturnsReset()
    {
        // Bowl 18 fives
        int[] bowls = new int[18];
        foreach (int bowl in bowls)
        {
            actionMaster.Bowl(5);
        }

        // Strike on turn 19 should reset
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T12_BowlAll5sAndThenStrikeTurn20ReturnsReset()
    {
        // Bowl 19 fives
        int[] bowls = new int[19];
        foreach (int bowl in bowls)
        {
            actionMaster.Bowl(5);
        }

        // Strike on turn 20 should reset
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T13_BowlAll5sAndThen3Turn21ReturnsEndGame()
    {
        // Bowl 20 fives
        int[] bowls = new int[20];
        foreach (int bowl in bowls)
        {
            actionMaster.Bowl(5);
        }

        // 3 on turn 21 should end game
        Assert.AreEqual(endGame, actionMaster.Bowl(3));
    }

    [Test]
    public void T14_Bowl0then10ReturnsEndTurn()
    {
        // First bowl 0
        actionMaster.Bowl(0);

        // Then roll 10. Should end turn.
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T15_Bowl5Then7NotAllowed()
    {
        // first bowl 0
        actionMaster.Bowl(5);

        // then roll 10. should end turn.
        try
        {
            actionMaster.Bowl(9);
        }
        catch (UnityException e)
        {
            Assert.AreEqual("Too many pins!", e.Message);
        }
    }

    [Test]
    public void T15_Bowl12ThrowsException()
    {
        try
        {
            actionMaster.Bowl(12);
        }
        catch (UnityException e)
        {
            Assert.AreEqual("Invalid number of pins!", e.Message);
        }
    }

    [Test]
    public void T16_Bowl0FirstRollReturnsTidy()
    {
        // Then roll 10. Should end turn.
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T17_Bowl0Then0ReturnsEndTurn()
    {
        actionMaster.Bowl(0);

        // Then roll 10. Should end turn.
        Assert.AreEqual(endTurn, actionMaster.Bowl(0));
    }

    [Test]
    public void T18_YoutubeRollsEndInEndGame()
    {
        int[] rolls = { 8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 8,2 }; // 10 turns... awarded final roll
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }

        // Then roll 9. Should end game.
        Assert.AreEqual(endGame, actionMaster.Bowl(9));
    }

    [Test]
    public void T19_DarylStrikeTurn19AndNot10Roll20ShouldReturnTidy()
    {
        int[] rolls = { 8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 10 }; // strike turn 19
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }

        // Then roll not 10. Should return tidy.
        Assert.AreEqual(tidy, actionMaster.Bowl(2));
    }

    [Test]
    public void T20_StrikeTurn19And0Roll20ShouldReturnTidy()
    {
        int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 10 }; // strike turn 19
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }

        // Then roll not 10. Should return tidy.
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T21_KnockDown10PinsOnSecondBowlInFrameShouldIncrementBowlBy1()
    {
        int[] rolls = { 0,10, 0,10, 0,10, 0,10, 0,10, 0,10, 0,10, 0,10, 0,0, 0 };
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }

        Assert.AreEqual(20, actionMaster.GetCurrentBowl());

        // Then roll 0. Should end game.
        Assert.AreEqual(endGame, actionMaster.Bowl(0));
    }

    [Test]
    public void T22_Bowl13StrikesThrowsExceptionGameAlreadyOver()
    {
        int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }

        Debug.Log(actionMaster.GetCurrentBowl());

        try {
            actionMaster.Bowl(10);
            throw new Exception("Test should have failed!");
        }
        catch (UnityException e)
        {
            Assert.AreEqual("Game already over!", e.Message);
        }
    }

    [Test]
    public void T23_CurrentBowlStartsAt1()
    {
        Assert.AreEqual(actionMaster.GetCurrentBowl(), 1);
    }

    [Test]
    public void T24_CurrentBowlIncreasesBy2WithStrikeFirstFrame()
    {
        actionMaster.Bowl(10);
        Assert.AreEqual(actionMaster.GetCurrentBowl(), 3);
    }

    [Test]
    public void T25_CurrentBowlIncreasesBy1WithStrikeSecondFrame()
    {
        actionMaster.Bowl(0);
        actionMaster.Bowl(10);
        Assert.AreEqual(actionMaster.GetCurrentBowl(), 3);
    }

    [Test]
    public void T26_0Then10Then5Then1ShouldEndTurn()
    {
        int[] rolls = { 0, 10, 5 };
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }

        // 1 should now return end turn -- second ball of second frame
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }

    [Test]
    public void T27_Dondi10thFrameTurkey()
    {
        int[] rolls = { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1 };
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }

        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }
}