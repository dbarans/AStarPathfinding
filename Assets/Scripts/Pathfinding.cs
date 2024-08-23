using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private NodeGrid nodeGrid;
    private readonly int strightCost = 10;
    private readonly int diagonalCost = 14;


    private void Start()
    {
        nodeGrid.GenerateNodeGrid();
    }

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        nodeGrid.ClearGrid();

        Node startNode = FindClosestNode(startPos);
        Node targetNode = FindClosestNode(targetPos);

        var openNodes = new List<Node> { startNode };
        var closedNodes = new HashSet<Node>();

        while (openNodes.Count > 0)
        {
            Node currentNode = FindCheapestNode(openNodes);
            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            if (currentNode == targetNode)
                return ReconstructPath(startNode, targetNode);

            foreach (Node neighbor in nodeGrid.GetNeighbors(currentNode.GridX, currentNode.GridZ))
            {
                if (closedNodes.Contains(neighbor))
                    continue;

                int tentativeGCost = currentNode.CostG + GetDistance(currentNode, neighbor);

                if (!openNodes.Contains(neighbor) || tentativeGCost < neighbor.CostG)
                {
                    neighbor.Parent = currentNode;
                    CalculateNodeCosts(neighbor, targetNode, tentativeGCost);

                    if (!openNodes.Contains(neighbor))
                        openNodes.Add(neighbor);
                }
            }
        }

        return new List<Node>();
    }

    private Node FindClosestNode(Vector3 pos)
    {
        float percentX = Mathf.Clamp01((pos.x - nodeGrid.Nodes[0, 0].WorldPosition.x) /
                                        (nodeGrid.Nodes[nodeGrid.GridSizeX - 1, nodeGrid.GridSizeZ - 1].WorldPosition.x - nodeGrid.Nodes[0, 0].WorldPosition.x));

        float percentZ = Mathf.Clamp01((pos.z - nodeGrid.Nodes[0, 0].WorldPosition.z) /
                                        (nodeGrid.Nodes[nodeGrid.GridSizeX - 1, nodeGrid.GridSizeZ - 1].WorldPosition.z - nodeGrid.Nodes[0, 0].WorldPosition.z));

        int x = Mathf.RoundToInt((nodeGrid.GridSizeX - 1) * percentX);
        int z = Mathf.RoundToInt((nodeGrid.GridSizeZ - 1) * percentZ);

        return nodeGrid.Nodes[x, z];
    }

    private Node FindCheapestNode(List<Node> openNodes)
    {
        return openNodes.OrderBy(node => node.CostF).ThenBy(node => node.CostH).First();
    }

    private void CalculateNodeCosts(Node node, Node target, int newCostG)
    {
        node.CostG = newCostG;
        node.CostH = HeuristicCost(node, target);
        node.CostF = node.CostG + node.CostH;
    }

    private int HeuristicCost(Node currentNode, Node targetNode)
    {
        int dx = Mathf.Abs(currentNode.GridX - targetNode.GridX);
        int dz = Mathf.Abs(currentNode.GridZ - targetNode.GridZ);
        return (dx + dz) * strightCost + (Mathf.Min(dx, dz) * (diagonalCost - strightCost));
    }

    private int GetDistance(Node a, Node b)
    {
        int dx = Mathf.Abs(a.GridX - b.GridX);
        int dz = Mathf.Abs(a.GridZ - b.GridZ);
        return dx + dz == 2 ? diagonalCost : strightCost;
    }

    private List<Node> ReconstructPath(Node startNode, Node endNode)
    {
        var path = new List<Node>();
        for (Node currentNode = endNode; currentNode != startNode; currentNode = currentNode.Parent)
        {
            path.Add(currentNode);
        }
        path.Add(startNode);
        path.Reverse();
        return path;
    }
}
