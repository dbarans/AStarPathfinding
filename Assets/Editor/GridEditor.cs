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
        for (int x = 0; x < nodeGrid.GridSizeX; x++)
        {
            for (int z = 0; z < nodeGrid.GridSizeZ; z++)
            {
                Vector3 nodeRadius = new Vector3(nodeGrid.NodeRadius, nodeGrid.NodeRadius, nodeGrid.NodeRadius);
                Handles.DrawWireCube(nodeGrid.Nodes[x, z].WorldPosition, nodeRadius);

            }
        }
    }
}
