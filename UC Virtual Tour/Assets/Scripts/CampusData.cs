using UnityEngine;

// Class for storing campus data
public class CampusData : MonoBehaviour
{
    public string campusName;
    [HideInInspector] public int campusIndex;

    // URL for the introductory clip that is played when selecting a campus for the first time
    public string introductoryClipURL;
    [HideInInspector] public bool IsIntroductoryClipPlayed;
    // the index (Based on TourManager locationSpheres array) of the location sphere that will load when selecting the campus
    public int locationSphereIndex;

    void Start()
    {
        switch(campusName)
        {
            case "Legarda":
                campusIndex = 0;
                break;
            case "Main":
                campusIndex = 1;
                break;
            case "Libertad":
                campusIndex = 2;
                break;
        }
    }
}
