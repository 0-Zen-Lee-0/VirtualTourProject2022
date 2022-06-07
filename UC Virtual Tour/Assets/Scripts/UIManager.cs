using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    //[SerializeField] GameObject leftPanel;
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
        //leftPanel.SetActive(true);
        rightButtonsPanel.SetActive(true);

        setAudioButtonSprite();        
    }

    // sets audio button sprite based on mute state
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

    public void showRightButtonsPanel()
    {
        rightButtonsPanel.SetActive(true);
    }

    public void hideRightButtonsPanel()
    {
        rightButtonsPanel.SetActive(false);
    }

    public void ShowSlideshowPanel()
    {
        descriptionPanel.SetActive(false);
        slideshowPanel.SetActive(true);
    }

    public void HideSlideshowPanel()
    {
        slideshowPanel.SetActive(false);
    }

    public void ToggleAudioButton()
    {
        // TODO: refactor if possible
        // reverses the mute state
        AudioManager.Instance.ToggleMute();
        setAudioButtonSprite();
    }

    public void ToggleDescriptionPanel()
    {
        bool currentState = descriptionPanel.activeSelf;
        descriptionPanel.SetActive(!currentState);
    }

    public void showDescriptionPanel()
    {
        descriptionPanel.SetActive(true);
    }

    public void showVideoPanel()
    {
        videoPanel.SetActive(true);
    }

    public void hideVideoPanel()
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
