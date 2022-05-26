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

        TourManager.onLocationSphereChanged += PlayAudioNarration;
        source = GetComponent<AudioSource>();
    }

    void PlayAudioNarration(GameObject currentLocationSphere)
    {
        source.Stop();

        // TODO: refactor, find a shorthand for this
        if (currentLocationSphere.GetComponent<LocationSphereData>().audioNarration != null)
        {
            clip = currentLocationSphere.GetComponent<LocationSphereData>().audioNarration;
            source.PlayOneShot(clip);
        }
    }

    public void ToggleMute()
    {
        isAudioMuted = !isAudioMuted;
        source.mute = isAudioMuted;
    }
}
