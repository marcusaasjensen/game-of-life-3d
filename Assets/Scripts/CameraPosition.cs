using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private float projectionSizeOffset = 3;
    [SerializeField] private Vector3Int defaultCameraRotation = new(45, 45, 0);
    [SerializeField] private float timeToRotate = .5f;
    [SerializeField] private float timeToFixRotation = .1f;
    [SerializeField] private float rotationStep = 90f;

    private Camera _cam;
    private Transform _transform;

    private Vector3 _initialPosition;
    private Vector3 _previousWorldMousePosition;
    private Vector3 _newWorldMousePosition;

    private bool _fixRotationControlUsed;
    private void Start()
    {
        SetInitialValues();
        SetPositionWithOffset();
        SetSizeProjection();
    }
    private void Update()
    {
        OnFixRotationControl();
        OnRotateCameraControl();
    }

    private void SetInitialValues()
    {
        _cam = Camera.main;
        _transform = transform;
        _initialPosition = _transform.position;
        _transform.eulerAngles = defaultCameraRotation;
    }
    private void OnFixRotationControl()
    {
        _fixRotationControlUsed = Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftControl);
        if (!_fixRotationControlUsed)
        {
            return;
        }
        FixCurrentRotationToStep(_newWorldMousePosition, rotationStep);
    }

    private void OnRotateCameraControl()
    {
        if (_fixRotationControlUsed)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _previousWorldMousePosition = _cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (!Input.GetMouseButton(0))
        {
            return;
        }

        _newWorldMousePosition = Vector3.Lerp(_previousWorldMousePosition, _cam.ScreenToViewportPoint(Input.mousePosition), timeToFixRotation);
        var direction = _previousWorldMousePosition - _newWorldMousePosition;
        _previousWorldMousePosition = Vector3.Lerp(_previousWorldMousePosition, _newWorldMousePosition, timeToRotate);

        RotateCameraToDirection(direction);
    }

    private void RotateCameraToDirection(Vector3 direction)
    {
        var rotationAroundXAxis = direction.y * 180;
        var rotationAroundYAxis = -direction.x * 180;

        _transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
        _transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);
    }

    private void FixCurrentRotationToStep(Vector3 direction, float step)
    {
        step = step == 0 ? 1 : step;

        var eulerAngles = _transform.eulerAngles;
        
        var targetRotation = Quaternion.Euler(
            step * Mathf.RoundToInt(1 / step * eulerAngles.x),
            step * Mathf.RoundToInt(1 / step * eulerAngles.y), 
            0f
        );

        _transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, timeToFixRotation);
    }

    private void SetPositionWithOffset()
    {
        _transform.position = new Vector3(
            _initialPosition.x + (GridCellManager.GridSize.x - 1) / 2f,
            _initialPosition.y + (GridCellManager.GridSize.y - 1) / 2f,
            _initialPosition.z + (GridCellManager.GridSize.z - 1) / 2f
        );
    }
    private void SetSizeProjection() => _cam.orthographicSize = Vector3.Magnitude(GridCellManager.GridSize / 2) + projectionSizeOffset;
}
