using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SideUIScript : MonoBehaviour
{

    private Button showWListBtn;
    private Button minimizeBtn;
    private Button showLeaderBoard;
    private Button closeLeaderBoard;
    private Button pauseBtn;
    private Button mainMenuBtn;
    private Button resumeBtn;

    // word list visual element 
    private VisualElement wListVS;
    // pause ui
    private VisualElement pauseUI;

    
    private void OnEnable()
    {
        // Side UI Root
        var root1 = GetComponent<UIDocument>().rootVisualElement;
        
        // Buttom UI Root
        var root2 = GameObject.Find("GameManager").GetComponent<UIDocument>().rootVisualElement;

        showWListBtn = root1.Q<Button>("showWordListBtn");
        wListVS = root2.Q<VisualElement>("wlistVS");
        minimizeBtn = root2.Q<Button>("sideUiCloseBtn");
        showLeaderBoard = root2.Q<Button>("showLeaderBoardBtn");
        closeLeaderBoard = root2.Q<Button>("closeLeaderBoardBtn");
        
        pauseUI = root2.Q<VisualElement>("pauseUI");
        
        pauseBtn = root2.Q<Button>("pauseBtn");
        mainMenuBtn = root2.Q<Button>("mainMenuBtn");
        resumeBtn = root2.Q<Button>("resumeBtn");
                

        showWListBtn.clickable = new Clickable(() =>
        {
            print("is clicked ----");
            ShowWordList();
        });

        minimizeBtn.clickable = new Clickable(() =>
        {
            
            HideWordList();
        });
    }

    private void ShowWordList()
    {
        wListVS.style.height = 200;
    }
    private void HideWordList()
    {
        wListVS.style.height = 0;
    }
}
