using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{

    public const int mapX = 20;
    public const int mapY = 10;

    public const float tileSize = 1.3f;
    public const float tileOffSetX = 0.65f;
    public const float tileOffSetY = 0.65f;



    private static int[][] dimensions = {new int[] { 0, 1 },
       new int[] { 0, -1 }, new int[] { -1, 0 }, new int[] { 1, 0 } };

    public static int[][] getDimensions()
    {
        return dimensions;
    }
}