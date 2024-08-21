using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivityX = 10f;
    [SerializeField] private float sensitivityY = 10f;
    [SerializeField] private float minYAngle = -80f;
    [SerializeField] private float maxYAngle = 80f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX += mouseX * sensitivityX;
        rotationY -= mouseY * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minYAngle, maxYAngle);

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
    }
}
