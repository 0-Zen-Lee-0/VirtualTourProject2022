using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] float zoomSpeed;
    [SerializeField] float scrollZoomSpeed;
    [SerializeField] float minFieldOfView = 40.0f;
    [SerializeField] float maxFieldOfView = 110.0f;
    // TODO: Deprecate
    [SerializeField] float defaulFieldOfView = 90.0f;
    float fieldOfView;
    float maxVerticalAngle = 90f;

    bool isDragging;
    Vector3 dragVelocity;
    Vector3 lastMousePosition;
    Vector3 lastPanInput;
    Vector3 currentPanInput;
    Vector3 panVelocity;

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
        HandleSphereNavigation();
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
        yield return new WaitForSeconds(initialPanDelay);
        StartCoroutine(InitialPan());
    }

    // TODO: refactor, inefficient
    IEnumerator InitialPan()
    {
        while(!isCurrentSphereInteracted)
        {
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

    void HandleSphereNavigation()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            lastMousePosition = Input.mousePosition;

            isCurrentSphereInteracted = true;
            isDragging = true;
        }

        
        if (Input.GetMouseButton(0) && isDragging)
        {
            dragVelocity = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;    

            lastPanInput = currentPanInput;
            
            // rotate camera based on mouse actions
            currentPanInput =  new Vector3(ClampVerticalAngle(transform.localEulerAngles.x + (dragVelocity.y * Time.deltaTime * rotateSpeed)), transform.localEulerAngles.y + (dragVelocity.x * Time.deltaTime * -rotateSpeed), 0);
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
        
        // Update camera rotation
        if (isCurrentSphereInteracted)
        {
            transform.localEulerAngles = currentPanInput;
            // transform.localEulerAngles = new Vector3(Mathf.Lerp(lastPanInput.x, currentPanInput.x, Time.deltaTime * a), Mathf.Lerp(lastPanInput.y, currentPanInput.y, Time.deltaTime * a), Mathf.Lerp(lastPanInput.z, currentPanInput.z, Time.deltaTime * a));
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
        lastPanInput = lookRotation;
        currentPanInput = lookRotation;

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
