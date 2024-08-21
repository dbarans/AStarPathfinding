using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 worldPosition;
    public bool isObstacle;
    public bool isStartOrEnd;
    public int costG;
    public int costH;
    public int costF; 

    public Node (Vector3 worldPosition)
    {
        this.worldPosition = worldPosition;
        isObstacle = false;
        isStartOrEnd = false;
    }
}
