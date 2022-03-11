using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviour
{
    private TextField username;
    private TextField password;
    private TextField username1;
    private TextField password1;
    private Button loginBtn;
    private Button registerBtn;
    private Button signUpBtn;        
    private Label loginErrorLabel;
    
    private VisualElement signUpPanel;
    
    private WWWForm form;
    
    private void OnEnable(){
    
    	// Login Ui root
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        username = root.Q<TextField>("username");
        password = root.Q<TextField>("password");
        username1 = root.Q<TextField>("username1");
        password1 = root.Q<TextField>("password1");
        
        loginBtn = root.Q<Button>("signInBtn");
        registerBtn = root.Q<Button>("registerBtn");
        signUpBtn = root.Q<Button>("createBtn");
        loginErrorLabel = root.Q<Label>("loginErrorLabel");
        
        signUpPanel = root.Q<VisualElement>("SignUpPanel");
        signUpPanel.visible = false;
        
        loginBtn.clickable = new Clickable(() =>
        {
            //StartCoroutine(Login());
            GoToLevel1();
        });
        
        // Register button event (show sign up panel)
        registerBtn.clickable = new Clickable(() =>
        {
            signUpPanel.visible = true;
        });
        
        // Sign up
        signUpBtn.clickable = new Clickable(() =>
        {
            StartCoroutine(SignUp());
        });
    }
    
    // Login logic
    IEnumerator Login(){
    	
    	form = new WWWForm ();

	form.AddField ("username", username.text);
	form.AddField ("password", password.text);
	
	var webR = UnityWebRequest.Post("http://localhost:8089/unity/", form);

        // Wait until the download is done
        yield return webR.SendWebRequest();
        
        if (webR.result != UnityWebRequest.Result.Success)
        {
            print( "Error downloading: " + webR.error );
            loginErrorLabel.text=webR.error;
            loginErrorLabel.style.color=Color.red;
        }
        else{
        	Debug.Log(webR.downloadHandler.text);
        	GoToLevel1();
        	MainManager.Instance.SetUsername(username.text);
        }    	
    }
    
    // Sign up logic
    IEnumerator SignUp(){
    	
    	form = new WWWForm ();

	form.AddField ("username", username1.text);
	form.AddField ("password", password1.text);
	
	var webR = UnityWebRequest.Post("http://localhost:8089/unity/createUser.php", form);

        // Wait until the download is done
        yield return webR.SendWebRequest();
        
        if (webR.result != UnityWebRequest.Result.Success)
        {
            print( "Error downloading: " + webR.error );
            loginErrorLabel.text=webR.error;
            loginErrorLabel.style.color=Color.red;
        }
        else{
        	Debug.Log(webR.downloadHandler.text);
        }    	
    }
  
    // Start Level1
    private void GoToLevel1(){
   	SceneManager.LoadScene("Level1");    	
    }    
}
