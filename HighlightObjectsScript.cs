using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HighlightObjectsScript : MonoBehaviour
{

    private string levelName;
    private LevelPuzzles puzzles;
    
    private Label label1;
    private Button soundBtn;
    private AudioSource audioSource;

    private Label qLabel;
    private Label checkLabel;
    private Button checkBtn;
    private Label rLabel;
    private TextField txtF;
    
    private bool toContinue;

    private  IReadOnlyCollection<String> levelQuestions;
    private  IReadOnlyCollection<String> levelAnswers;
    
    private  IReadOnlyCollection<String> currentLevelQuestions;
    private  IReadOnlyCollection<String> currentLevelAnswers;
    
    private static int index;

    private Button addToListBtn;
    private ListView wordsListView;
    private static List<String> wordsList;
    
    private ProgressBar pBar;
    private float countPoints;
    
    private Label loggedUser;
    
    private Foldout foldout1;
    private string wordListTitle;
       
    

    private void Awake()
    {    	   
        audioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
        levelName = SceneManager.GetActiveScene().name;                     

	// Loading Level Puzzles		
	loadPuzzles();
	
	// Storing the level Puzzles (Questions) and their corresponding Answers
	levelQuestions = getCurrentLevelQuestions();
	levelAnswers = getCurrentLevelAnswers();
        
    }

    private void OnEnable()
    {
        wordsList = new List<String>();
        
        // Buttom UI root Element
        var root = GameObject.Find("GameManager").GetComponent<UIDocument>().rootVisualElement;
        
        // Side UI root Element
        var root2 = GameObject.Find("SideUi").GetComponent<UIDocument>().rootVisualElement;
        
        label1 = root.Q<Label>("label1P2");
        soundBtn = root.Q<Button>("soundBtn");

        addToListBtn = root.Q<Button>("addToBookBtn");
	
        wordsListView = new ListView();

        qLabel = root.Q<Label>("questionLabel");
        txtF = root.Q<TextField>("answerField");
        checkBtn = root.Q<Button>("checkBtn");
        checkLabel = root.Q<Label>("checkLabel");

        qLabel.text = levelQuestions.ElementAt(index);
        
        loggedUser = root.Q<Label>("loggedUser");
        if(MainManager.Instance!=null){
        	//loggedUser.text = "Hi, "+ MainManager.Instance.GetUsername();
        }
        
        soundBtn.visible = false;
        addToListBtn.visible = false;
        
        // When clicked, the player can hear the correct spelling of the german word
        soundBtn.clickable = new Clickable(() =>
        {
            PlayWordClip(label1.text);            
        });

	// Check the answer (right or false)
        checkBtn.clickable = new Clickable(() =>
        {
            LevelPuzzle();
            if (toContinue)
            {
                qLabel.text = levelQuestions.ElementAt(index);
                txtF.value = "";
                StartCoroutine(WaitALittleBit());
            }
        });

        addToListBtn.clickable = new Clickable(() =>
        {
            AddWordToList(label1.text);
            SaveWord();
        });
        
        
        // ProgressBar logic        
        pBar = root2.Q<ProgressBar>("myProgressBar");
        
        // Foldout (Word List)
        foldout1 = root.Q<Foldout>("myFoldout");
        
        
        
    }
    
    // When an object is clicked
    private void OnMouseDown()
    {
        label1.text = gameObject.name;
        soundBtn.visible = true;
        addToListBtn.visible = true;
    }

    // Hover an object mouse effect
    private void OnMouseEnter()
    {
        if (gameObject.name.Equals("Die Tür"))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.5f, 0.5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void PlayWordClip(String word)
    {
        var audioClip = Resources.Load<AudioClip>("Sounds/WordsSounds/"+word);
        audioSource.PlayOneShot(audioClip);
    }

    // Method to check and compare puzzles with the answers
    private void LevelPuzzle()
    {

        if (txtF.text!=null && txtF.text.Equals(label1.text) && txtF.text.Equals(levelAnswers.ElementAt(index)))
        {
            checkLabel.text = "Richtig !";
            checkLabel.style.color = Color.blue;
            index++;
            toContinue = true;
            countPoints+=(100/15);
            pBar.value = countPoints;
            pBar.title = "+ "+Math.Round(countPoints);
        }
        else
        {
            checkLabel.text = "Falsch! Versuch noch einmal !";
            checkLabel.style.color = Color.red;
            toContinue = false;
        }
    }

    IEnumerator WaitALittleBit()
    {
        yield return new WaitForSeconds(2f);
        checkLabel.text = "Try the next one !";
    }

    // Method to rebuild/refresh the wordListView when a word is added to the wordList
    private void SaveWord()
    {
    	foldout1.text = wordListTitle;

        Func<VisualElement> makeItem = () => new Label();
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = wordsList[i];

        wordsListView.itemsSource = wordsList;
        wordsListView.bindItem = bindItem;
        wordsListView.makeItem = makeItem;
        wordsListView.Rebuild();
        foldout1.Add(wordsListView);
    }
    
    // Method to add a word to the wordList
    private void AddWordToList(String word)
    {
        
        if (word != "" && !wordsList.Exists(e=>e==word) )
        {
            wordsList.Add(word);
        }
    }
    
    // Method to load the current Level puzzles
    private IReadOnlyCollection<String> getCurrentLevelQuestions(){
        	
    	if(puzzles!=null && puzzles.levelName.Equals("Level1")){
		wordListTitle = "Im Klassenzimmer";
    		// Loading Level 1 Puzzles 
        	currentLevelQuestions = puzzles.levelPuzzles;
    	}
    	if(puzzles!=null && puzzles.levelName.Equals("Level2")){
		wordListTitle = "Im Klassenzimmer";
    		// Loading Level 1 Puzzles 
        	currentLevelQuestions = puzzles.levelPuzzles;
    	}
    	if(puzzles!=null && puzzles.levelName.Equals("Level3")){
		wordListTitle = "Im Klassenzimmer";
    		// Loading Level 1 Puzzles 
        	currentLevelQuestions = puzzles.levelPuzzles;
    	}    	
    	return currentLevelQuestions;
    }
    // Method to load the current Level puzzles answers    
    private IReadOnlyCollection<String> getCurrentLevelAnswers(){

    	if(puzzles!=null && puzzles.levelName.Equals("Level1")){
        	// Loading Level 1 Puzzles Answers
        	currentLevelAnswers = puzzles.puzzlesAnswers;
    	}
    	if(puzzles!=null && puzzles.levelName.Equals("Level2")){
        	// Loading Level 2 Puzzles Answers
        	currentLevelAnswers = puzzles.puzzlesAnswers;
    	}
    	if(puzzles!=null && puzzles.levelName.Equals("Level3")){
        	// Loading Level 3 Puzzles Answers
        	currentLevelAnswers = puzzles.puzzlesAnswers;
    	}    	
    	return currentLevelAnswers;
    }
    
    // Method to load and deserialize the json file
    // each Level has a json file containing the levelName,levelPuzzles and puzzlesAnswers
    private void loadPuzzles()
    {        
        var level = Resources.Load<TextAsset>("Puzzles/"+levelName);
        puzzles = LoadLevelPuzzles.FromJson(level.text);               
    }
}
