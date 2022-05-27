using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 300.0f;
    [SerializeField] float zoomSpeed = 600.0f;
    [SerializeField] float scrollZoomSpeed;
    [SerializeField] float minFieldOfView = 40.0f;
    [SerializeField] float maxFieldOfView = 110.0f;
    // TODO: Deprecate
    [SerializeField] float defaulFieldOfView = 90.0f;
    float fieldOfView;
    float maxVerticalAngle = 90f;

    bool isDragging;

    [SerializeField] float initialPanDelay;
    [SerializeField] float initialPanSpeed;
    [SerializeField] float initialPanMaxSpeed;
    [SerializeField] float initialPanAcceleration;
    float initialPanCurrentSpeed;
    int initialRotationDir;

    bool isCurrentSphereInteracted; 
    bool initialPanStarted;

    void Awake()
    {
        // TODO: Refactor this, design conflicts existing
        TourManager.onLocationSphereChanged += ResetFlags;

        ResetCamera();
    }

    void Update()
    {
        HandleMouseInput();
        if (!isCurrentSphereInteracted && !initialPanStarted)
        {
            StartCoroutine(StartInitialPan());
        }
    }

    // TODO: find a more elegant solution, current implementation is too expensive
    float ClampVerticalAngle(float angle)
    {
        float upperLimit = 360f - maxVerticalAngle;
        if (angle > 180 && angle < upperLimit)
        {
            return upperLimit;
        }
        else if (angle < 180 && angle > maxVerticalAngle)
        {
            return maxVerticalAngle;
        }
        else
        {
            return angle;
        }
    }

    IEnumerator StartInitialPan()
    {
        initialPanStarted = true;
        Debug.Log("started");
        yield return new WaitForSeconds(initialPanDelay);
        StartCoroutine(InitialPan());
    }

    // TODO: refactor, inefficient
    IEnumerator InitialPan()
    {
        while(true)
        {
            if (isCurrentSphereInteracted)
            {
                yield break;
            }

            if (initialPanSpeed < initialPanMaxSpeed)
            {
                initialPanSpeed += initialPanAcceleration * Time.deltaTime;
            }

            transform.Rotate(Vector3.up, initialPanSpeed * Time.deltaTime * initialRotationDir, Space.World);

            yield return null;
        }
    }

    void ResetFlags(GameObject currentLocationSphere)
    {
        initialPanSpeed = initialPanCurrentSpeed;
        isCurrentSphereInteracted = false;
        initialPanStarted = false;
        initialRotationDir = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            isCurrentSphereInteracted = true;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            // rotate camera based on mouse actions
            // TODO: refactor
            transform.localEulerAngles =  new Vector3(ClampVerticalAngle(transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed), transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Time.deltaTime * -rotateSpeed, 0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        float scrollInput = Input.mouseScrollDelta.y;
        if ((Input.GetMouseButton(1) || Input.GetMouseButton(2)) && !IsPointerOverUIObject())
        {
            isCurrentSphereInteracted = true;

            // move camera forward/backward
            fieldOfView = Mathf.Clamp(fieldOfView + Input.GetAxis("Mouse Y") * Time.deltaTime * zoomSpeed, minFieldOfView, maxFieldOfView);
            UpdateCameraFOV();
        }
        else if (scrollInput != 0)
        {
            isCurrentSphereInteracted = true;
            fieldOfView = Mathf.Clamp(fieldOfView + scrollInput * Time.deltaTime * scrollZoomSpeed, minFieldOfView, maxFieldOfView);
            UpdateCameraFOV();
        }
    }

    bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void ResetCamera()
    {
        transform.rotation = Quaternion.identity;
        fieldOfView = defaulFieldOfView;
        UpdateCameraFOV();
    }

    public void ResetCamera(Vector3 lookRotation, float fieldOfView)
    {
        transform.eulerAngles = lookRotation;
        // TODO: refactor
        this.fieldOfView = fieldOfView;
        UpdateCameraFOV();
    }

    void UpdateCameraFOV()
    {
        Camera.main.fieldOfView = fieldOfView;
    }
}
