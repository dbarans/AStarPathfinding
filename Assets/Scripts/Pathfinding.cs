using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private NodeGrid nodeGrid;
    
    private void Start()
    {
        nodeGrid.GenerateNodeGrid();
    }

    public Node[] FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = FindClosestNode(startPos);
        Node targetNode = FindClosestNode(targetPos);
        if (startNode != null) { startNode.isStartOrEnd = true; }
        if (targetNode != null) { targetNode.isStartOrEnd = true; }



        return null;
    }

    private Node FindClosestNode(Vector3 pos)
    {
        Vector3 firstNodePos = nodeGrid.nodes[0, 0].worldPosition;
        Vector3 lastNodePos = nodeGrid.nodes[nodeGrid.gridSizeX - 1, nodeGrid.gridSizeZ - 1].worldPosition;
        float percentX = (pos.x - firstNodePos.x) / (lastNodePos.x - firstNodePos.x);
        float percentZ = (pos.z - firstNodePos.z) / (lastNodePos.z - firstNodePos.z);
        percentX = Mathf.Clamp01(percentX);
        percentZ = Mathf.Clamp01(percentZ);
        int x = Mathf.RoundToInt((nodeGrid.gridSizeX - 1) * percentX);
        int z = Mathf.RoundToInt((nodeGrid.gridSizeZ - 1) * percentZ);

        return nodeGrid.nodes[x, z];
    }
}
