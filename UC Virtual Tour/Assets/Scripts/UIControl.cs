using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    //Start Page
    public GameObject startPage;
    //Campus Button Menu
    public GameObject campusMenu;
    //up-down button
    public Button btnCmenu;
    public Sprite[] upDown;
    //Campus
    public GameObject[] smCampus;
    //Side Menu
    public GameObject[] sideMenus;
    //hider
    public GameObject hider;
    public int cNo = 0;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCampusMenu(){
        if(campusMenu.activeSelf == false){
            HideUI();
            //show Campus Menu
            campusMenu.SetActive(true);
            //change sprite
            btnCmenu.image.sprite = upDown[1];
            hider.SetActive(true);
        }
        else{
            HideUI();
            btnCmenu.image.sprite = upDown[0];
        }
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
        //show side menu
        sideMenus[campusNumber].SetActive(true);
        //show hider
        hider.SetActive(true);
        //hide smBtn
        smCampus[campusNumber].SetActive(false);
    }

    public void HideUI(){
        if(sideMenus[cNo].activeSelf == true){
            //hide side menu
            sideMenus[cNo].SetActive(false);
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
}
