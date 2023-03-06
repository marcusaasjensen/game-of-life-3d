using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    [SerializeField] bool isAlive = false;
    [SerializeField] Material deadCellMaterial;
    [SerializeField] Material aliveCellMaterial;

    MeshRenderer _currentMesh;
    Vector3Int _position;
    public bool IsAlive { get { return isAlive; } }
    public Vector3Int Position { get { return _position; } }

    void Awake()
    {
        _position.Set((int) transform.position.x, (int) transform.position.y, (int) transform.position.z);

        if(!_currentMesh)
            _currentMesh = GetComponent<MeshRenderer>();

        Die();
    }

    public void Die()
    {
        isAlive = false;
        if (!deadCellMaterial) return;
        _currentMesh.material = deadCellMaterial;
    }

    public void Live()
    {
        isAlive = true;
        if (!aliveCellMaterial) return;
        _currentMesh.material = aliveCellMaterial;
    }

}
