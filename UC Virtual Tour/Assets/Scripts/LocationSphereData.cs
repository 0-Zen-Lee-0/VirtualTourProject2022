using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSphereData : MonoBehaviour
{
    // TODO: Link defaultFOV from CameraController to this
    // used for tour system
    public Vector3 lookRotation = Vector3.zero;
    public float fieldOfView = 90.0f;
    // used for slideshow system
    public Sprite[] slideshowImages;
    // used for narration system
    public AudioClip audioNarration;
    // used for description system
    public string locationName;
    [TextArea]
    public string locationDescription;
}
