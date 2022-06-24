using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Class for managing the UI of various systems used by location spheres
public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [SerializeField] GameObject rightButtonsPanel;
    [SerializeField] GameObject bottomLeftPanel;
    [SerializeField] GameObject slideshowPanel;
    [SerializeField] GameObject descriptionPanel;
    [SerializeField] GameObject videoPanel;

    [SerializeField] Button audioButton;
    [SerializeField] Sprite nonMuteSprite;
    [SerializeField] Sprite muteSprite;

    [SerializeField] TextMeshProUGUI buildingNameText;
    [SerializeField] TextMeshProUGUI floorNameText;

    [SerializeField] TextMeshProUGUI locationNameText;
    [SerializeField] TextMeshProUGUI locationDescriptionText;

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
        setAudioButtonSprite();        
    }

    // Sets audio button sprite based on mute state
    void setAudioButtonSprite()
    {
        if (AudioManager.Instance.isAudioMuted)
        {
            audioButton.image.sprite = muteSprite;
        }
        else
        {
            audioButton.image.sprite = nonMuteSprite;
        }
    }

    public void showBottomLeftPanel()
    {
        bottomLeftPanel.SetActive(true);
    }

    public void hideBottomLeftPanel()
    {
        bottomLeftPanel.SetActive(false);
    }

    public void setBottomLeftPanel(string buildingName, string floorName)
    {
        buildingNameText.text = buildingName;
        floorNameText.text = floorName;
    }

    public void ShowRightButtonsPanel()
    {
        rightButtonsPanel.SetActive(true);
    }

    public void HideRightButtonsPanel()
    {
        rightButtonsPanel.SetActive(false);
    }

    public void ShowSlideshowPanel()
    {
        descriptionPanel.SetActive(false);
        slideshowPanel.SetActive(true);
        UIControl.Instance.HideUI();
    }

    public void HideSlideshowPanel()
    {
        slideshowPanel.SetActive(false);
    }

    public void ToggleAudioButton()
    {
        // Reverses the mute state
        AudioManager.Instance.ToggleMute();
        setAudioButtonSprite();
    }

    public void EnableAudioButton()
    {
        audioButton.interactable = true;
    }

    public void DisableAudioButton()
    {
        audioButton.interactable = false;
    }

    public void ShowAudioButton()
    {
        audioButton.gameObject.SetActive(true);
    }

    public void HideAudioButton()
    {
        audioButton.gameObject.SetActive(false);
    }

    public void ToggleDescriptionPanel()
    {
        bool currentState = descriptionPanel.activeSelf;
        descriptionPanel.SetActive(!currentState);
    }

    public void ShowDescriptionPanel()
    {
        descriptionPanel.SetActive(true);
    }

    public void ShowVideoPanel()
    {
        videoPanel.SetActive(true);
    }

    public void HideVideoPanel()
    {
        videoPanel.SetActive(false);
    }

    public void DisableDescriptionPanel()
    {
        descriptionPanel.SetActive(false);
    }

    public void InitializeDescriptionData(String locationName, String locationDescription)
    {
        locationNameText.SetText(locationName);
        locationDescriptionText.SetText(locationDescription);
    }
}
