using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ScoreDisplayTest : MonoBehaviour {

    [Test]
    public void T00_TestExample()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01Bowl1()
    {
        int[] rolls = { 1 };
        string rollsString = "1";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T02Bowl123()
    {
        int[] rolls = { 1, 2, 3 };
        string rollsString = "123";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T03BowlStrike()
    {
        int[] rolls = { 10 };
        string rollsString = "X "; // remember the space
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    // Write tests that force me to write new code
    [Test]
    public void T04BowlSpare()
    {
        int[] rolls = { 1, 9 };
        string rollsString = "1/";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T05Bowl191()
    {
        int[] rolls = { 1, 9, 1 };
        string rollsString = "1/1";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T06BowlXXX()
    {
        int[] rolls = { 10, 10, 10};
        string rollsString = "X X X ";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T07BowlSpareLastFrame()
    {
        int[] rolls = { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,9,3 };
        string rollsString = "1111111111111111111/3";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T08BowlStrikeLastFrame()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 1, 1};
        string rollsString = "111111111111111111X11";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T09BowlZero()
    {
        int[] rolls = { 0 };
        string rollsString = "-";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
    [Test]
    public void T10Bowl0Ten()
    {
        int[] rolls = { 0, 10 };
        string rollsString = "-/";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
}
