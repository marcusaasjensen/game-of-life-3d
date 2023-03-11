using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] float projectionSizeOffset = 3;
    [SerializeField] Vector3Int defaultCameraRotation = new(45, 45, 0);
    [SerializeField] float timeToFixRotation = .1f;
    [SerializeField] float rotationStep = 90f;

    Camera _cam;
    Transform _transform;

    Vector3 _initialPosition;
    Vector3 _previousWorldMousePosition;
    Vector3 _newWorldMousePosition;

    void Start()
    {
        _cam = Camera.main;
        _transform = transform;
        _initialPosition = _transform.position;
        SetLocalPositionWithOffset();
        SetSizeProjection();
        _transform.eulerAngles = defaultCameraRotation;
    }
    void Update()
    {
        if (!Input.GetMouseButton(0) && Input.GetMouseButton(1))
            FixCurrentRotationToStep(_newWorldMousePosition, rotationStep);
        else
            OnLeftMouseButton();
    }

    void OnLeftMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
            _previousWorldMousePosition = _cam.ScreenToViewportPoint(Input.mousePosition);

        if (!Input.GetMouseButton(0)) return;

        _newWorldMousePosition = _cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 direction = _previousWorldMousePosition - _newWorldMousePosition;
        _previousWorldMousePosition = _newWorldMousePosition;

        RotateCameraToDirection(direction);

    }

    void RotateCameraToDirection(Vector3 direction)
    {
        float rotationAroundXAxis = direction.y * 180;
        float rotationAroundYAxis = -direction.x * 180;

        _transform.Rotate(new(1, 0, 0), rotationAroundXAxis);
        _transform.Rotate(new(0, 1, 0), rotationAroundYAxis, Space.World);
    }

    void FixCurrentRotationToStep(Vector3 direction, float rotationStep)
    {
        rotationStep = rotationStep == 0 ? 1 : rotationStep;

        Quaternion targetRotation = Quaternion.Euler(
            rotationStep * Mathf.RoundToInt(1 / rotationStep * _transform.eulerAngles.x),
            rotationStep * Mathf.RoundToInt(1 / rotationStep * _transform.eulerAngles.y), 
            0f
        );

        _transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, timeToFixRotation);
    }

    void SetLocalPositionWithOffset()
    {
        _transform.position = new(
            _initialPosition.x + (GridCellManager.s_gridSize.x - 1) / 2,
            _initialPosition.y + (GridCellManager.s_gridSize.y - 1) / 2,
            _initialPosition.z + (GridCellManager.s_gridSize.z - 1) / 2
        );
    }
    void SetSizeProjection() => _cam.orthographicSize = Vector3.Magnitude(GridCellManager.s_gridSize / 2) + projectionSizeOffset;
}
