using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using System.Diagnostics;

public class BallMovement : MonoBehaviour
{
    private List<Node> nodes;
    private Pathfinding pathfinding;
    public GameObject target;
    private Vector3 previousTargetPosition;
    private ReferenceManager Ref;
    
    private int currentPointIndex = 0;
    [HideInInspector] public float speed = 2;

    private void Start()
    {
        Ref = ReferenceManager.Instance;
        pathfinding = Ref.NodeGrid.Pathfinding;
        target = Ref.Target;
        previousTargetPosition = target.transform.position;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        nodes = pathfinding.FindPath(transform.position, target.transform.position);
        stopwatch.Stop();
        UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds);

    }
    private void Update()
    {
        if (nodes != null && currentPointIndex < nodes.Count)
        {
            GoToNextPoint();
        }
    }

    private void GoToNextPoint()
    {
        Vector3 targetPosition = nodes[currentPointIndex].WorldPosition;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex++;
            if (Ref.NodeGrid.GridChanged || pathfinding.FindClosestNode(target.transform.position) != pathfinding.FindClosestNode(previousTargetPosition))
            {
                previousTargetPosition = target.transform.position;
                nodes = pathfinding.FindPath(transform.position, target.transform.position);
                currentPointIndex = 0;
            }
        }
    }
}