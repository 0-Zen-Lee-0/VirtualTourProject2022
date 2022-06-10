using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //hider
    public GameObject hider;
    
    public int cNo = 0;
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

    // TODO; refactor alongside HideUI, inefficient and too complex
    public void ShowCampusMenu(){

        if (campusMenu.activeSelf)
        {
            btnCmenu.image.sprite = upDown[1];
            HideUI();

            // show bottom left panel
            if (TourManager.Instance.isFirstLocationSphereLoaded)
            {
                UIManager.Instance.showBottomLeftPanel();
            }
        }
        else
        {
            HideUI();
            //show Campus Menu
            campusMenu.SetActive(true);
            //change sprite
            ShowHider();
            btnCmenu.image.sprite = upDown[1];
        }
    }

    public void FirstShowSideMenu(int campusNumber)
    {
        startPage.SetActive(true);
        campusMenu.SetActive(true);
        ShowLeftMenu(campusNumber);
    }

    public void HideUI(){
        if(LeftMenus[cNo].activeSelf){

            HideLeftMenu(cNo);
            foreach(GameObject specialSite in specialSite)
            {
                specialSite.SetActive(false);
            }
            ReturnToWhite();
            HideBuildingFloorGroups();

            ShowLeftMenuBtn(cNo);

            UIManager.Instance.showBottomLeftPanel();
        }
        if(campusMenu.activeSelf){
            campusMenu.SetActive(false);
            btnCmenu.image.sprite = upDown[0];
        }

        HideHider();
    }

    public void BackToHome(){

        startPage.SetActive(true);
        foreach (GameObject leftMenu in LeftMenus)
        {
           leftMenu.SetActive(false);
        }

        HideHider();
        //hide description panel and locator
        UIManager.Instance.DisableDescriptionPanel();
        //show bottom left panel
        UIManager.Instance.showBottomLeftPanel();
    }

    public void ChooseCampus(int campusNumber){
        cNo = campusNumber;
        btnCmenu.image.sprite = upDown[0];
        startPage.SetActive(false);
        HideLeftMenuBtns();
        ShowLeftMenuBtn(campusNumber);
        campusMenu.SetActive(false);
        HideHider();
        ShowLeftMenuBtnBehavior(campusNumber);
    }

    public void ShowLeftMenuBtnBehavior(int campusNumber){
        HideUI();
        UIManager.Instance.hideBottomLeftPanel();
        HideLeftMenuBtns();
        ShowLeftMenu(campusNumber);
        ShowHider();
    }
    
    public void ShowLeftMenuBtn(int campusNumber){
        ShowLeftMenuBtns[campusNumber].SetActive(true);
    }
    public void HideLeftMenuBtns(){
        for(int i = 0; i < ShowLeftMenuBtns.Length; i++)
        {
            ShowLeftMenuBtns[i].SetActive(false);
        }
    }

    public void ShowLeftMenu(int campusNumber){
        LeftMenus[campusNumber].SetActive(true);
    }
    public void HideLeftMenu(int campusNumber){
        LeftMenus[campusNumber].SetActive(false);
    }
    
    public void MainCampusBldgBtnBehavior(int building){
        if(!buildingFloorGroup[building].activeSelf)
        {
            HideBuildingFloorGroups();
            ShowFloorButtons(building);
            ShowHider();
        }
        HideSpecial();
        RemainSelectedColor(building);
    }


    public void ShowFloorButtons(int building){
            buildingFloorGroup[building].SetActive(true);
    }
    public void HideBuildingFloorGroups(){
        foreach(GameObject buildingFloorGroup in buildingFloorGroup)
        {
            buildingFloorGroup.SetActive(false);
        }
    }


    public void RemainSelectedColor(int building){
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
    public void ReturnToWhite(){
        foreach(Button leftPanelButton in leftPanelButton)
        {
            ColorBlock colors = leftPanelButton.colors;
            colors.normalColor = new Color32(255,255,255,255);
            leftPanelButton.colors = colors;
        }
    }
    public void ReturnToWhite(int limitLow, int limitHi){
        for(int i= limitLow; i<=limitHi; i++)
        {
            ColorBlock colors = leftPanelButton[i].colors;
            colors.normalColor = new Color32(255,255,255,255);
            leftPanelButton[i].colors = colors;
        }
    }


    public void ShowSpecial(int floor){
        HideSpecial();
        specialSite[floor].SetActive(true); 
    }
    public void HideSpecial(){
        foreach(GameObject specialSite in specialSite)
        {
            specialSite.SetActive(false);
        }
    }


    public void ShowHider(){
        hider.SetActive(true);
    }
    public void HideHider(){
        hider.SetActive(false);
    }
}
