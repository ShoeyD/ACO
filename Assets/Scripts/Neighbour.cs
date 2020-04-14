using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighbour
{
    public GameObject startPosition;
    public GameObject newPosition;
    public int runTime;

    private int smallestDistancePointer;
    private float smallestDistance;

    public List<GameObject> beenToPlaces = new List<GameObject>();
    public float path = 0.0f;
    public List<GameObject> waypoints = new List<GameObject>();
    bool skip = false;

    public void Move()
    {
        for (int j = 0; j < runTime; j++)
        {
            smallestDistance = 99999f;
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (waypoints[i].transform.position == startPosition.transform.position)
                {
                    beenToPlaces.Add(waypoints[i]);
                    i = waypoints.Count;
                }
            }
            for (int i = 0; i < waypoints.Count; i++)
            {
                for (int k = 0; k < beenToPlaces.Count; k++)
                {
                    if (waypoints[i].transform.position == beenToPlaces[k].transform.position)
                    {
                        skip = true;
                        k = waypoints.Count;
                    }
                    else
                    {
                        skip = false;
                    }
                }
                if (Vector3.Distance(startPosition.transform.position, waypoints[i].transform.position) < smallestDistance && skip == false)
                {
                    smallestDistance = Vector3.Distance(startPosition.transform.position, waypoints[i].transform.position);
                    smallestDistancePointer = i;
                }
            }
            if (smallestDistance == 99999f)
            {
                smallestDistance = 0.0f;
            }
            path += smallestDistance;
            startPosition = waypoints[smallestDistancePointer];
        }
    }
}