using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Vector3[] points = new Vector3[2];
    [SerializeField] private float speed = 2f;

    private int currentPointIndex = 0;

    private void Update()
    {
        if (currentPointIndex < points.Length)
        {
            MoveTowardsNextPoint();
        }
    }

    private void MoveTowardsNextPoint()
    {
        Vector3 targetPosition = points[currentPointIndex];
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            currentPointIndex++;

        }
    }
}
