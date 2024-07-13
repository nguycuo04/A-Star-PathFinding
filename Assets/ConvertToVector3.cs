using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ConvertToVector3 : MonoBehaviour
{
    private Pathfinding pathFinding; 
    [SerializeField] float speed = 2f;
    int targetIndex = 0;

    private void Awake()
    {
        
    }
    void Start()
    {
        pathFinding = GameObject.Find("A*").GetComponent<Pathfinding>();
        StartCoroutine(FollowPath());
    }

    void Update()
    {
       
    }

    //private Vector3[] ConvertPathToVector3(List<Node> pathFound)
    //{
    //    List<Vector3> vectorPath = new List<Vector3>();
    //    foreach (Node node in pathFound)
    //    {
    //        vectorPath.Add(node.worldPos);
    //    }
    //    return vectorPath.ToArray();
    //}


    //IEnumerator FollowPath()
    //{
    //    foreach (Vector3 waypoint in ConvertPathToVector3(gridNode.finalPath))
    //    {
    //        yield return StartCoroutine(MoveToPosition(waypoint));
    //    }
    //}

    //IEnumerator MoveToPosition(Vector3 waypoint)
    //{
    //    while (Vector3.Distance(transform.position, waypoint) > 0.1f)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
    //        yield return null;
    //    }
    //    transform.position = waypoint;
    //}

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = pathFinding.path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= pathFinding.path.Length)
                {
                    yield break;
                }
                currentWaypoint = pathFinding.path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }
}
