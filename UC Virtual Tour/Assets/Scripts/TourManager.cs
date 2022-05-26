using System;
using UnityEngine;

// TODO: null safety!
public class TourManager : MonoBehaviour
{
    public static TourManager Instance {get; private set;}
    public GameObject[] locationSpheres;

    public static event Action<GameObject> onLocationSphereChanged;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        //LoadSite(9);
    }
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.gameObject.tag == "Move")
                {
                    LoadSite(hit.transform.gameObject.GetComponent<MoveLocation>().GetLocationIndex());
                }
                if (hit.transform.gameObject.tag == "Description")
                {
                    UIManager.Instance.ToggleDescriptionPanel();
                }
            }
        }
    }
 
    public void LoadSite(int locationIndex)
    {
        Vector3 defaultLookRotation = locationSpheres[locationIndex].GetComponent<LocationSphereData>().lookRotation;
        float defaultFieldOfView = locationSpheres[locationIndex].GetComponent<LocationSphereData>().fieldOfView;

        HideAllSites();
    
        // show selected location
        locationSpheres[locationIndex].SetActive(true);
        Camera.main.GetComponent<CameraController>().ResetCamera(defaultLookRotation, defaultFieldOfView);

        // TODO: figure out if I really need ?.Invoke here
        onLocationSphereChanged?.Invoke(locationSpheres[locationIndex]);
    }

    void HideAllSites()
    {
        foreach (GameObject locationSphere in locationSpheres)
        {
            locationSphere.SetActive(false);
        }
    }
}
