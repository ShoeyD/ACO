using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{

    //int waypointPtr = 0;
    //public List<int> beenToPlaces = new List<int>();

    public float speed = 1.0f;
    int randomDirection;
    public GameObject newDestination;
    public bool noMoreMoves = false;
    bool add;



    public List<GameObject> beenToPlaces = new List<GameObject>();
    public float tourLength = 0.0f;
    public List<GameObject> waypoints = new List<GameObject>();
    public List<GameObject> waypointCopy = new List<GameObject>();
    public List<float> edgeProbabilities/* = new List<float>()*/;
    float totalEdgeProbabilities = 0.0f;
    GameObject currentPillar;

    // Use this for initialization
    void Start()
    {
        waypointCopy = waypoints;
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
            if (transform.position == newDestination.transform.position)
            {
                beenToPlaces.Add(newDestination);
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
                newDestination = waypointCopy[currentMax];
                tourLength += Vector3.Distance(newDestination.transform.position, transform.position);
            }
            transform.position = Vector3.MoveTowards(transform.position, newDestination.transform.position, speed * Time.deltaTime);
            edgeProbabilities.Clear();
        }
    }
}
