using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Controller : MonoBehaviour
{
    public GameObject pillarPrefab;
    public GameObject antPrefab;
    public List<GameObject> antInstances;
    public GameObject floor;

    public int antAmount;

    HouseWithGarage hwg; 

    private List<GameObject> pillars = new List<GameObject>();
    //List<int[]> pillarEdges = new List<int[]>();

    // Use this for initialization
    void Start()
    {
        hwg = new HouseWithGarage(floor.GetComponent<MeshRenderer>().bounds.size);
        CreateHouseWithGarage();
        antPrefab.GetComponent<AntMovement>().waypoints = pillars;
        for (int i = 0; i < antAmount; i++)
        {
            //GameObject antInstance = Instantiate(antPrefab, pillars[0].transform.position, Quaternion.identity);
            //antInstances.Add(antInstance);
            antInstances.Add(Instantiate(antPrefab, pillars[0].transform.position, Quaternion.identity));

        }
        //antInstance = Instantiate(antPrefab, pillars[0].transform.position, Quaternion.identity);

    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < antInstances.Count; i++)
        {
            if (antInstances[i].GetComponent<AntMovement>().noMoreMoves == true)
            {
                Destroy(antInstances[i]);
                antInstances.Remove(antInstances[i]);
            }
        }
        if (antInstances.Count == 0)
        {
            for (int i = 0; i < antAmount; i++)
            {
                //GameObject antInstance = Instantiate(antPrefab, pillars[0].transform.position, Quaternion.identity);
                //antInstances.Add(antInstance);
                antInstances.Add(Instantiate(antPrefab, pillars[0].transform.position, Quaternion.identity));

            }
        }

    }

    //void CreateHouseWithGarage()
    //{
    //    Vector3 floorSize = floor.GetComponent<MeshRenderer>().bounds.size;
    //    Vector3[] hwgPositions = new Vector3[8];
    //    hwgPositions[0] = new Vector3(-floorSize.x * 0.3f, 0.0f, -floorSize.z * 0.3f);
    //    hwgPositions[1] = new Vector3(0.0f, 0.0f, -floorSize.z * 0.3f);
    //    hwgPositions[2] = new Vector3(floorSize.x * 0.3f, 0.0f, -floorSize.z * 0.3f);
    //    hwgPositions[3] = new Vector3(-floorSize.x * 0.15f, 0.0f, -floorSize.z * 0.15f);
    //    hwgPositions[4] = new Vector3(-floorSize.x * 0.3f, 0.0f, 0.0f);
    //    hwgPositions[5] = new Vector3(0.0f, 0.0f, 0.0f);
    //    hwgPositions[6] = new Vector3(floorSize.x * 0.3f, 0.0f, 0.0f);
    //    hwgPositions[7] = new Vector3(-floorSize.x * 0.15f, 0.0f, floorSize.z * 0.15f);
    //    pillarEdges.Add(new[] { 1, 3, 4 });
    //    pillarEdges.Add(new[] { 0, 2, 3, 5 });
    //    pillarEdges.Add(new[] { 1, 5, 6 });
    //    pillarEdges.Add(new[] { 0, 1, 4, 5 });
    //    pillarEdges.Add(new[] { 0, 3, 5, 7 });
    //    pillarEdges.Add(new[] { 1, 2, 3, 4, 6, 7 });
    //    pillarEdges.Add(new[] { 2, 5 });
    //    pillarEdges.Add(new[] { 4, 5 });
    //    for (int i = 0; i < hwgPositions.Length; i++)
    //    {
    //        GameObject go = Instantiate(pillarPrefab, hwgPositions[i], Quaternion.identity);
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
        for (int i = 0; i < hwg.pPositions.Length; i++)
        {
            GameObject go = Instantiate(pillarPrefab, hwg.pPositions[i], Quaternion.identity);
            pillars.Add(go);
        }
        for (int i = 0; i < pillars.Count; i++)
        {
            for (int j = 0; j < hwg.pEdges[i].Length; j++)
            {
                pillars[i].GetComponent<Edge_Controller>().otherEdges.Add(pillars[hwg.pEdges[i][j]]);
            }
            pillars[i].GetComponent<Edge_Controller>().RenderLine();
            pillars[i].GetComponent<Edge_Controller>().pointValue = i;
            //pillars[i].GetComponent<Edge_Controller>().ant = antInstance;
        }
    }
}
