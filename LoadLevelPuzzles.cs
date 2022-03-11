using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadLevelPuzzles : MonoBehaviour
{

    // Method to deserialize a json file 
    // create a LevelPuzzles Object from the JSON representation  	
    public static LevelPuzzles FromJson(string jsonFile)
    {        
        return JsonUtility.FromJson<LevelPuzzles>(jsonFile);
    }    
}

[System.Serializable]
public class LevelPuzzles
{
    public string levelName;
    public List<string> levelPuzzles;
    public List<string> puzzlesAnswers;    

    public LevelPuzzles(string level, List<string> puzzles, List<string> answers)
    {
        levelName = level;
        levelPuzzles = puzzles;
        puzzlesAnswers = answers;
    }
}
