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

    bool isHomeImageHidden;

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
    }

    void Update()
    {
        if (videoPlayer.time > 0 && !isHomeImageHidden)
        {
            HideHomeImage();
            isHomeImageHidden = true;
        }
    }

    void HideHomeImage()
    {
        homeImage.SetActive(false);
    }

    void ShowHomeImage()
    {
        homeImage.SetActive(true);
    }

    public void PlayHomeClip()
    {
        // reset bool flag
        isHomeImageHidden = false;
        ShowHomeImage();

        videoPlayer.url = homeClipURL;
        videoPlayer.Play();
    }

    public void StopHomeClip()
    {
        videoPlayer.Stop();
        videoPlayer.targetTexture.Release();
    }
}
