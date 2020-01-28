using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseWithGarage
{
    private const int AMOUNT_OF_POINTS = 8;
    public Vector3[] pPositions = new Vector3[AMOUNT_OF_POINTS];
    public List<int[]> pEdges = new List<int[]>();

    public HouseWithGarage(Vector3 floor)
    {
        Vector3 floorSize = floor;
        //Vector3 floorSize = floor.GetComponent<MeshRenderer>().bounds.size;
        pPositions[0] = new Vector3(-floorSize.x * 0.3f, 0.0f, -floorSize.z * 0.3f);
        pPositions[1] = new Vector3(0.0f, 0.0f, -floorSize.z * 0.3f);
        pPositions[2] = new Vector3(floorSize.x * 0.3f, 0.0f, -floorSize.z * 0.3f);
        pPositions[3] = new Vector3(-floorSize.x * 0.15f, 0.0f, -floorSize.z * 0.15f);
        pPositions[4] = new Vector3(-floorSize.x * 0.3f, 0.0f, 0.0f);
        pPositions[5] = new Vector3(0.0f, 0.0f, 0.0f);
        pPositions[6] = new Vector3(floorSize.x * 0.3f, 0.0f, 0.0f);
        pPositions[7] = new Vector3(-floorSize.x * 0.15f, 0.0f, floorSize.z * 0.15f);
        pEdges.Add(new[] { 1, 3, 4 });
        pEdges.Add(new[] { 0, 2, 3, 5 });
        pEdges.Add(new[] { 1, 5, 6 });
        pEdges.Add(new[] { 0, 1, 4, 5 });
        pEdges.Add(new[] { 0, 3, 5, 7 });
        pEdges.Add(new[] { 1, 2, 3, 4, 6, 7 });
        pEdges.Add(new[] { 2, 5 });
        pEdges.Add(new[] { 4, 5 });


    }
}
