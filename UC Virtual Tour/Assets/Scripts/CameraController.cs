using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

// Class for handling camera interaction on location spheres
public class CameraController : MonoBehaviour
{
    // Base speed at 60 fps
    [SerializeField] float rotateSpeed = 4;
    [SerializeField] float zoomSpeed;
    [SerializeField] float scrollZoomSpeed;
    [SerializeField] float minFieldOfView = 40.0f;
    [SerializeField] float maxFieldOfView = 110.0f;
    // TODO: Deprecate
    [SerializeField] float defaultFieldOfView = 90.0f;
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

    Coroutine startInitialPan;
    Coroutine initialPan;
    // TODO: too many bools for one functionality; refactor
    bool isCurrentSphereInteracted; 
    bool initialPanPrepared;
    bool initialPanRunning;

    // Used for showing fps
    float deltaTime;
    float fps;

    void Awake()
    {
        // TODO: Refactor this, design conflicts existing
        TourManager.onLocationSphereChanged += ResetFlags;

        ResetCamera();
    }

    void Update()
    {   
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;

        HandleSphereNavigation();
        if (!isCurrentSphereInteracted && !initialPanPrepared)
        {
            startInitialPan = StartCoroutine(StartInitialPan());
        }
    }

    // When user rotates the camera to the uppermost and lowermost part of the location sphere, flickering occurs. This function prevents this problem by clamping the vertical angle.
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

    // Coroutine for initializing the initial pan
    IEnumerator StartInitialPan()
    {
        initialPanPrepared = true;
        yield return new WaitForSeconds(initialPanDelay);
        initialPan = StartCoroutine(InitialPan());
    }

    // When user doesn't interact with the current location sphere after a specified time, the camera will automatically pan to either left or right.
    IEnumerator InitialPan()
    {
        initialPanRunning = true;

        while(!isCurrentSphereInteracted && initialPanRunning)
        {
            if (initialPanSpeed < initialPanMaxSpeed)
            {
                initialPanSpeed += initialPanAcceleration * Time.deltaTime;
            }

            transform.Rotate(Vector3.up, initialPanSpeed * Time.deltaTime * initialRotationDir, Space.World);

            yield return null;
        }
    }

    // Resets the boolean flags and coroutine used by the last location sphere
    void ResetFlags(GameObject currentLocationSphere)
    {  
        // prevents running multiple coroutines
        if (initialPanPrepared)
        {
            StopCoroutine(startInitialPan); 
        }
        if (initialPanRunning)
        {
            initialPanRunning = false;
        }

        initialPanSpeed = initialPanCurrentSpeed;
        isCurrentSphereInteracted = false;
        initialPanPrepared = false;
        initialRotationDir = Random.Range(0, 2) == 0 ? -1 : 1;

    }

    // Function for handling camera rotation and zooming based on user input
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
            // fpsMultiplier is used to make camera rotation frame-independent
            float fpsMultiplier = fps / 60;
            dragVelocity = Input.mousePosition - lastMousePosition;
            lastMousePosition = Input.mousePosition;    

            lastPanInput = currentPanInput;
            
            // Rotate camera based on mouse actions
            currentPanInput =  new Vector3(ClampVerticalAngle(transform.localEulerAngles.x + (dragVelocity.y * Time.deltaTime * rotateSpeed * fpsMultiplier)), transform.localEulerAngles.y + (dragVelocity.x * Time.deltaTime * -rotateSpeed * fpsMultiplier), 0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        float scrollInput = Input.mouseScrollDelta.y;
        if ((Input.GetMouseButton(1) || Input.GetMouseButton(2)) && !IsPointerOverUIObject())
        {
            isCurrentSphereInteracted = true;

            // Move camera forward/backward (practically zooming in/out)
            fieldOfView = Mathf.Clamp(fieldOfView + Input.GetAxis("Mouse Y") * Time.deltaTime * zoomSpeed, minFieldOfView, maxFieldOfView);
            UpdateCameraFOV();
        }
        else if (scrollInput != 0)
        {
            isCurrentSphereInteracted = true;
            fieldOfView = Mathf.Clamp(fieldOfView + scrollInput * Time.deltaTime * scrollZoomSpeed, minFieldOfView, maxFieldOfView);
            UpdateCameraFOV();
        }
        
        // Updates camera rotation
        if (isCurrentSphereInteracted)
        {
            transform.localEulerAngles = currentPanInput;
        }
    }

    // Function for checking if the mouse cursor is pointing over a UI component. Used to prevent other functions from activating when this returns true.
    bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    // Resets the camera fields
    public void ResetCamera()
    {
        transform.rotation = Quaternion.identity;
        fieldOfView = defaultFieldOfView;
        UpdateCameraFOV();
    }

    // Resets the camera fields
    public void ResetCamera(Vector3 lookRotation, float fieldOfView)
    {
        lastPanInput = lookRotation;
        currentPanInput = lookRotation;

        transform.eulerAngles = lookRotation;
        this.fieldOfView = fieldOfView;
        UpdateCameraFOV();
    }

    // Updates camera field of view
    void UpdateCameraFOV()
    {
        Camera.main.fieldOfView = fieldOfView;
    }
}
