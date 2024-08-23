using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using System.Diagnostics;

public class BallMovement : MonoBehaviour
{
    private List<Node> nodes;
    private Pathfinding pathfinding;
    public Vector3 target;
    private ReferenceManager Ref;
    
    private int currentPointIndex = 0;
    public float speed = 2;

    private void Start()
    {
        Ref = ReferenceManager.Instance;
        pathfinding = Ref.Pathfinding;
        target = Ref.Target.position;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        nodes = pathfinding.FindPath(transform.position, target);
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
            if (Ref.NodeGrid.GridChanged)
            {
                nodes = pathfinding.FindPath(transform.position, target);
                currentPointIndex = 0;
            }
        }
    }
}