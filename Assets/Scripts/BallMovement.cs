using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Vector3[] points = new Vector3[2];
    private Pathfinding pathfinding;
    public Vector3 target;
    
    private int currentPointIndex = 0;
    private float speed = 2;

    private void Start()
    {
        pathfinding = ReferenceManager.Instance.Pathfinding;
        target = ReferenceManager.Instance.Target.position;
        pathfinding.FindPath(transform.position, target);

    }
    private void Update()
    {
        if (currentPointIndex < points.Length)
        {
            GoToNextPoint();
        }
    }

    private void GoToNextPoint()
    {
        Vector3 targetPosition = points[currentPointIndex];
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex++;
        }
    }
}