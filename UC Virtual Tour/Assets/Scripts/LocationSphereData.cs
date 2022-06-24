using UnityEngine;

// Class for storing location sphere data
public class LocationSphereData : MonoBehaviour
{
    // Look rotation used by CameraController
    public Vector3 lookRotation = Vector3.zero;
    
    // Field of view used by CameraController
    public float fieldOfView = 90.0f;
    
    // Slideshow sprites used for slideshow system
    public Sprite[] slideshowImages;
    
    // used for narration system
    public AudioClip audioNarration;
    
    // used for description system
    public string locationName;
    [TextArea]
    public string locationDescription;
    
    // To be shown at the bottom left corner
    public string locationBuilding;
    public string locationFloor;
}
