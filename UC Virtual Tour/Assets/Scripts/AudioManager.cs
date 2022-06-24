using UnityEngine;

// Class for handling the audio narration for every location sphere
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

    // Called when a new location sphere is active
    void UpdateAudioSystem(GameObject currentLocationSphere)
    {
        PlayAudioNarration(currentLocationSphere);
    }

    // Handles the playing of audio narration and showing/hiding the audio button
    void PlayAudioNarration(GameObject currentLocationSphere)
    {
        source.Stop();

        // TODO: refactor, find a shorthand for this
        if (currentLocationSphere.GetComponent<LocationSphereData>().audioNarration != null)
        {
            UIManager.Instance.EnableAudioButton();

            clip = currentLocationSphere.GetComponent<LocationSphereData>().audioNarration;
            source.PlayOneShot(clip);
        }
        else
        {
            UIManager.Instance.DisableAudioButton();
        }
    }

    // Togles audio narration mute
    public void ToggleMute()
    {
        isAudioMuted = !isAudioMuted;
        source.mute = isAudioMuted;
    }
}
