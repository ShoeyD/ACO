using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge_Controller : MonoBehaviour
{
    public List<GameObject> otherEdges = new List<GameObject>();
    //public GameObject ant;
    //public bool hasBeenTraversed;
    public LineRenderer tspLines;
    public Renderer ren;
    public int pointValue;

	// Use this for initialization
	void Start ()
    {
        RenderLine();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("MY POSITION = " + transform.position);
        //Debug.Log("ANT'S POSITION = " + ant.transform.position);
        //if (ant.transform.position == transform.position)
        //{
            //Debug.Log("HELL YEAH");
            //hasBeenTraversed = true;
        //}
        //RenderLine();
	}

    public void RenderLine()
    {
        Vector3[] positions = new Vector3[otherEdges.Count];
        for (int i = 0; i < otherEdges.Count; i++)
        {
            positions[i] = otherEdges[i].transform.position;
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
        //tspLines.SetPositions(positions);
        //Debug.Log(tspLines.positionCount);
        //tspLines.SetPosition(0, transform.position);
        //tspLines.SetPosition(1, positions[0]);
        //tspLines.SetPosition(2, transform.position);
        //tspLines.SetPosition(3, positions[1]);
        //tspLines.SetPosition(4, transform.position);
        //tspLines.SetPosition(5, positions[2]);

        // Connect the start and end positions of the line together to form a continuous loop.
        //tspLines.loop = true;
        //List<Vector3> linePoints = new List<Vector3>();
        //if (travelledPath.Count > 2)
        //{
        //    for (int i = 0; i < travelledPath.Count - 1; i++)
        //    {
        //        linePoints.Add(travelledPath[i]);
        //    }
        //    linePoints.Add(transform.position);
        //}
        //else
        //{
        //    linePoints.Add(travelledPath[0]);
        //    linePoints.Add(transform.position);
        //}
        //lr.positionCount = linePoints.Count;
        //lr.SetPositions(linePoints.ToArray());
    }
}
