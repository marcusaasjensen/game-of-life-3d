using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] CellManager cellManager;
    [SerializeField] float step = 1;
    [SerializeField] float projectionSizeOffset = 3;

    Vector3 _initialPosition;
    Camera _cam;

    void Start()
    {
        _initialPosition = transform.position;
        _cam = Camera.main;
    }
    void Update()
    {
        SetLocalPositionWithOffset();
        SetSizeProjection();
    }

    void SetLocalPositionWithOffset()
    {
        if (!cellManager) return;
        transform.position = new(
        _initialPosition.x + step * cellManager.GridSize.x / 2,
        _initialPosition.y + step * cellManager.GridSize.y / 2,
        _initialPosition.z + step * cellManager.GridSize.z / 2
        );
    }

    void SetSizeProjection()
    {
        if (!cellManager || !_cam) return;
        _cam.orthographicSize = Vector3.Magnitude(cellManager.GridSize) / 2 + projectionSizeOffset;
    }
}
