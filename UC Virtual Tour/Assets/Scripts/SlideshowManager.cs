using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Ponder if this should be merged with UIManager
public class SlideshowManager : MonoBehaviour
{
    [SerializeField] Image slideshowImage;
    [SerializeField] Button slideshowButton;
    [SerializeField] Button creditsButton;  

    // holds the reference of the current working slideshow sprites
    Sprite[] slideshowImages;
    // sprites for the current location sphere
    Sprite[] locationSphereSlideshowImages;
    // sprites for the credits
    [SerializeField] Sprite[] creditSlideshowImages;

    // just use one index for both location sphere and credits, adding another variable that holds a reference of the current index field that should be used is possible, however
    int currentImageIndex = 0;
    float slideshowFadeDuration = 0.8f;

    void Awake()
    {
        TourManager.onLocationSphereChanged += InitializeLocationSphereSlideshowSystem;
    }

    void InitializeLocationSphereSlideshowSystem(GameObject currentLocationSphere)
    { 
        // gets the slideshow images from locationSphereData
        locationSphereSlideshowImages = currentLocationSphere.GetComponent<LocationSphereData>().slideshowImages;

        // the button only shows up when there is at least one image located
        if (locationSphereSlideshowImages.Length > 0)
        {
            //slideshowButton.gameObject.SetActive(true);
            slideshowButton.interactable = true;
            
        }        
        else
        {
            //slideshowButton.gameObject.SetActive(false);
            slideshowButton.interactable = false;
        }
    }

    public void ShowLocationSphereSlideshowSystem()
    {
        // this should only be called when there is at least one sprite slideshow image in the current location sphere
        // should be safe assuming this function can only be called when the slideshow button is active
        slideshowImage.sprite = locationSphereSlideshowImages[0];

        currentImageIndex = 0;
        slideshowImages = locationSphereSlideshowImages;
        UIManager.Instance.ShowSlideshowPanel();
    }

    public void ShowCreditSlideshowSystem()
    {
        // there should at least be three credit sprites, so no need to put a condition for when there is no credit sprites
        slideshowImage.sprite = creditSlideshowImages[0];

        currentImageIndex = 0;
        slideshowImages = creditSlideshowImages;
        UIManager.Instance.ShowSlideshowPanel();
    }

    public void SelectNextImage()
    {
        currentImageIndex++;
        // wraps around to the first array index
        if (currentImageIndex >= slideshowImages.Length)
        {
            currentImageIndex = 0;
        }
        UpdateSlideshowImage();
    }

    public void SelectPreviousImage()
    {
        currentImageIndex--;
        // wraps around to the last array index
        if (currentImageIndex < 0)
        {
            currentImageIndex = slideshowImages.Length - 1;
        }
        UpdateSlideshowImage();
    }

    // transitions to the next selected slideshow image
    void UpdateSlideshowImage()
    {
        slideshowImage.canvasRenderer.SetAlpha(0f);
        slideshowImage.CrossFadeAlpha(1, slideshowFadeDuration, false);
        slideshowImage.sprite = slideshowImages[currentImageIndex];
    }
}
