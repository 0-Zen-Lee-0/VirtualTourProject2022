using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;
    int locationSphereIndex;
    int campusIndex;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += StopVideo;
    }

    public void StartVideo(CampusData campusData)
    {
        locationSphereIndex = campusData.locationSphereIndex;

        switch(campusData.campusName)
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

        if (!campusData.IsIntroductoryClipPlayed)
        {
            try
            {
                TourManager.Instance.LoadInitialSite();

                campusData.IsIntroductoryClipPlayed = true;
                videoPlayer.url = campusData.introductoryClipURL;

                UIManager.Instance.showVideoPanel();
                // UIManager.Instance.hideRightButtonsPanel();
                
                videoPlayer.Play();
            }
            // TODO: Specify exception
            catch (Exception e)
            {
                Debug.Log(e);
                StopVideo();
            }
        }
        else
        {
            LoadSite();
        }
    }

    // TODO: current implementation is too hacky, try to find a more elegant implementation
    void StopVideo(VideoPlayer videoPlayer)
    {
        StopVideo();
    }

    public void StopVideo()
    {
        UIManager.Instance.hideVideoPanel();
        UIManager.Instance.showRightButtonsPanel();
        videoPlayer.Stop();
        videoPlayer.targetTexture.Release();
        LoadSite();
    }

    void LoadSite()
    {
        TourManager.Instance.LoadSite(locationSphereIndex, campusIndex);
    }
}
