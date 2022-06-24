using UnityEngine;
using UnityEngine.Video;

// Class for handling the video playback at the home page
// Very similar to VideoManager.cs; had to duplicate due to time constraints, VideoManager wasn't scalable enough at the time
public class HomeVideoManager : MonoBehaviour
{
    public static HomeVideoManager Instance { get; private set; }

    VideoPlayer videoPlayer;
    // URL for the introductory clip
    [SerializeField] string homeClipURL;
    // Static image when the home clip isn't available yet
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
        // Resets bool flag
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
