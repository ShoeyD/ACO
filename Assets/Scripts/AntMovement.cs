using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{

    //int waypointPtr = 0;
    //public List<int> beenToPlaces = new List<int>();

    public float speed = 1.0f;
    int randomDirection;
    Vector3 newDestination;
    public bool noMoreMoves = false;
    bool add;



    public List<Vector3> beenToPlaces = new List<Vector3>();
    public float tourLength = 0.0f;
    public List<GameObject> waypoints = new List<GameObject>();
    public List<GameObject> waypointCopy = new List<GameObject>();
    public List<float> edgeProbabilities/* = new List<float>()*/;
    float totalEdgeProbabilities = 0.0f;
    GameObject currentPillar;

    // Use this for initialization
    void Start()
    {
        newDestination = transform.position;
        waypointCopy = waypoints;
        //beenToPlaces.Add(0);
        //randomDirection = Random.Range(0, waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges.Count);
        //newDestination = waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[randomDirection].transform.position;
        //waypointPtr = waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[randomDirection].GetComponent<Edge_Controller>().pointValue;
    }

    // Update is called once per frame
    void Update()
    {
        ACOmovement();
    }


    void ACOmovement()
    {
        if (noMoreMoves == false)
        {
            if (transform.position == newDestination)
            {
                beenToPlaces.Add(transform.position);
                for (int i = 0; i < waypointCopy.Count; i++)
                {
                    if (waypointCopy[i].transform.position == transform.position)
                    {
                        currentPillar = waypointCopy[i];
                        waypointCopy.Remove(waypointCopy[i]);
                    }
                }
                if (waypointCopy.Count == 0)
                {
                    noMoreMoves = true;
                    return;
                }
                for (int i = 0; i < currentPillar.GetComponent<Edge_Controller>().edges.Count; i++)
                {
                    for (int j = 0; j < waypointCopy.Count; j++)
                    {
                        if (currentPillar.GetComponent<Edge_Controller>().edges[i].pillarLocation == waypointCopy[j].transform.position)
                        {
                            float scent = currentPillar.GetComponent<Edge_Controller>().edges[i].edgeScent;
                            float visibility = 1 / Vector3.Distance(currentPillar.GetComponent<Edge_Controller>().edges[i].pillarLocation, transform.position);
                            edgeProbabilities.Add(scent * Mathf.Pow(visibility, 5));
                            totalEdgeProbabilities += scent * Mathf.Pow(visibility, 5);
                        }
                    }
                }
                int currentMax = 0;
                float maxValue = 0.0f;
                for (int i = 0; i < edgeProbabilities.Count; i++)
                {
                    edgeProbabilities[i] = edgeProbabilities[i] / (totalEdgeProbabilities - edgeProbabilities[i]);
                    if (edgeProbabilities[i] > maxValue)
                    {
                        currentMax = i;
                        maxValue = edgeProbabilities[i];
                    }
                }
                newDestination = waypointCopy[currentMax].transform.position;
                tourLength += Vector3.Distance(newDestination, transform.position);
            }
            transform.position = Vector3.MoveTowards(transform.position, newDestination, speed * Time.deltaTime);
        }
    }


    //void OldMovement()
    //{
    //    if (transform.position == newDestination)
    //    {
    //        //waypoints[waypointPtr].GetComponent<Edge_Controller>().hasBeenTraversed = true;
    //        //waypoints[waypointPtr].GetComponent<Edge_Controller>().ren.material.color = Color.red;



    //        beenToPlaces.Add(waypointPtr);
    //        //Debug.Log(waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges.Count); THIS IS CORRECT
    //        //Debug.Log(beenToPlaces.Count); WORKS
    //        //Debug.Log(waypointCopy.Count);
    //        for (int i = 0; i < waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges.Count; i++)
    //        {
    //            add = true;
    //            for (int j = 0; j < beenToPlaces.Count; j++)
    //            {
    //                if (waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[i].GetComponent<Edge_Controller>().pointValue == beenToPlaces[j])
    //                {
    //                    add = false;
    //                    j = beenToPlaces.Count;
    //                }
    //            }
    //            if (add == true)
    //            {
    //                waypointCopy.Add(waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[i].GetComponent<Edge_Controller>().pointValue);
    //            }
    //        }
    //        //Debug.Log(waypointCopy.Count);
    //        //for (int i = 0; i < waypointCopy.Count; i++)
    //        //{
    //        //    Debug.Log(waypointCopy[i]);
    //        //}
    //        if (waypointCopy.Count != 0)
    //        {
    //            randomDirection = Random.Range(0, waypointCopy.Count);

    //            waypointPtr = waypointCopy[randomDirection];
    //            //Debug.Log(waypointPtr);
    //            newDestination = waypoints[waypointPtr].transform.position;
    //            waypointCopy.Clear();
    //        }
    //        else
    //        {
    //            noMoreMoves = true;
    //        }
    //        //randomDirection = Random.Range(0, waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges.Count);
    //        // for (int i = 0; i < beenToPlaces.Count; i++)
    //        // {
    //        //     if (randomDirection == beenToPlaces[i])
    //        //     {
    //        //
    //        //    }
    //        //}

    //        //newDestination = waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[randomDirection].transform.position;
    //        //waypointPtr = waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[randomDirection].GetComponent<Edge_Controller>().pointValue;

    //    }
    //    //if (move == true)
    //    //{
    //    transform.position = Vector3.MoveTowards(transform.position, newDestination, speed * Time.deltaTime);
    //    //}


    //    //Debug.Log("My position = " + transform.position);
    //    //Debug.Log("My new destination = " + newDestination);


    //    //randomDirection = Random.Range(0, waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges.Count);
    //    //Vector3 newDestination = waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[randomDirection].transform.position;
    //    //transform.position = Vector3.MoveTowards(transform.position, waypoints[randomDirection].transform.position, speed * Time.deltaTime);
    //    //if (Vector3.Distance(transform.position, waypoints[waypointPtr].transform.position) < 0.001f)
    //    //{
    //    //    if (waypointPtr < waypoints.Count - 1)
    //    //    {
    //    //        waypointPtr++;
    //    //    }
    //    //    else
    //    //    {
    //    //        waypointPtr = 0;
    //    //    }
    //    //}

    //}
}
