using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public Node[,] nodes;
    public int gridSizeX = 10, gridSizeZ = 10; 
    public float nodeRadius = 1;



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
                nodes[x, z] = new Node(worldPoint);
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

                    if (nodes[x, z].isObstacle)
                    {
                        Gizmos.color = Color.red;
                    }
                    else if (nodes[x,z].isStartOrEnd)
                    {
                        Gizmos.color = Color.black;
                    }
                    else
                    {
                        Gizmos.color = Color.white;
                    }
                    Gizmos.DrawWireSphere(nodes[x, z].worldPosition, nodeRadius/2);
                }
            }
        }
    }
    private void CheckObstacles()
    {
        if (nodes == null) return;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                if (Physics.CheckSphere(nodes[x, z].worldPosition, nodeRadius))
                {
                    nodes[x, z].isObstacle = true;
                }
                else
                {
                    nodes[x, z].isObstacle = false;
                }
            }
        }
    }

}
