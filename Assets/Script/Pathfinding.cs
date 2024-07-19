using JetBrains.Annotations;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    GridNode gridNode;
    public Transform seeker, target;
    public Vector3[] path;
    //int targetIndex = 0;
    //[SerializeField] float speed = 5f;

    private void Awake()
    {
        
    }
    private void Start()
    {
        gridNode = GetComponent<GridNode>();
       
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
        path = ConvertPathToVector3(gridNode.finalPath);
    }
    public void FindPath( Vector3 startPos, Vector3 targetPos)
    {
        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        Node startNode = gridNode.NodeInWorldPosition(startPos);
        Node endNode = gridNode.NodeInWorldPosition(targetPos);
      
        openSet.Add(startNode);
        
        while (openSet.Count >0)
        {
            Node node = openSet[0]; 
            for (int i=1; i< openSet.Count; i++)
            {
                if (node.fCost < openSet[i].fCost || node.fCost == openSet[i].fCost )
                {
                    if (openSet[i].hCost < node.hCost )
                    {
                        node = openSet[i];
                    }
                   
                }
            }

            openSet.Remove(node);
            closeSet.Add(node);

            if (node == endNode )
            {
                ReTrackThePath(startNode, endNode);
                return;
            }

            foreach (Node neighbour in gridNode.GetNeighbourNodes(node))
            {
                if (! neighbour.walkable || closeSet.Contains (neighbour))
                {
                    continue;
                }

                int newMovementCost = node.gCost + GetDistance(node, neighbour);
                if (newMovementCost < neighbour .gCost || !openSet.Contains (neighbour ))
                {
                    neighbour.gCost = newMovementCost;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parent = node; 

                    if (! openSet.Contains (neighbour ))
                    {
                        openSet.Add(neighbour);
                    }
                }
                    
            }

        }
    }

    List<Node> ReTrackThePath(Node seekerNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;
        while (currentNode != seekerNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        gridNode.finalPath = path;
        return path;

    }

    private Vector3[] ConvertPathToVector3(List<Node> pathfound)
    {
       List<Vector3> vectorpath = new List<Vector3>();
        foreach (Node node in pathfound)
        {
           vectorpath.Add(node.worldPos);
       }
        return vectorpath.ToArray();
    }

    //IEnumerator FollowPath()
    //{
    //    Vector3 currentWaypoint = path[0];


    //    for (int i =1; i < path.Length; i++)
    //    {
    //        if (i >= path .Length )
    //        {
    //            yield break; 
    //        }

    //        currentWaypoint = path[i];

    //        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

    //        yield return new WaitForSeconds(1f); 
    //    }
        //while (true)
        //{
        //    if (transform.position == currentWaypoint)
        //    {
        //        targetIndex++;
        //        if (targetIndex > path.Length)
        //        {
        //            yield break;
        //        }
        //        currentWaypoint = path[targetIndex];
        //    }

        //    transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
        //    yield return null;

        //}
    //}

    public int GetDistance (Node nodeA, Node nodeB)
    {
        int disX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int disY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (disX> disY)
        {
            return 14* disY + 10 * (disX - disY); 
        }
        else 

        return 14*disX + 10 * (disY - disX); 
    }

  
}
