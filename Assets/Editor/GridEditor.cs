using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NodeGrid))]
public class PathfindingEditor : Editor
{
    private void OnSceneGUI()
    {
        bool isPlaying = EditorApplication.isPlaying;
        if (isPlaying) return;
        NodeGrid nodeGrid = (NodeGrid)target;
        

        nodeGrid.GenerateNodeGrid();

        Handles.color = Color.white;
        for (int x = 0; x < nodeGrid.gridSizeX; x++)
        {
            for (int z = 0; z < nodeGrid.gridSizeZ; z++)
            {
                Vector3 nodeRadius = new Vector3(nodeGrid.nodeRadius, nodeGrid.nodeRadius, nodeGrid.nodeRadius);
                Handles.DrawWireCube(nodeGrid.nodes[x, z].worldPosition, nodeRadius);

            }
        }
    }
}
