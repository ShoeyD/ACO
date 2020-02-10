using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Controller : MonoBehaviour
{
    public GameObject pillarPrefab;
    public GameObject antPrefab;
    public List<GameObject> antInstances;
    public GameObject floor;

    public float constantCalledQ = 1.0f;

    public int antAmount;
    public bool allAntsDone;

    Basic_TSP bTSP;

    private List<GameObject> pillars = new List<GameObject>();

    public float shortestDistance = 999f;

    // Use this for initialization
    void Start()
    {
        bTSP = new Basic_TSP(floor.GetComponent<MeshRenderer>().bounds.size);
        SpawnPillarsAndAnts();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAntsAreDone();
    }

    void CheckAntsAreDone()
    {
        int antsDone = 0;
        for (int i = 0; i < antAmount; i++)
        {
            if (antInstances[i].GetComponent<AntMovement>().noMoreMoves == true)
            {
                antsDone++;
            }
        }
        if (antsDone == antAmount)
        {
            for (int i = 0; i < pillars.Count; i++)
            {
                for (int j = 0; j < pillars[i].GetComponent<Edge_Controller>().edges.Count; j++)
                {
                    pillars[i].GetComponent<Edge_Controller>().edges[j].edgeScent *= 0.5f;
                }
            }

            int antLength = antInstances.Count;

            for (int i = 0; i < antLength; i++)
            {
                if (shortestDistance > antInstances[0].GetComponent<AntMovement>().tourLength)
                {
                    shortestDistance = antInstances[0].GetComponent<AntMovement>().tourLength;
                }
                //Add scent here
                for (int j = 0; j < antInstances[0].GetComponent<AntMovement>().beenToPlaces.Count - 1; j++)
                {
                    for (int k = 0; k < antInstances[0].GetComponent<AntMovement>().beenToPlaces[j].GetComponent<Edge_Controller>().edges.Count; k++)
                    {
                        if (antInstances[0].GetComponent<AntMovement>().beenToPlaces[j + 1].transform.position == antInstances[0].GetComponent<AntMovement>().beenToPlaces[j].GetComponent<Edge_Controller>().edges[k].pillarLocation)
                        {
                            antInstances[0].GetComponent<AntMovement>().beenToPlaces[j].GetComponent<Edge_Controller>().edges[k].edgeScent += constantCalledQ / antInstances[0].GetComponent<AntMovement>().tourLength;
                        }
                    }
                }

                Destroy(antInstances[0].gameObject);
                antInstances.Remove(antInstances[0]);
            }
            int rLocation;
            for (int i = 0; i < antAmount; i++)
            {
                rLocation = Random.Range(0, pillars.Count);
                antInstances.Add(Instantiate(antPrefab, pillars[rLocation].transform.position, Quaternion.identity));
                antInstances[i].GetComponent<AntMovement>().newDestination = pillars[rLocation];
            }
            Debug.Log(shortestDistance);
        }
    }

    void SpawnPillarsAndAnts()
    {
        int rLocation;
        antPrefab.GetComponent<AntMovement>().waypoints = pillars;
        for (int i = 0; i < bTSP.pPositions.Length; i++)
        {
            GameObject p = Instantiate(pillarPrefab, bTSP.pPositions[i], Quaternion.identity);
            pillars.Add(p);
        }
        for (int i = 0; i <pillars.Count; i++)
        {
            for (int j = 0; j < pillars.Count; j++)
            {
                if (i != j)
                {
                    pillars[i].GetComponent<Edge_Controller>().edges.Add(new Edge(pillars[j], pillars[j].transform.position));
                }
            }
        }
        for (int i = 0; i < antAmount; i++)
        {
            rLocation = Random.Range(0, pillars.Count);
            antInstances.Add(Instantiate(antPrefab, pillars[rLocation].transform.position, Quaternion.identity));
            antInstances[i].GetComponent<AntMovement>().newDestination = pillars[rLocation];
        }
    }
}
