using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class XCoordinateFinder
{
    public static int findXCoordinate(float xPos)
    {
        int xCoord = 0;  

        if (Math.Abs(xPos+334.1088)<1) xCoord = 0; 
        else if (Math.Abs(xPos+34.10876)<1) xCoord = 1; 
        else if (Math.Abs(xPos-265.8912)<1) xCoord = 2; 

        return xCoord;
    }
}
