using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    
    public GameObject startPage;
    public GameObject campusMenu;
    //up-down button
    public Button btnCmenu;
    public Sprite[] upDown;
    //Campus
    public GameObject[] smCampus;
    //Side Menu
    public GameObject[] sideMenus;
    
    public Button[] mainBuilding;
    public GameObject[] buildingFloorGroup;
    public GameObject[] buildingFloor;
    public GameObject[] specialSite;

    //hider
    public GameObject hider;
    
    public int cNo = 0;
    bool inMain;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCampusMenu(){
        if(btnCmenu.image.sprite == upDown[0]){
            HideUI();
            //show Campus Menu
            campusMenu.SetActive(true);
            //change sprite
            btnCmenu.image.sprite = upDown[1];
            hider.SetActive(true);
        }
        else{
            HideUI();
        }
    }

    public void HideUI(){
        if(sideMenus[cNo].activeSelf == true){
            //hide side menu
            sideMenus[cNo].SetActive(false);
            foreach(GameObject specialSite in specialSite)
            {
                specialSite.SetActive(false);
            }
            ReturnToWhite();
            //show smBtn
            smCampus[cNo].SetActive(true);
        }
        if(campusMenu.activeSelf == true){
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
        if(buildingFloorGroup[building].activeSelf == false){
            ReturnToWhite();
            buildingFloorGroup[building].SetActive(true);
            hider.SetActive(true);
        }
    }
    public void RemainSelectedColor(int building){
        ColorBlock colors = mainBuilding[building].colors;
        colors.normalColor = new Color32(8,105,60,255);
        mainBuilding[building].colors = colors;
        hider.SetActive(true);
    }
    public void ReturnToWhite(){
        for(int i =0; i < buildingFloorGroup.Length; i++)
            {
                buildingFloorGroup[i].SetActive(false);
                ColorBlock colors = mainBuilding[i].colors;
                colors.normalColor = new Color32(255,255,255,255);
                mainBuilding[i].colors = colors;
            }
    }
    public void ShowSpecial(int floor){
        foreach(GameObject specialSite in specialSite)
        {
            specialSite.SetActive(false);
        }
        specialSite[floor].SetActive(true); 
        
    }
}
