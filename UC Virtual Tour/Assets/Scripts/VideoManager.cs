using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;
    int locationSphereIndex;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += StopVideo;
    }

    public void StartVideo(CampusData campusData)
    {
        locationSphereIndex = campusData.locationSphereIndex;

        if (!campusData.IsIntroductoryClipPlayed)
        {
            TourManager.Instance.LoadInitialSite();

            UIManager.Instance.showVideoPanel();

            VideoClip introductoryClip = campusData.introductoryClip;
            campusData.IsIntroductoryClipPlayed = true;
            videoPlayer.clip = introductoryClip;
            videoPlayer.Play();
        }
        else
        {
            LoadSite();
        }
    }

    //public void StartVideo(CampusData campusData)
    //{
    //    locationSphereIndex = campusData.locationSphereIndex;

    //    if (!campusData.IsIntroductoryClipPlayed)
    //    {
    //        TourManager.Instance.LoadInitialSite();

    //        UIManager.Instance.showVideoPanel();

    //        campusData.IsIntroductoryClipPlayed = true;
    //        videoPlayer.url = campusData.introductoryClipURL;
    //        videoPlayer.Play();
    //    }
    //    else
    //    {
    //        LoadSite();
    //    }
    //}

    // TODO: current implementation is too hacky, try to find a more elegant implementation
    void StopVideo(VideoPlayer videoPlayer)
    {
        StopVideo();
    }

    public void StopVideo()
    {
        UIManager.Instance.hideVideoPanel();
        videoPlayer.Stop();
        videoPlayer.targetTexture.Release();
        LoadSite();
    }

    void LoadSite()
    {
        TourManager.Instance.LoadSite(locationSphereIndex);
    }
}
