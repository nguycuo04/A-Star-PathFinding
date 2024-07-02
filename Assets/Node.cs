using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool walkable;
    public Vector3 worldPos;
    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;
    public Node parent;

    public Node ( bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        _walkable = walkable;
        _worldPos = worldPos;
        _gridX = gridX;
        _gridY = gridY; 
    }

    public int fCost 
    {
        get
        {
            return gCost + hCost; 
        }
    }
}
