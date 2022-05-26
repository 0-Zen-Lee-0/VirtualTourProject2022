using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 300.0f;
    [SerializeField] float zoomSpeed = 600.0f;
    [SerializeField] float minFieldOfView = 40.0f;
    [SerializeField] float maxFieldOfView = 110.0f;
    // TODO: Deprecate
    [SerializeField] float defaulFieldOfView = 90.0f;
    float fieldOfView;

    bool isDragging;
    Quaternion targetRotation = Quaternion.identity;

    void Awake()
    {
        ResetCamera();
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            // rotate camera based on mouse actions
            // TODO: refactor
            transform.eulerAngles =  new Vector3(transform.eulerAngles.x + Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed, transform.eulerAngles.y + Input.GetAxis("Mouse X") * Time.deltaTime * -rotateSpeed, 0);
        }

        if (Input.GetMouseButton(1) || Input.GetMouseButton(2) && !IsPointerOverUIObject())
        {
            // move camera forward/backward
            fieldOfView = Mathf.Clamp(fieldOfView + Input.GetAxis("Mouse Y") * Time.deltaTime * zoomSpeed, minFieldOfView, maxFieldOfView);
            UpdateCameraFOV();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
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
