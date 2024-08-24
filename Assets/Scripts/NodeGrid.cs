using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    [SerializeField] private Node[,] nodes;
    [SerializeField] private int gridSizeX = 10;
    [SerializeField] private int gridSizeZ = 10;
    [SerializeField] private float nodeRadius = 1;
    [SerializeField] private LayerMask obstacleLayerMask;
    private Pathfinding pathfinding;

    private bool gridChanged = false;
    private int[,] nodeHashes;
    private GUIStyle style;

    public Node[,] Nodes
    {
        get { return nodes; }
        private set { nodes = value; }
    }

    public int GridSizeX
    {
        get { return gridSizeX; }
        private set { gridSizeX = value; }
    }

    public int GridSizeZ
    {
        get { return gridSizeZ; }
        private set { gridSizeZ = value; }
    }

    public float NodeRadius
    {
        get { return nodeRadius; }
        private set { nodeRadius = value; }
    }

    public LayerMask ObstacleLayerMask
    {
        get { return obstacleLayerMask; }
        private set { obstacleLayerMask = value; }
    }

    public bool GridChanged
    {
        get { return gridChanged; }
        private set { gridChanged = value; }
    }

    public Pathfinding Pathfinding
    {
        get { return pathfinding; }
        private set { pathfinding = value; }
    }

    private void Start()
    {
        Pathfinding =  new Pathfinding(this);
        InitializeNodeHashes();
        style = new GUIStyle();
    }

    private void InitializeNodeHashes()
    {
        nodeHashes = new int[gridSizeX, gridSizeZ];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                nodeHashes[x, z] = ComputeNodeHash(nodes[x, z]);
            }
        }
    }

    private int ComputeNodeHash(Node node)
    {
        return node.IsObstacle ? 1 : 0;
    }

    private void Update()
    {
        CheckObstacles();
    }

    public void GenerateNodeGrid()
    {
        nodes = new Node[gridSizeX, gridSizeZ];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                float zPos = transform.position.z + nodeRadius * z;
                float xPos = transform.position.x + nodeRadius * x;
                Vector3 worldPoint = new Vector3(xPos, transform.position.y, zPos);
                nodes[x, z] = new Node(worldPoint, x, z);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (nodes != null)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    style.normal.textColor = nodes[x, z].IsObstacle ? Color.red : Color.green;
                    Handles.DrawWireDisc(nodes[x, z].WorldPosition, Vector3.up, nodeRadius / 2);
                    Handles.Label(nodes[x, z].WorldPosition + Vector3.up * 0.5f, nodes[x, z].CostF.ToString(), style);
                }
            }
        }
    }

    private void CheckObstacles()
    {
        if (nodes == null) return;

        gridChanged = false;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                bool isObstacleNow = Physics.CheckSphere(nodes[x, z].WorldPosition, nodeRadius * 0.5f, obstacleLayerMask);

                if (isObstacleNow != nodes[x, z].IsObstacle)
                {
                    nodes[x, z].IsObstacle = isObstacleNow;
                    int newHash = ComputeNodeHash(nodes[x, z]);

                    if (newHash != nodeHashes[x, z])
                    {
                        nodeHashes[x, z] = newHash;
                        gridChanged = true;
                    }
                }
            }
        }
    }

    public List<Node> GetNeighbors(int x, int z)
    {
        List<Node> neighbors = new List<Node>();
        int[] dx = { -1, 0, 1, 0, -1, 1, 1, -1 };
        int[] dz = { 0, -1, 0, 1, -1, -1, 1, 1 };

        for (int i = 0; i < dx.Length; i++)
        {
            int neighborX = x + dx[i];
            int neighborZ = z + dz[i];
            if (neighborX >= 0 && neighborX < gridSizeX && neighborZ >= 0 && neighborZ < gridSizeZ)
            {
                Node neighborNode = nodes[neighborX, neighborZ];
                if (!neighborNode.IsObstacle)
                {
                    neighbors.Add(nodes[neighborX, neighborZ]);
                }
            }
        }
        return neighbors;
    }

    public void ClearGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Node node = nodes[x, z];
                node.CostF = 0;
                node.CostG = 0;
                node.CostH = 0;
                node.Parent = null;
            }
        }
    }
}
