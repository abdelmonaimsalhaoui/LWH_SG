using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;



public class leaderBoardScript : MonoBehaviour
{
    private WWWForm form;
    private ListView leaderboardListView;
    
    private void OnEnable(){
    
    	// Ui root element
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        leaderboardListView = root.Q<ListView>("leaderboardList");
        
        StartCoroutine(getLeaderBoard());               
    }
    
    // Logic to get the Leaderboard data from the database 
    // and to show the data in a Listview element
    
    IEnumerator getLeaderBoard(){
    	
    	form = new WWWForm ();
	
	var webR = UnityWebRequest.Post("http://localhost:8089/unity/leaderboard.php", form);

        // Wait until the download is done
        yield return webR.SendWebRequest();
        
        if (webR.result != UnityWebRequest.Result.Success)
        {
            print( "Error : " + webR.error );
            
        }
        else{
	        Debug.Log(webR.downloadHandler);	        
        	string results = webR.downloadHandler.text;
        	
        	// Deserializing the result
        	Wrapper leadB = LeaderBoardInfo.CreateFromJSON(results);        	
        	        	
        	       	
		Func<VisualElement> makeItem = () => new Label();
		Action<VisualElement, int> bindItem = (e, i) => {
		
		Label playerNameLabel = new Label();
		playerNameLabel.style.width = Length.Percent(50);
        	playerNameLabel.text = leadB.items[i].playerName;
        	
        	Label playerPointsLabel = new Label(); 		        	    	
        	playerPointsLabel.text = leadB.items[i].playerPoints;

		(e as VisualElement).style.flexDirection = FlexDirection.Row;
		(e as VisualElement).style.backgroundColor = new Color(1,0,0,0.5f);				
		(e as VisualElement).Add(playerNameLabel); 
		(e as VisualElement).Add(playerPointsLabel); 		
		};
		  
		leaderboardListView.itemsSource = leadB.items;
		leaderboardListView.bindItem = bindItem;
		leaderboardListView.makeItem = makeItem;
		leaderboardListView.Rebuild();    	
        }
    	
    }
}

[Serializable]
public class LeaderBoardInfo
{
    public string playerName;
    public string playerPoints;

    public static Wrapper CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Wrapper>("{\"items\":" + jsonString + "}");
    }
    
}

[Serializable]
public class Wrapper
{
	public LeaderBoardInfo[] items;
}
