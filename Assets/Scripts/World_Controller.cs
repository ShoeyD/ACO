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

    private int antAmount;
    public bool allAntsDone;

    Basic_TSP bTSP;

    public List<GameObject> pillars = new List<GameObject>();

    public float shortestDistance = 99999f;
    private GameObject bestAnt;
    public LineRenderer bestAntLine;
    public bool pressButton = true;
    public Canvas buttons;
    public bool spawnPillars = false;
    public bool destroyPillars = false;
    public bool runNN = true;
    public int antAttempts = 0;
    public float deltaTime = 0.0f;

    public float nnPath;

    // Use this for initialization
    void Start()
    {
        buttons.enabled = true;
        bTSP = new Basic_TSP(floor.GetComponent<MeshRenderer>().bounds.size);
        SpawnPillarsAndAnts();
        antAmount = pillars.Count;
        GameObject.Find("Number of Pillars").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Number of Pillars = " + pillars.Count;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAntsAreDone();
        SpawnPillar();
        DestroyPillar();
    }

    void CheckAntsAreDone()
    {
        int antsDone = 0;
        for (int i = 0; i < antInstances.Count; i++)
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
                    bestAnt = antInstances[0].gameObject;
                    bestAntLine.positionCount = bestAnt.GetComponent<AntMovement>().beenToPlaces.Count;
                    for (int j = 0; j < bestAnt.GetComponent<AntMovement>().beenToPlaces.Count; j++)
                    {
                        bestAntLine.SetPosition(j, bestAnt.GetComponent<AntMovement>().beenToPlaces[j].transform.position);
                    }
                }
                //Scent
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
            antAttempts++;
            GameObject.Find("Number of Attempts").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Number of Attempts = " + antAttempts;
            GameObject.Find("Spawn Ants").GetComponentInChildren<UnityEngine.UI.Text>().text = "Spawn Ants";
            pressButton = true;
            runNN = true;
            GameObject.Find("ACO Best").GetComponentInChildren<UnityEngine.UI.Text>().text = "ACO's Best Path = " + shortestDistance;
        }
    }

    void SpawnPillarsAndAnts()
    {
        antPrefab.GetComponent<AntMovement>().waypoints = pillars;
        for (int i = 0; i < bTSP.pPositions.Length; i++)
        {
            GameObject p = Instantiate(pillarPrefab, bTSP.pPositions[i], Quaternion.identity);
            pillars.Add(p);
        }
        for (int i = 0; i < pillars.Count; i++)
        {
            for (int j = 0; j < pillars.Count; j++)
            {
                if (i != j)
                {
                    pillars[i].GetComponent<Edge_Controller>().edges.Add(new Edge(pillars[j], pillars[j].transform.position));
                }
            }
        }
    }

    void SpawnPillar()
    {
        if (spawnPillars == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 89;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 tempFloor = floor.GetComponent<MeshRenderer>().bounds.size;
                if (Mathf.Abs(mousePosition.x) < tempFloor.x / 2 && Mathf.Abs(mousePosition.z) < tempFloor.z / 2)
                {
                    GameObject p = Instantiate(pillarPrefab, new Vector3(mousePosition.x, 0.0f, mousePosition.z), Quaternion.identity);
                    for (int i = 0; i < pillars.Count; i++)
                    {
                        pillars[i].GetComponent<Edge_Controller>().edges.Add(new Edge(p, p.transform.position));
                        p.GetComponent<Edge_Controller>().edges.Add(new Edge(pillars[i], pillars[i].transform.position));
                    }
                    pillars.Add(p);
                    shortestDistance = 999999f;
                }
                antAttempts = 0;
                antAmount = pillars.Count;
                GameObject.Find("Number of Attempts").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Number of Attempts = " + antAttempts;
                GameObject.Find("Number of Pillars").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Number of Pillars = " + pillars.Count;
                runNN = true;
            }
        }
    }

    void DestroyPillar()
    {
        if (destroyPillars == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 89;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                for (int i = 0; i < pillars.Count; i++)
                {
                    if (Vector3.Distance(pillars[i].transform.position, mousePosition) < 1.0f)
                    {
                        for (int j = 0; j < pillars.Count; j++)
                        {
                            for (int k = 0; k < pillars[j].GetComponent<Edge_Controller>().edges.Count; k++)
                            {
                                if (pillars[j].GetComponent<Edge_Controller>().edges[k].pillarLocation == pillars[i].transform.position)
                                {
                                    pillars[j].GetComponent<Edge_Controller>().edges.Remove(pillars[j].GetComponent<Edge_Controller>().edges[k]);
                                    k = pillars[j].GetComponent<Edge_Controller>().edges.Count;
                                }
                            }
                        }
                        Destroy(pillars[i].gameObject);
                        pillars.Remove(pillars[i]);
                        i = pillars.Count + 1;
                    }
                }
                antAttempts = 0;
                antAmount = pillars.Count;
                GameObject.Find("Number of Attempts").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Number of Attempts = " + antAttempts;
                GameObject.Find("Number of Pillars").gameObject.GetComponent<UnityEngine.UI.Text>().text = "Number of Pillars = " + pillars.Count;
                runNN = true;
            }
        }
    }

    public void SpawnAnts()
    {
        if (pressButton == true)
        {
            GameObject.Find("Spawn Ants").GetComponentInChildren<UnityEngine.UI.Text>().text = "Cancel Ants";
            int rLocation;
            for (int i = 0; i < antAmount; i++)
            {
                rLocation = Random.Range(0, pillars.Count);
                antInstances.Add(Instantiate(antPrefab, pillars[rLocation].transform.position, Quaternion.identity));
                antInstances[i].GetComponent<AntMovement>().newDestination = pillars[rLocation];
            }
            pressButton = false;
            spawnPillars = false;
            destroyPillars = false;
            runNN = false;
        }
        else //If cancel ants is needed
        {
            for (int i = 0; i < antAmount; i++)
            {
                Destroy(antInstances[0].gameObject);
                antInstances.Remove(antInstances[0]);
            }
            pressButton = true;
            runNN = true;
            GameObject.Find("Spawn Ants").GetComponentInChildren<UnityEngine.UI.Text>().text = "Spawn Ants";
        }
    }

    public void SpawnPillarButton()
    {
        if (pressButton == true)
        {
            spawnPillars = true;
            destroyPillars = false;
        }
    }

    public void DestroyPillarButton()
    {
        if (pressButton == true)
        {
            destroyPillars = true;
            spawnPillars = false;
        }
    }

    public void CalculateNN()
    {
        if (runNN == true)
        {
            float before = Time.realtimeSinceStartup;
            Neighbour[] traversers = new Neighbour[pillars.Count];
            float bestDistance = 99999f;
            for (int i = 0; i < pillars.Count; i++)
            {
                traversers[i] = new Neighbour
                {
                    waypoints = pillars,
                    startPosition = pillars[i],
                    runTime = pillars.Count
                };
                traversers[i].Move();
                if (traversers[i].path < bestDistance)
                {
                    bestDistance = traversers[i].path;
                }
            }
            GameObject.Find("NN Best").GetComponentInChildren<UnityEngine.UI.Text>().text = "NN's Best Path = " + bestDistance;
            runNN = false;
            float after = Time.realtimeSinceStartup;
            float duration = after - before;
            GameObject.Find("Time taken for NN").GetComponentInChildren<UnityEngine.UI.Text>().text = "Duration in seconds: " + duration;
        }
    }
}
