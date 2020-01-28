using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    public List<GameObject> waypoints = new List<GameObject>();
    int waypointPtr = 0;
    public List<int> beenToPlaces = new List<int>();
    public List<int> waypointCopy = new List<int>();
    public float speed = 1.0f;
    int randomDirection;
    Vector3 newDestination;
    public bool noMoreMoves = false;
    bool add;
    // Use this for initialization
    void Start()
    {
        newDestination = transform.position;
        //beenToPlaces.Add(0);
        //randomDirection = Random.Range(0, waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges.Count);
        //newDestination = waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[randomDirection].transform.position;
        //waypointPtr = waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[randomDirection].GetComponent<Edge_Controller>().pointValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == newDestination)
        {
            //waypoints[waypointPtr].GetComponent<Edge_Controller>().hasBeenTraversed = true;
            //waypoints[waypointPtr].GetComponent<Edge_Controller>().ren.material.color = Color.red;



            beenToPlaces.Add(waypointPtr);
            //Debug.Log(waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges.Count); THIS IS CORRECT
            //Debug.Log(beenToPlaces.Count); WORKS
            //Debug.Log(waypointCopy.Count);
            for (int i = 0; i < waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges.Count; i++)
            {
                add = true;
                for (int j = 0; j < beenToPlaces.Count; j++)
                {
                    if (waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[i].GetComponent<Edge_Controller>().pointValue == beenToPlaces[j])
                    {
                        add = false;
                        j = beenToPlaces.Count;
                    }
                }
                if (add == true)
                {
                    waypointCopy.Add(waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[i].GetComponent<Edge_Controller>().pointValue);
                }
            }
            //Debug.Log(waypointCopy.Count);
            //for (int i = 0; i < waypointCopy.Count; i++)
            //{
            //    Debug.Log(waypointCopy[i]);
            //}
            if (waypointCopy.Count != 0)
            {
                randomDirection = Random.Range(0, waypointCopy.Count);

                waypointPtr = waypointCopy[randomDirection];
                //Debug.Log(waypointPtr);
                newDestination = waypoints[waypointPtr].transform.position;
                waypointCopy.Clear();
            }
            else
            {
                noMoreMoves = true;
            }
            //randomDirection = Random.Range(0, waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges.Count);
            // for (int i = 0; i < beenToPlaces.Count; i++)
            // {
            //     if (randomDirection == beenToPlaces[i])
            //     {
            //
            //    }
            //}

            //newDestination = waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[randomDirection].transform.position;
            //waypointPtr = waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[randomDirection].GetComponent<Edge_Controller>().pointValue;

        }
        //if (move == true)
        //{
        transform.position = Vector3.MoveTowards(transform.position, newDestination, speed * Time.deltaTime);
        //}


        //Debug.Log("My position = " + transform.position);
        //Debug.Log("My new destination = " + newDestination);


        //randomDirection = Random.Range(0, waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges.Count);
        //Vector3 newDestination = waypoints[waypointPtr].GetComponent<Edge_Controller>().otherEdges[randomDirection].transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, waypoints[randomDirection].transform.position, speed * Time.deltaTime);
        //if (Vector3.Distance(transform.position, waypoints[waypointPtr].transform.position) < 0.001f)
        //{
        //    if (waypointPtr < waypoints.Count - 1)
        //    {
        //        waypointPtr++;
        //    }
        //    else
        //    {
        //        waypointPtr = 0;
        //    }
        //}
    }
}
