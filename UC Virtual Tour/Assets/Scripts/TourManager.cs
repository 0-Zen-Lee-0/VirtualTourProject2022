using System;
using System.Collections;
using UnityEngine;

// TODO: null safety!
public class TourManager : MonoBehaviour
{
    public static TourManager Instance { get; private set; }
    public GameObject[] locationSpheres;
    [SerializeField] int initialLocationSphereIndex;

    // event triggered when a location sphere is selected
    public static event Action<GameObject> onLocationSphereChanged;

    // Note: Inconsistent; if I make this public, there would be no point in sending a game object from the event
    GameObject currentLocationSphere;
    GameObject previousLocationSphere;

    public bool isFirstLocationSphereLoaded { get; private set; }

    CampusData currentCampusData;

    Vector3 startingLookRotation;

    [SerializeField] GameObject transitionSphere;
    [SerializeField] float transitionDuration = 1f;  

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
        LoadInitialSite();
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
                    int locationIndex = hit.transform.gameObject.GetComponent<MoveLocation>().GetLocationIndex();
                    startingLookRotation = hit.transform.gameObject.GetComponent<MoveLocation>().GetLookRotation();
                    LoadSite(locationIndex);
                }
                if (hit.transform.gameObject.tag == "Description")
                {
                    UIManager.Instance.ToggleDescriptionPanel();
                }
            }
        }
    }

    public void LoadInitialSite()
    {
        HideAllSites();

        // assign the current location sphere
        currentLocationSphere = locationSpheres[initialLocationSphereIndex];
        
        LoadLocationSphereData();

        // show selected location
        currentLocationSphere.SetActive(true);
        // call event 
        onLocationSphereChanged?.Invoke(currentLocationSphere);
    }

    // TODO: refactor, two load sites function with tons of repetitive code
    public void LoadSite(CampusData campusData)
    {
        currentCampusData = campusData;
        int locationIndex = campusData.locationSphereIndex;
        int campusIndex = campusData.campusIndex;

        // set true to this bool, will be used for determining whether to show the bottom left panel at start
        isFirstLocationSphereLoaded = true;

        // before loading the new site, keep reference of the previous one
        previousLocationSphere = currentLocationSphere;
        
        // assign the current location sphere
        currentLocationSphere = locationSpheres[locationIndex];

        SetupBottomLeftUI(currentLocationSphere.name);

        StartCoroutine(StartTransition(campusIndex));
    }
 
    public void LoadSite(int locationIndex)
    {
        // set true to this bool, will be used for determining whether to show the bottom left panel at start
        isFirstLocationSphereLoaded = true;

        // before loading the new site, keep reference of the previous one
        previousLocationSphere = currentLocationSphere;
        
        // assign the current location sphere
        currentLocationSphere = locationSpheres[locationIndex];

        SetupBottomLeftUI(currentLocationSphere.name);

        StartCoroutine(StartTransition());
    }

    void SetupBottomLeftUI(string locationSphereName)
    {
        string[] nameSubstring = locationSphereName.Split("_");
        string buildingName;
        string floorName;
        switch (nameSubstring[0])
        {
            case "C":
                buildingName = "Campo Libertad";
                break;
            case "L":
                buildingName = "Legarda";
                break;
            case "M":
                buildingName = "Main Building";
                break;
            case "F":
                buildingName = "F Building";
                break;
            case "N":
                buildingName = "EDS Building";
                break;
            case "U":
                buildingName = "BRS Building";
                break;
            case "S":
                buildingName = "Science Building";
                break;
            case "G":
                buildingName = "Gym";
                break;
            case "USG":
                buildingName = "Intersection";
                break;
            default:
                buildingName = nameSubstring[0];
                break;
        }
        switch (nameSubstring[1])
        {
            case "1":
                floorName = "1st floor";
                break;
            case "2":
                floorName = "2nd floor";
                break;
            case "3":
                floorName = "3rd floor";
                break;
            case "0":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "10":
                floorName = nameSubstring[1]+"th floor";
                break;
            default:
                floorName = nameSubstring[1];
                break;
        }
        UIManager.Instance.setBottomLeftPanel(buildingName, floorName);
    }

    IEnumerator StartTransition()
    {
        // animating transition sphere
        transitionSphere.SetActive(true);
        Material material = transitionSphere.GetComponent<Renderer>().material;
        Renderer renderer = transitionSphere.GetComponent<Renderer>();

        // fade out
        Color color = material.color;
        float frameTime = Time.deltaTime;
        float duration = transitionDuration / 2;
        for (float i = 0; i < duration; i += frameTime)
        {
            frameTime = Time.deltaTime;

            color.a = i / duration;
            material.color = color;

            yield return new WaitForSeconds(frameTime);
        }

        // hide previous location
        previousLocationSphere.SetActive(false);
        LoadLocationSphereData();
        // show selected location
        currentLocationSphere.SetActive(true);

        // call event 
        onLocationSphereChanged?.Invoke(currentLocationSphere);

        // fade in
        for (float i = duration; i > 0; i -= frameTime)
        {
            frameTime = Time.deltaTime;

            color.a = i / duration;
            material.color = color;

            yield return new WaitForSeconds(frameTime);
        }
        transitionSphere.SetActive(false);
    }

    // for load site called by initial campus buttons
    IEnumerator StartTransition(int campusIndex)
    {
        // Temporary fix
        UIControl.Instance.HideStartPage();
        UIControl.Instance.HideCampusMenu();

        // animating transition sphere
        transitionSphere.SetActive(true);
        Material material = transitionSphere.GetComponent<Renderer>().material;
        Renderer renderer = transitionSphere.GetComponent<Renderer>();

        // fade out
        Color color = material.color;
        float frameTime = Time.deltaTime;
        float duration = transitionDuration / 2;
        for (float i = 0; i < duration; i += frameTime)
        {
            frameTime = Time.deltaTime;

            color.a = i / duration;
            material.color = color;

            yield return new WaitForSeconds(frameTime);
        }

        // hide previous location
        previousLocationSphere.SetActive(false);
        LoadLocationSphereData();
        // show selected location
        currentLocationSphere.SetActive(true);

        

        // call event 
        onLocationSphereChanged?.Invoke(currentLocationSphere);

        // fade in
        for (float i = duration; i > 0; i -= frameTime)
        {
            frameTime = Time.deltaTime;

            color.a = i / duration;
            material.color = color;

            yield return new WaitForSeconds(frameTime);
        }
        transitionSphere.SetActive(false);
        // show left quick locate ui
        ChooseCampus(campusIndex);
    }

    void LoadLocationSphereData()
    {
        if (startingLookRotation == Vector3.zero)
        {
            startingLookRotation = currentLocationSphere.GetComponent<LocationSphereData>().lookRotation;
        }
        float defaultFieldOfView = currentLocationSphere.GetComponent<LocationSphereData>().fieldOfView;

        Camera.main.GetComponent<CameraController>().ResetCamera(startingLookRotation, defaultFieldOfView);
    }

    // TODO: deprecate? If so, replace int indexes with gameobject references
    void HideAllSites()
    {
        foreach (GameObject locationSphere in locationSpheres)
        {
            locationSphere.SetActive(false);
        }
    }

    public void ChooseCampus(int campusIndex)
    {
        UIControl.Instance.ChooseCampus(campusIndex);
    }
}
