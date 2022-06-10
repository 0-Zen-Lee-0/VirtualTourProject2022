using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CampusData : MonoBehaviour
{
    public string campusName;
    [HideInInspector] public int campusIndex;

    public VideoClip introductoryClip;
    public string introductoryClipURL;
    public bool IsIntroductoryClipPlayed;
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
