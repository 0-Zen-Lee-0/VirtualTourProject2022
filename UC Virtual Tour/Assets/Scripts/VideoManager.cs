using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void StartVideo(CampusData campusData)
    {
        if (!campusData.IsIntroductoryClipPlayed)
        {
            UIManager.Instance.showVideoPanel();
            VideoClip introductoryClip = campusData.introductoryClip;
            campusData.IsIntroductoryClipPlayed = true;
            videoPlayer.clip = introductoryClip;
            videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        UIManager.Instance.hideVideoPanel();
        videoPlayer.Stop();
        
    }
}
