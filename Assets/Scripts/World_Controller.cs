using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Controller : MonoBehaviour
{
    public GameObject pillarPrefab;
    public GameObject antPrefab;
    public List<GameObject> antInstances;
    public GameObject floor;

    public float gayAssConstantCalledQ = 1.0f;

    public int antAmount;

    //HouseWithGarage hwg;
    Basic_TSP bTSP;

    private List<GameObject> pillars = new List<GameObject>();
    //List<int[]> pillarEdges = new List<int[]>();

    // Use this for initialization
    void Start()
    {
        bTSP = new Basic_TSP(floor.GetComponent<MeshRenderer>().bounds.size);
        SpawnPillarsAndAnts();
        //CreateHouseWithGarage();
        //antPrefab.GetComponent<AntMovement>().waypoints = pillars;
        //for (int i = 0; i < antAmount; i++)
        //{
        //    //GameObject antInstance = Instantiate(antPrefab, pillars[0].transform.position, Quaternion.identity);
        //    //antInstances.Add(antInstance);
        //    antInstances.Add(Instantiate(antPrefab, pillars[0].transform.position, Quaternion.identity));

        //}
        //antInstance = Instantiate(antPrefab, pillars[0].transform.position, Quaternion.identity);

    }


    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < antInstances.Count; i++)
        //{
        //    if (antInstances[i].GetComponent<AntMovement>().noMoreMoves == true)
        //    {
        //        Destroy(antInstances[i]);
        //        antInstances.Remove(antInstances[i]);
        //    }
        //}
        //if (antInstances.Count == 0)
        //{
        //    for (int i = 0; i < antAmount; i++)
        //    {
        //        //GameObject antInstance = Instantiate(antPrefab, pillars[0].transform.position, Quaternion.identity);
        //        //antInstances.Add(antInstance);
        //        antInstances.Add(Instantiate(antPrefab, pillars[0].transform.position, Quaternion.identity));

        //    }
        //}

    }

    //void CreateHouseWithGarage()
    //{
    //    Vector3 floorSize = floor.GetComponent<MeshRenderer>().bounds.size;
    //    Vector3[] bTSPPositions = new Vector3[8];
    //    bTSPPositions[0] = new Vector3(-floorSize.x * 0.3f, 0.0f, -floorSize.z * 0.3f);
    //    bTSPPositions[1] = new Vector3(0.0f, 0.0f, -floorSize.z * 0.3f);
    //    bTSPPositions[2] = new Vector3(floorSize.x * 0.3f, 0.0f, -floorSize.z * 0.3f);
    //    bTSPPositions[3] = new Vector3(-floorSize.x * 0.15f, 0.0f, -floorSize.z * 0.15f);
    //    bTSPPositions[4] = new Vector3(-floorSize.x * 0.3f, 0.0f, 0.0f);
    //    bTSPPositions[5] = new Vector3(0.0f, 0.0f, 0.0f);
    //    bTSPPositions[6] = new Vector3(floorSize.x * 0.3f, 0.0f, 0.0f);
    //    bTSPPositions[7] = new Vector3(-floorSize.x * 0.15f, 0.0f, floorSize.z * 0.15f);
    //    pillarEdges.Add(new[] { 1, 3, 4 });
    //    pillarEdges.Add(new[] { 0, 2, 3, 5 });
    //    pillarEdges.Add(new[] { 1, 5, 6 });
    //    pillarEdges.Add(new[] { 0, 1, 4, 5 });
    //    pillarEdges.Add(new[] { 0, 3, 5, 7 });
    //    pillarEdges.Add(new[] { 1, 2, 3, 4, 6, 7 });
    //    pillarEdges.Add(new[] { 2, 5 });
    //    pillarEdges.Add(new[] { 4, 5 });
    //    for (int i = 0; i < bTSPPositions.Length; i++)
    //    {
    //        GameObject go = Instantiate(pillarPrefab, bTSPPositions[i], Quaternion.identity);
    //        pillars.Add(go);
    //    }
    //    for (int i = 0; i < pillars.Count; i++)
    //    {
    //        for (int j = 0; j < pillarEdges[i].Length; j++)
    //        {
    //            pillars[i].GetComponent<Edge_Controller>().otherEdges.Add(pillars[pillarEdges[i][j]]);
    //        }
    //        pillars[i].GetComponent<Edge_Controller>().RenderLine();
    //    }
    //}

    void CreateHouseWithGarage()
    {
        for (int i = 0; i < bTSP.pPositions.Length; i++)
        {
            GameObject go = Instantiate(pillarPrefab, bTSP.pPositions[i], Quaternion.identity);
            pillars.Add(go);
        }
        for (int i = 0; i < pillars.Count; i++)
        {
            //for (int j = 0; j < bTSP.pEdges[i].Length; j++)
            //{
            //    pillars[i].GetComponent<Edge_Controller>().otherEdges.Add(pillars[bTSP.pEdges[i][j]]);
            //}
            pillars[i].GetComponent<Edge_Controller>().RenderLine();
            pillars[i].GetComponent<Edge_Controller>().pointValue = i;
            //pillars[i].GetComponent<Edge_Controller>().ant = antInstance;
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
                    //pillars[i].GetComponent<Edge_Controller>().otherEdges.Add(pillars[j]);
                    pillars[i].GetComponent<Edge_Controller>().edges.Add(new Edge(pillars[i], pillars[i].transform.position));

                }
            }
        }
        for (int i = 0; i < antAmount; i++)
        {
            rLocation = Random.Range(0, pillars.Count);
            antInstances.Add(Instantiate(antPrefab, pillars[rLocation].transform.position, Quaternion.identity));

        }
    }
}
