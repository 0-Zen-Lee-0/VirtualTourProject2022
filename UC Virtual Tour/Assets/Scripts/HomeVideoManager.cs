using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class HomeVideoManager : MonoBehaviour
{
    public static HomeVideoManager Instance { get; private set; }

    VideoPlayer videoPlayer;
    [SerializeField] string homeClipURL;
    [SerializeField] GameObject homeImage;

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
        videoPlayer = GetComponent<VideoPlayer>();
        PlayHomeClip();
        videoPlayer.prepareCompleted += HideHomeImage;
    }

    void HideHomeImage(VideoPlayer videoPlayer)
    {
        homeImage.SetActive(false);
    }

    public void PlayHomeClip()
    {
        videoPlayer.url = homeClipURL;
        videoPlayer.Play();
    }

    public void StopHomeClip()
    {
        videoPlayer.Stop();
        videoPlayer.targetTexture.Release();
    }
}
