using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Edge
{
    public GameObject otherPillar;
    public Vector3 pillarLocation;
    public float edgeScent;
    public Edge(GameObject op, Vector3 pl, float es = 1)
    {
        otherPillar = op;
        pillarLocation = pl;
        edgeScent = es;
    }
}


public class Edge_Controller : MonoBehaviour
{
    //public List<GameObject> otherEdges = new List<GameObject>();
    //public GameObject ant;
    //public bool hasBeenTraversed;
    public LineRenderer tspLines;
    public Renderer ren;
    //public int pointValue;
    //public float scent;

    public List<Edge> edges = new List<Edge>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RenderLine()
    {
        Vector3[] positions = new Vector3[edges.Count];
        for (int i = 0; i < edges.Count; i++)
        {
            positions[i] = edges[i].pillarLocation;
        }
        tspLines.positionCount = (positions.Length) * 2;
        for (int i = 0; i < tspLines.positionCount; i++)
        {
            if (i % 2 == 0)
            {
                tspLines.SetPosition(i, transform.position);
            }
            else
            {
                tspLines.SetPosition(i, positions[(i - 1) / 2]);
            }
        }
    }
}
