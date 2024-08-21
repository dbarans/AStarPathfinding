using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallMovement>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
