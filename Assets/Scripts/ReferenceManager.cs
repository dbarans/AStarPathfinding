using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { get; private set; }


    [SerializeField] private CameraController playerCamera;
    [SerializeField] private NodeGrid nodeGrid;
    [SerializeField] private Pathfinding pathfinding;
    [SerializeField] private GameObject target;

    public CameraController PlayerCamera => playerCamera;
    public NodeGrid NodeGrid => nodeGrid;
    public Pathfinding Pathfinding => pathfinding;
    public GameObject Target => target;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
