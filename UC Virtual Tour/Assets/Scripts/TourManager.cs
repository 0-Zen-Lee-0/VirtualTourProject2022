using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourManager : MonoBehaviour
{
    public static TourManager Instance {get; private set;}
    public GameObject[] locationSpheres;

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
        
    }
 
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100.0f))
            {
                if(hit.transform.gameObject.tag == "Move")
                {
                    LoadSite(hit.transform.gameObject.GetComponent<MoveLocation>().GetLocationIndex());
                }
            }
        }
    }
 
    public void LoadSite(int locationIndex)
    {
        Vector3 defaultLookRotation = locationSpheres[locationIndex].GetComponent<LocationSphere>().lookRotation;
        float defaultFieldOfView = locationSpheres[locationIndex].GetComponent<LocationSphere>().fieldOfView;

        HideAllSites();
        // show selected location
        locationSpheres[locationIndex].SetActive(true);
        Camera.main.GetComponent<CameraController>().ResetCamera(defaultLookRotation, defaultFieldOfView);
    }

    void HideAllSites()
    {
        foreach (GameObject locationSphere in locationSpheres)
        {
            locationSphere.SetActive(false);
        }
    }
}
