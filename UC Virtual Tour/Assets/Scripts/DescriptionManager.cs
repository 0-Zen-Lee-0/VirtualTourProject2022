using UnityEngine;
using UnityEngine.UI;

// Class for handling the description for every location sphere
public class DescriptionManager : MonoBehaviour
{
    [SerializeField] Button descriptionButton;

    void Awake()
    {
        TourManager.onLocationSphereChanged += InitializeDescriptionSystem;
    }

    // Called when a new location sphere is selected
    void InitializeDescriptionSystem(GameObject currentLocationSphere)
    {
        // Ensure that the entry was filled out, if not, the overlay button will not be shown
        if (currentLocationSphere.GetComponent<LocationSphereData>().locationDescription.Length > 0)
        {
            // Shows description panel
            UIManager.Instance.ShowDescriptionPanel();

            descriptionButton.interactable = true;

            string locationName = currentLocationSphere.GetComponent<LocationSphereData>().locationName;
            string locationDescription = currentLocationSphere.GetComponent<LocationSphereData>().locationDescription;
            UIManager.Instance.InitializeDescriptionData(locationName, locationDescription);
        }
        else
        {
            // Disables the description panel when moving to a location sphere without a description
            UIManager.Instance.DisableDescriptionPanel();
            descriptionButton.interactable = false;
        }
    }
}
