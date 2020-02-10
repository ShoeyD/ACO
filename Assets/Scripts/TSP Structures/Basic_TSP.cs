using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_TSP 
{
    private const int AMOUNT_OF_POINTS = 7;
    public Vector3[] pPositions = new Vector3[AMOUNT_OF_POINTS];

    public Basic_TSP(Vector3 floor)
    {
        Vector3 floorSize = floor;
        pPositions[0] = new Vector3(-floorSize.x * 0.3f, 0.0f, -floorSize.z * 0.4f);
        pPositions[1] = new Vector3(floorSize.x * 0.2f, 0.0f, -floorSize.z * 0.3f);
        pPositions[2] = new Vector3(floorSize.x * 0.4f, 0.0f, -floorSize.z * 0.1f);
        pPositions[3] = new Vector3(floorSize.x * 0.1f, 0.0f, floorSize.z * 0.4f);
        pPositions[4] = new Vector3(-floorSize.x * 0.4f, 0.0f, floorSize.z * 0.3f);
        pPositions[5] = new Vector3(0.0f, 0.0f, 0.0f);
        pPositions[6] = new Vector3(floorSize.x * 0.3f, 0.0f, floorSize.z * 0.1f);
    }
}
