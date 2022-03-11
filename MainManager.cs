using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    private static int points;
    public static MainManager Instance;
    private static string username;
    

    private void Awake()
    {	  	   	        
    	if (Instance != null)
    	{
        	Destroy(gameObject);
	        return;
	}	

    	Instance = this;
    	DontDestroyOnLoad(gameObject);

    }
    
    
    public void SetUsername(string val){
    	username = val;
    }
    public string GetUsername(){
    	return username;
    }
    
    public void SetPoints(int val){
    	points += val;
    }
    public int GetTotalPoints(){
    	return points;
    }
    
    
    

    
    
    
    
    
    
    
    
    
    
}
