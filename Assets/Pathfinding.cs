using JetBrains.Annotations;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    public GridNode grid;
    public Transform seeker, target;
   


    private void Awake()
    {
        grid = GetComponent<GridNode>(); 
    }

    private void Update()
    {
        FindPath(seeker.position , target.position );
    }
    void FindPath(Vector3 startPoint, Vector3 endPoint)
    {
        Node startNode = grid.NodeInWorldPosition(startPoint);
        Node endNode = grid.NodeInWorldPosition(endPoint);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();

        openSet.Add(startNode);
        

        while (openSet.Count >0)
        {
            Node currentNode = openSet[0]; 
            for (int i=1; i< openSet .Count; i++)
            {
                if (currentNode.fCost < openSet[i].fCost || currentNode.fCost == openSet[i].fCost )
                {
                    if (openSet[i].hCost < currentNode.hCost )
                    {
                        currentNode = openSet[i];
                    }
                   
                }
            }

            openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            if (currentNode == endNode )
            {
                ReTrackThePath(startNode, endNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbourNodes(currentNode))
            {
                if (! neighbour.walkable || closeSet.Contains (neighbour))
                {
                    continue;
                }

                int newMovementCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCost < neighbour .gCost || !openSet.Contains (neighbour ))
                {
                    neighbour.gCost = newMovementCost;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parent = currentNode; 

                    if (! openSet.Contains (neighbour ))
                    {
                        openSet.Add(neighbour);
                    }
                }
                    
            }

        }
    }

    void ReTrackThePath(Node startNode, Node endNode )
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode; 
        while (currentNode != startNode )
        {
            path.Add(currentNode); 
            currentNode = currentNode.parent;
        }

        path.Reverse();
        grid.finalPath = path;

    }

    public int GetDistance (Node nodeA, Node nodeB)
    {
        int disX = Mathf.Abs(nodeA.gridX = nodeB.gridX);
        int disY = Mathf.Abs(nodeA.gridY = nodeB.gridY);

        if (disX> disY)
        {
            return disY * 14 + 10 * (disX - disY); 
        }
        else 

        return disX * 14 + 10 * (disY - disX); 
    }

  
}
