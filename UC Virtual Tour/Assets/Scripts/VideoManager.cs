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
        if (!campusData.IsIntroductoryClipPlayed)
        {
            UIManager.Instance.showVideoPanel();

            locationSphereIndex = campusData.locationSphereIndex;

            VideoClip introductoryClip = campusData.introductoryClip;
            campusData.IsIntroductoryClipPlayed = true;
            videoPlayer.clip = introductoryClip;
            videoPlayer.Play();
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
        videoPlayer.Stop();

        TourManager.Instance.LoadSite(locationSphereIndex);
    }
}
