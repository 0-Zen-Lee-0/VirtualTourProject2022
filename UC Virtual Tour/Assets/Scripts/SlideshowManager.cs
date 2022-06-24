using UnityEngine;
using UnityEngine.UI;

// Class for handling the slideshow system 
public class SlideshowManager : MonoBehaviour
{
    [SerializeField] Image slideshowImage;
    [SerializeField] Button slideshowButton;
    [SerializeField] Button creditsButton;  

    // Holds the reference of the current working slideshow sprites
    Sprite[] slideshowImages;
    // Sprites for the current location sphere
    Sprite[] locationSphereSlideshowImages;
    // Sprites for the credits
    [SerializeField] Sprite[] creditSlideshowImages;

    // Just use one index for both location sphere and credits, adding another variable that holds a reference to the current index field that should be used is possible, however
    int currentImageIndex = 0;
    float slideshowFadeDuration = 0.8f;

    void Awake()
    {
        TourManager.onLocationSphereChanged += InitializeLocationSphereSlideshowSystem;
    }

    // Called when a new location sphere is active
    void InitializeLocationSphereSlideshowSystem(GameObject currentLocationSphere)
    { 
        // Gets the slideshow images from locationSphereData
        locationSphereSlideshowImages = currentLocationSphere.GetComponent<LocationSphereData>().slideshowImages;

        // The button only shows up when there is at least one image located
        if (locationSphereSlideshowImages.Length > 0)
        {
            slideshowButton.interactable = true;
        }        
        else
        {
            slideshowButton.interactable = false;
        }
    }

    // Function attached to the slideshow button, activates when the button is pressed
    // This should only be called when there is at least one sprite slideshow image in the current location sphere; should be safe assuming this function can only be called when the slideshow button is active
    public void ShowLocationSphereSlideshowSystem()
    {
        slideshowImage.sprite = locationSphereSlideshowImages[0];

        currentImageIndex = 0;
        slideshowImages = locationSphereSlideshowImages;
        UIManager.Instance.ShowSlideshowPanel();
    }

    // Function attached to the credits button, activates when the button is pressed
    // There should at least be three credit sprites, so no need to put a condition for when there are no credit sprites
    public void ShowCreditSlideshowSystem()
    {
        slideshowImage.sprite = creditSlideshowImages[0];

        currentImageIndex = 0;
        slideshowImages = creditSlideshowImages;
        UIManager.Instance.ShowSlideshowPanel();
    }

    // Function attached to the next button
    public void SelectNextImage()
    {
        currentImageIndex++;
        // Wraps around to the first array index to prevent arrayIndexOutOfBounds
        if (currentImageIndex >= slideshowImages.Length)
        {
            currentImageIndex = 0;
        }
        UpdateSlideshowImage();
    }

    // Function attached to the previous button
    public void SelectPreviousImage()
    {
        currentImageIndex--;
        // Wraps around to the last array index
        if (currentImageIndex < 0)
        {
            currentImageIndex = slideshowImages.Length - 1;
        }
        UpdateSlideshowImage();
    }

    // Transitions to the next selected slideshow image
    void UpdateSlideshowImage()
    {
        slideshowImage.canvasRenderer.SetAlpha(0f);
        slideshowImage.CrossFadeAlpha(1, slideshowFadeDuration, false);
        slideshowImage.sprite = slideshowImages[currentImageIndex];
    }
}
