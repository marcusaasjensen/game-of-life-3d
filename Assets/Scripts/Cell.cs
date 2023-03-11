using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] bool isAlive = false;
    [SerializeField] Material deadCellMaterial = default;
    [SerializeField] Material aliveCellMaterial = default;

    MeshRenderer _currentMesh;
    Vector3Int _position;
    public bool IsAlive { get { return isAlive; } }
    public Vector3Int Position { get { return _position; } }

    void Awake()
    {
        _position = Vector3Int.FloorToInt(transform.position);
        _currentMesh = GetComponent<MeshRenderer>();
        Die();
    }

    public void Die()
    {
        isAlive = false;
        _currentMesh.material = deadCellMaterial;
    }

    public void Live()
    {
        isAlive = true;
        _currentMesh.material = aliveCellMaterial;
    }

}
