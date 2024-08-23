using UnityEngine;

public class Node
{
    private int costG;
    private int costH;
    private int costF;
    private Node parent;
    private bool isObstacle;

    public Vector3 WorldPosition { get; private set; }
    public int GridX { get; private set; }
    public int GridZ { get; private set; }

    public int CostG
    {
        get { return costG; }
        set { costG = value; }
    }

    public int CostH
    {
        get { return costH; }
        set { costH = value; }
    }

    public int CostF
    {
        get { return costF; }
        set { costF = value; }
    }

    public Node Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public bool IsObstacle
    {
        get { return isObstacle; }
        set { isObstacle = value; }
    }

    public Node(Vector3 worldPosition, int gridX, int gridZ)
    {
        WorldPosition = worldPosition;
        GridX = gridX;
        GridZ = gridZ;
        isObstacle = false;
    }
}
