using UnityEngine;

public class CellAuthoring : MonoBehaviour
{
    [SerializeField] private bool isAlive;

    [SerializeField] private Material deadCellMaterial;
    [SerializeField] private Material aliveCellMaterial;

    private MeshRenderer _currentMesh;
    public bool IsAlive => isAlive;
    public Vector3Int Position { get; private set; }
    
    private void Awake()
    {
        Position = Vector3Int.FloorToInt(transform.position);
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