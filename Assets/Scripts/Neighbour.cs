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
    //public List<GameObject> waypointCopy = new List<GameObject>();
    //public List<float> edgeProbabilities/* = new List<float>()*/;
    //float totalEdgeProbabilities = 0.0f;
    //GameObject currentPillar;

    public void Move()
    {
        //waypointCopy = waypoints;
        //Debug.Log("Run time = " + runTime);
        //Debug.Log("Amount of pillars = " + waypoints.Count);
        for (int j = 0; j < runTime; j++)
        {
            smallestDistance = 99999f;
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (waypoints[i].transform.position == startPosition.transform.position)
                {
                    beenToPlaces.Add(waypoints[i]);
                    //waypointCopy.Remove(waypointCopy[i]);
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
                    //Debug.Log(Vector3.Distance(startPosition.transform.position, waypointCopy[i].transform.position));
                    smallestDistance = Vector3.Distance(startPosition.transform.position, waypoints[i].transform.position);
                    smallestDistancePointer = i;
                }
            }
            if (smallestDistance == 99999f)
            {
                smallestDistance = 0.0f;
            }
            //Debug.Log("Smallest distance in the loop = " + smallestDistance);
            path += smallestDistance;
            //Debug.Log("After adding my value of path is " + path);
            startPosition = waypoints[smallestDistancePointer];
        }
        //Debug.Log("Path to traverse everywhere = " + path);

        //if (noMoreMoves == false)
        //{
        //    if (transform.position == newDestination.transform.position)
        //    {
        //        beenToPlaces.Add(newDestination);
        //        for (int i = 0; i < waypointCopy.Count; i++)
        //        {
        //            if (waypointCopy[i].transform.position == transform.position)
        //            {
        //                currentPillar = waypointCopy[i];
        //                waypointCopy.Remove(waypointCopy[i]);
        //            }
        //        }
        //        if (waypointCopy.Count == 0)
        //        {
        //            noMoreMoves = true;
        //            return;
        //        }
        //        for (int i = 0; i < currentPillar.GetComponent<Edge_Controller>().edges.Count; i++)
        //        {
        //            for (int j = 0; j < waypointCopy.Count; j++)
        //            {
        //                if (currentPillar.GetComponent<Edge_Controller>().edges[i].pillarLocation == waypointCopy[j].transform.position)
        //                {
        //                    float scent = currentPillar.GetComponent<Edge_Controller>().edges[i].edgeScent;
        //                    float visibility = 1 / Vector3.Distance(currentPillar.GetComponent<Edge_Controller>().edges[i].pillarLocation, transform.position);
        //                    edgeProbabilities.Add(scent * Mathf.Pow(visibility, 5));
        //                    totalEdgeProbabilities += scent * Mathf.Pow(visibility, 5);
        //                }
        //            }
        //        }
        //        int currentMax = 0;
        //        float maxValue = 0.0f;
        //        for (int i = 0; i < edgeProbabilities.Count; i++)
        //        {
        //            edgeProbabilities[i] = edgeProbabilities[i] / (totalEdgeProbabilities - edgeProbabilities[i]);
        //            if (edgeProbabilities[i] > maxValue)
        //            {
        //                currentMax = i;
        //                maxValue = edgeProbabilities[i];
        //            }
        //        }
        //        newDestination = waypointCopy[currentMax];
        //        tourLength += Vector3.Distance(newDestination.transform.position, transform.position);
        //    }
        //    transform.position = Vector3.MoveTowards(transform.position, newDestination.transform.position, speed * Time.deltaTime);
        //    edgeProbabilities.Clear();
        //}
    }
}