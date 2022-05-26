using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Ponder if this should be merged with UIManager
public class SlideshowManager : MonoBehaviour
{
    [SerializeField] Image slideshowImage;
    [SerializeField] Button slideshowButton;

    Sprite[] slideshowImages;
    int currentImageIndex = 0;
    float slideshowFadeDuration = 0.8f;

    void Awake()
    {
        TourManager.onLocationSphereChanged += UpdateSlideshowSystem;
    }

    void UpdateSlideshowSystem(GameObject currentLocationSphere)
    {
        currentImageIndex = 0;

        // gets the slideshow images from locationSphereData
        slideshowImages = currentLocationSphere.GetComponent<LocationSphereData>().slideshowImages;

        // the button only shows up when there is at least one image located
        if (slideshowImages.Length > 0)
        {
            slideshowButton.gameObject.SetActive(true);

            slideshowImage.sprite = slideshowImages[currentImageIndex];
        }        
        else
        {
            slideshowButton.gameObject.SetActive(false);
        }
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
