using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private List<Node> nodes;
    private Pathfinding pathfinding;
    public GameObject target;
    private Node previousTargetNode;
    private Node currentTargetNode;
    private Node currentNode;
    private ReferenceManager Ref;

    private int currentPointIndex = 0;
    [HideInInspector] public float speed = 2;

    private void Start()
    {
        Ref = ReferenceManager.Instance;
        pathfinding = Ref.NodeGrid.Pathfinding;
        target = Ref.Target;
        previousTargetNode = pathfinding.FindClosestNode(target.transform.position);
        currentTargetNode = previousTargetNode;
        currentNode = pathfinding.FindClosestNode(transform.position);
    }

    private void Update()
    {
        currentTargetNode = pathfinding.FindClosestNode(target.transform.position);

        // Sprawdzenie, czy obiekt osi¹gn¹³ cel
        if (currentNode == currentTargetNode)
        {
            // Obiekt osi¹gn¹³ cel, nie trzeba dalej przeliczaæ œcie¿ki
            return;
        }

        if (nodes != null && nodes.Count > 0 && currentPointIndex < nodes.Count)
        {
            GoToNextPoint();
        }
        else if (nodes == null || nodes.Count == 0 || (currentTargetNode != previousTargetNode))
        {
            nodes = pathfinding.FindPath(transform.position, target.transform.position);
            previousTargetNode = currentTargetNode;
            currentPointIndex = 0;
        }
    }

    private void GoToNextPoint()
    {
        if (nodes == null || nodes.Count == 0)
        {
            return; // Nie ma dostêpnej œcie¿ki
        }

        Vector3 targetPosition = nodes[currentPointIndex].WorldPosition;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex++;
            currentNode = pathfinding.FindClosestNode(transform.position);

            if (Ref.NodeGrid.GridChanged || currentTargetNode != previousTargetNode)
            {
                nodes = null;
            }
        }
    }
}
