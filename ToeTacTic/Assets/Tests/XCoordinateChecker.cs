using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class XCoordinateChecker
{
    // A Test behaves as an ordinary method
    [Test]
    public void ChecksXCoordinate0()
    {
        int xCoord = XCoordinateFinder.findXCoordinate((float) -334.11);
        Assert.AreEqual(0, xCoord);
    }
    public void ChecksXCoordinate1()
    {
        int xCoord = XCoordinateFinder.findXCoordinate((float) -34.11);
        Assert.AreEqual(1, xCoord);
    }
    public void ChecksXCoordinate2()
    {
        int xCoord = XCoordinateFinder.findXCoordinate((float) 265.89);
        Assert.AreEqual(2, xCoord);
    }
}
