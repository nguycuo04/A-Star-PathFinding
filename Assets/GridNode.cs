using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.ComponentModel;

public class GridNode : MonoBehaviour
{
    public Transform player;
    public LayerMask unmovableMask;
    public Vector2 gridworldSize;
    public float nodeRadius;
    Node[,] grid;
    float nodeDiameter;
    int gridsizeX, gridsizeY;
    public List<Node> finalPath = new List<Node>();
    // public bool onlyDisplayPathGizmos; 

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridsizeX = Mathf.RoundToInt(gridworldSize.x / nodeDiameter);
        gridsizeY = Mathf.RoundToInt(gridworldSize.y / nodeDiameter);
        CreateTheGrid();
    }

    public void CreateTheGrid()
    {
        grid = new Node[gridsizeX, gridsizeY];
        Vector3 bottomLeftPos = transform.position - Vector3.right * gridworldSize.x / 2 - Vector3.forward * gridworldSize.y / 2;
        for ( int x =0; x< gridsizeX; x++)
        {
            for ( int y =0; y< gridsizeY; y++)
            {
                Vector3 worldPoint = bottomLeftPos + Vector3.right * (nodeDiameter * x + nodeRadius) + Vector3.forward * (nodeDiameter * y + nodeRadius);
                bool unmovable = !Physics.CheckSphere(worldPoint, nodeRadius,unmovableMask);
                grid[x, y] = new Node(unmovable, worldPoint,x,y);
                grid[x, y].worldPos = worldPoint;
                grid[x, y].walkable = unmovable;
            }
         
        }

    }

    public List<Node> GetNeighbourNodes(Node node)

    {
        List<Node> neighbours = new List<Node>();
        for (int x =-1; x<=1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y; 
                if ( checkX >=0 && checkX <gridsizeX && checkY >=0 && checkY <gridsizeY )
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;

    }

    public Node NodeInWorldPosition(Vector3 worldCoordinate)
    {
        float percentX = (worldCoordinate.x + gridworldSize.x/2) / gridworldSize.x; 
        float percentY = (worldCoordinate.z + gridworldSize.y/2) / gridworldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridsizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridsizeY - 1) * percentY);
        return grid[x, y];

    }

 
    void OnDrawGizmos()
    {
      
        Gizmos.DrawWireCube(transform.position, new Vector3(gridworldSize.x, 1, gridworldSize.y));

        if (grid != null )
        {
            Node playerNode = NodeInWorldPosition(player.position);
           
            foreach (Node n in grid)
            {
                
                Gizmos.color = Color.yellow;

              
                    if (finalPath != null && finalPath.Contains(n))
                    {
                        Gizmos.color = Color.black;
                        Gizmos.DrawWireCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
                    }

                if (playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }

                if (n.walkable == false)
                {
                    Gizmos.color = Color.gray;
                }
                Gizmos.DrawWireCube(n.worldPos, Vector3.one * (nodeDiameter - .1f));
            }
           

        }
     
    }
}
