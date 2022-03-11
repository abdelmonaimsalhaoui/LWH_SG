using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameUIScript : MonoBehaviour
{
   private Button startBtn;
   private Button tutoBtn;
   private Button settingsBtn;
   private Button backBtn;
   private Button backBtn2; 
   private Button quitBtn;   

   
   private void OnEnable(){
   	
   	// Game Ui root
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        // Tuto UI root
        var root2 = GameObject.Find("GameTutoUI").GetComponent<UIDocument>().rootVisualElement;
        
        startBtn = root.Q<Button>("startBtn");        
        tutoBtn = root.Q<Button>("tutoBtn"); 
        settingsBtn = root.Q<Button>("settingsBtn");        
        backBtn = root2.Q<Button>("backBtn");
        backBtn = root2.Q<Button>("backBtn2"); 
        quitBtn = root2.Q<Button>("quitBtn"); 
                 
        // Go to Login Scene
        startBtn.clickable = new Clickable(() =>
        {
            GoToLoginScene();
        });
        
        // Go to Game Tuto UI
        tutoBtn.clickable = new Clickable(() =>
        {
            GetComponent<UIDocument>().sortingOrder = 0;
            GameObject.Find("GameTutoUI").GetComponent<UIDocument>().sortingOrder = 1;
        });
        
        // Go to Game Settings UI
        settingsBtn.clickable = new Clickable(() =>
        {
            GetComponent<UIDocument>().sortingOrder = 0;
            GameObject.Find("GameTutoUI").GetComponent<UIDocument>().sortingOrder = 0;
            GameObject.Find("SettingsUI").GetComponent<UIDocument>().sortingOrder = 1;
        });
        
        // Back to Main Menu (from tuto ui)
        backBtn.clickable = new Clickable(() =>
        {
            GetComponent<UIDocument>().sortingOrder = 1;
            GameObject.Find("GameTutoUI").GetComponent<UIDocument>().sortingOrder = 0;
        });
        // Back to Main Menu (from settings ui)
        backBtn2.clickable = new Clickable(() =>
        {
            GetComponent<UIDocument>().sortingOrder = 1;
            GameObject.Find("SettingsUI").GetComponent<UIDocument>().sortingOrder = 0;
        });
        // Quit the Game
        quitBtn.clickable = new Clickable(() =>
        {
            QuitTheGame();
        });
   }   
   
   private void GoToLoginScene(){
   	SceneManager.LoadScene("LoginUI");
   }
   
   private void QuitTheGame(){
   	Application.Quit();
   }     
}
