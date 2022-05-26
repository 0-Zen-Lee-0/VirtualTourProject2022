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
    [SerializeField] GameObject slideshowPanel;
    [SerializeField] GameObject descriptionPanel;

    [SerializeField] Button audioButton;
    [SerializeField] Sprite nonMuteSprite;
    [SerializeField] Sprite muteSprite;

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

    public void ShowSlideshowPanel()
    {
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
