using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// class for managing the ui of navigation buttons
public class UIControl : MonoBehaviour
{
    public static UIControl Instance {get; private set;}
    
    public GameObject startPage;
    public GameObject campusMenu;
    //up-down button
    public Button btnCmenu;
    public Sprite[] upDown;
    //Campus
    public GameObject[] ShowLeftMenuBtns;
    //Side Menu
    public GameObject[] LeftMenus;
    
    public Button[] leftPanelButton;
    public GameObject[] buildingFloorGroup;
    public GameObject[] specialSite;

    [SerializeField] Button creditsButton;

    //hider
    public GameObject hider;
    
    public int cNo = 0;
    CampusData campusData;
    bool inMain;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void FirstShowSideMenu(int campusNumber)
    {
        ShowStartPage();
        ShowCampusMenu();
        ShowLeftMenu(campusNumber);
    }
        // TODO; refactor alongside HideUI, inefficient and too complex
    public void ShowCampusMenuBtnBehavior()
    {
        if (campusMenu.activeSelf)
        {
            HideUI();
            btnCmenu.image.sprite = upDown[0];
            // show bottom left panel
            if (TourManager.Instance.isFirstLocationSphereLoaded)
            {
                UIManager.Instance.showBottomLeftPanel();
            }
        }
        else
        {
            HideUI();
            ShowCampusMenu();
            ShowHider();
            btnCmenu.image.sprite = upDown[1];
        }
        
    }
    public void HideUI()
    {
        if(LeftMenus[cNo].activeSelf)
        {
            HideCreditsButton();
            HideLeftMenu();
            HideSpecial();
            ReturnToWhite();
            HideBuildingFloorGroups();
            //Fix This
            ShowLeftMenuBtn(cNo);

            UIManager.Instance.showBottomLeftPanel();
        }
        if(campusMenu.activeSelf)
        {
            HideCampusMenu();
            btnCmenu.image.sprite = upDown[0];
        }

        HideHider();
    }

    public void ChooseCampus(int campusNumber)
    {
        cNo = campusNumber;
        btnCmenu.image.sprite = upDown[0];
        HideStartPage();
        HideLeftMenuBtns();
        ShowLeftMenuBtn(campusNumber);
        HideCampusMenu();
        HideHider();
        ShowLeftMenuBtnBehavior(campusNumber);
        UIManager.Instance.showRightButtonsPanel();
    }
    public void BackToHome(){
        TourManager.Instance.LoadInitialSite();

        ShowStartPage();
        HideLeftMenu();
        HideHider();
        UIManager.Instance.DisableDescriptionPanel();
        UIManager.Instance.hideRightButtonsPanel();
        UIManager.Instance.showBottomLeftPanel();
    }

    public void HideStartPage(){
        startPage.SetActive(false);
    }
    public void ShowStartPage(){
        startPage.SetActive(true);
    }

    public void ShowCampusMenu()
    {
        campusMenu.SetActive(true);
    }
    public void HideCampusMenu()
    {
        campusMenu.SetActive(false);
    }

    public void ShowLeftMenuBtnBehavior(int campusNumber)
    {
        HideUI();
        UIManager.Instance.hideBottomLeftPanel();
        HideLeftMenuBtns();
        ShowLeftMenu(campusNumber);
        ShowCreditsButton();
        ShowHider();
    }
    
    void ShowCreditsButton()
    {
        creditsButton.gameObject.SetActive(true);
    }

    void HideCreditsButton()
    {
        creditsButton.gameObject.SetActive(false);
    }
    
    public void ShowLeftMenuBtn(int campusNumber)
    {
        ShowLeftMenuBtns[campusNumber].SetActive(true);
    }
    public void HideLeftMenuBtns()
    {
        for(int i = 0; i < ShowLeftMenuBtns.Length; i++)
        {
            ShowLeftMenuBtns[i].SetActive(false);
        }
    }

    public void ShowLeftMenu(int campusNumber)
    {
        LeftMenus[campusNumber].SetActive(true);
    }
    public void HideLeftMenu()
    {
        foreach(GameObject leftMenu in LeftMenus)
        {
            leftMenu.SetActive(false);
        }
    }
    
    public void MainCampusBldgBtnBehavior(int building)
    {
        if(!buildingFloorGroup[building].activeSelf)
        {
            HideBuildingFloorGroups();
            ShowFloorButtons(building);
            ShowHider();
        }
        HideSpecial();
        RemainSelectedColor(building);
    }

    public void ShowFloorButtons(int building)
    {
            buildingFloorGroup[building].SetActive(true);
    }
    public void HideBuildingFloorGroups()
    {
        foreach(GameObject buildingFloorGroup in buildingFloorGroup)
        {
            buildingFloorGroup.SetActive(false);
        }
    }

    public void RemainSelectedColor(int building)
    {
        if(building<15 || building>52)
        {
            ReturnToWhite();
        }
        switch (building) 
        {
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:
                ReturnToWhite(15,20);
                break;
        }
        ColorBlock colors = leftPanelButton[building].colors;
        colors.normalColor = new Color32(8,105,60,255);
        leftPanelButton[building].colors = colors;
        ShowHider();
    }
    public void ReturnToWhite()
    {
        foreach(Button leftPanelButton in leftPanelButton)
        {
            ColorBlock colors = leftPanelButton.colors;
            colors.normalColor = new Color32(255,255,255,255);
            leftPanelButton.colors = colors;
        }
    }
    public void ReturnToWhite(int limitLow, int limitHi)
    {
        for(int i= limitLow; i<=limitHi; i++)
        {
            ColorBlock colors = leftPanelButton[i].colors;
            colors.normalColor = new Color32(255,255,255,255);
            leftPanelButton[i].colors = colors;
        }
    }

    public void ShowSpecial(int floor)
    {
        HideSpecial();
        specialSite[floor].SetActive(true); 
    }
    public void HideSpecial()
    {
        foreach(GameObject specialSite in specialSite)
        {
            specialSite.SetActive(false);
        }
    }

    public void ShowHider()
    {
        hider.SetActive(true);
    }
    public void HideHider()
    {
        hider.SetActive(false);
    }
}
