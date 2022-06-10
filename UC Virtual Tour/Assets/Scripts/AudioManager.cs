using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}

    AudioSource source;
    AudioClip clip;

    public bool isAudioMuted;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        TourManager.onLocationSphereChanged += UpdateAudioSystem;
        source = GetComponent<AudioSource>();
    }

    void UpdateAudioSystem(GameObject currentLocationSphere)
    {
        PlayAudioNarration(currentLocationSphere);
    }

    void PlayAudioNarration(GameObject currentLocationSphere)
    {
        source.Stop();

        // TODO: refactor, find a shorthand for this
        if (currentLocationSphere.GetComponent<LocationSphereData>().audioNarration != null)
        {
            UIManager.Instance.ShowAudioButton();

            clip = currentLocationSphere.GetComponent<LocationSphereData>().audioNarration;
            source.PlayOneShot(clip);
        }
        else
        {
            UIManager.Instance.HideAudioButton();
        }
    }

    public void ToggleMute()
    {
        isAudioMuted = !isAudioMuted;
        source.mute = isAudioMuted;
    }
}
