using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] GridCellManager cellManager;
    [SerializeField] float step = 1;
    [SerializeField] float projectionSizeOffset = 3;

    Vector3 _initialPosition;
    Camera _cam;
    Transform _transform;

    void Start()
    {
        _initialPosition = transform.position;
        _cam = Camera.main;
        _transform = transform;
    }
    void Update()
    {
        SetLocalPositionWithOffset();
        SetSizeProjection();
    }

    void SetLocalPositionWithOffset()
    {
        _transform.position = new(
        _initialPosition.x + step * (GridCellManager.s_gridSize.x - 1) / 2,
        _initialPosition.y + step * (GridCellManager.s_gridSize.y - 1) / 2,
        _initialPosition.z + step * (GridCellManager.s_gridSize.z - 1) / 2
        );
    }

    void SetSizeProjection() => _cam.orthographicSize = Vector3.Magnitude(GridCellManager.s_gridSize) / 2 + projectionSizeOffset;
}
