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
    public GameObject[] smCampus;
    //Side Menu
    public GameObject[] sideMenus;
    
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

    public void ShowCampusMenu(){

        if (campusMenu.activeSelf)
        {
            Debug.Log("1");
            btnCmenu.image.sprite = upDown[1];
            HideUI();
        }
        else
        {
            Debug.Log("2");
            //show Campus Menu
            campusMenu.SetActive(true);
            //change sprite
            hider.SetActive(true);
            btnCmenu.image.sprite = upDown[1];
        }
    }

    public void HideUI(){
        if(sideMenus[cNo].activeSelf){
            //hide side menu
            sideMenus[cNo].SetActive(false);
            foreach(GameObject specialSite in specialSite)
            {
                specialSite.SetActive(false);
            }
            ReturnToWhite();
            HideBuildingFloorGroups();
            //show smBtn
            smCampus[cNo].SetActive(true);
        }
        if(campusMenu.activeSelf){
            campusMenu.SetActive(false);
            btnCmenu.image.sprite = upDown[0];
        }
        //hide hider
        hider.SetActive(false);

    }
    public void BackToHome(){
        //show start page
        startPage.SetActive(true);
        foreach (GameObject sideMenu in sideMenus)
        {
            sideMenu.SetActive(false);
        }
        //hide hider
        hider.SetActive(false);
    }

    public void ChooseCampus(int campusNumber){
        btnCmenu.image.sprite = upDown[0];
        //hide start page
        startPage.SetActive(false);
        //hide smBtns
        for(int i = 0; i < smCampus.Length; i++)
        {
            smCampus[i].SetActive(false);
        }
        //show smBtn
        smCampus[campusNumber].SetActive(true);
        //hide Campus Menu
        campusMenu.SetActive(false);
        cNo = campusNumber;
        hider.SetActive(false);
    }

    public void ShowSideMenu(int campusNumber){
        HideUI();
        //show side menu
        sideMenus[campusNumber].SetActive(true);
        //show hider
        hider.SetActive(true);
        //hide smBtn
        smCampus[campusNumber].SetActive(false);
    }

    public void ShowFloorButtons(int building){
        if(buildingFloorGroup[building].activeSelf == false)
        {
            HideBuildingFloorGroups();
            buildingFloorGroup[building].SetActive(true);
            hider.SetActive(true);
        }
        HideSpecial();
        RemainSelectedColor(building);
    }
    public void RemainSelectedColor(int building){
        if(building<16)
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
        hider.SetActive(true);
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
    public void HideBuildingFloorGroups(){
        foreach(GameObject buildingFloorGroup in buildingFloorGroup)
        {
            buildingFloorGroup.SetActive(false);
        }
    }
}
