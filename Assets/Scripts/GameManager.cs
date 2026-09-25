using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get ; private set;}

    [SerializeField] private int levelNumberToLoad ;                                   // this variable in serialized field means that level number we want to load , we set our desired level no. we want to load through inspector. 
    [SerializeField] private List<GameLevel> gameLevelList ;


    private int coinScore ;                                                            // Variable to keep track of the player's score, which will be incremented when a coin is picked up.
    private float time; 
    private bool isTimerActive ;

    private void Awake() {
        Instance = this ;
    } 

    private void Start(){

        Lander.Instance.OnCoinPickUp += Lander_OnCoinPickUp ;                    // Subscribe to the OnCoinPickUp event from the Lander script. When the event is triggered, the Lander_OnCoinPickUp method will be called.Lander.instance is just a refference to the Lander script(insted of declaring Lander then drag and drop things), which is a singleton class that manages the lander's behavior and state. 
        Lander.Instance.OnLanded += Lander_OnLanded ;                            // Subscribe to the OnLanded event from the Lander script. When the event is triggered, the Lander_OnLanded method will be called. This allows the GameManager to react to the lander successfully landing on a landing pad and handle scoring or other game logic related to landing.
        Lander.Instance.OnStateChanged += Lander_OnStateChanged ;       

        LoadCurrentLevel() ;                                                     // Call this function at start to load the correct level .
    }
    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e){

        if(e.stateAsEventArg == Lander.State.Normal) {
            isTimerActive = true;  
        } else {
            isTimerActive = false; 
        }
    }
    private void Update(){

        if (isTimerActive == true){
        time += Time.deltaTime ;                        
        }
    }

    private void LoadCurrentLevel(){                                                                       // Our custom function - finds and spawns the right level                                                    

        foreach (GameLevel gameLevel in gameLevelList){                                                    // LOOP logic : Go through every Level_1,2,3... prefab inside gameLevelList one by one. foreach = Loop keyword. Means For each thing inside a list, do this. GameLevel = Type (only GameLevel allowed). gameLevel = temporary variable, holds current prefab in this loop (first Level_1, then Level_2). in = inside. gameLevelList = Menu bag/list containing all level prefabs [Level_1, Level_2 so on...] . GameLevel gameLevel = Blueprint/Each parent prefab level, in unity Project. 

            if (gameLevel.GetLevelNumber() == levelNumberToLoad){                                          // CONDITION logic : if the level no. we want to load is equal to any level no. indide the list then load then load that level which are similar/equal. gameLevel here = current level no. prefab we are checking.

                GameLevel spawnedGameLevel = Instantiate (gameLevel,Vector3.zero,Quaternion.identity) ;    // SPAWN logic: Clone the matching blueprint/level prefab inisde project into the game scene. GameLevel = Type of clone (because we cloned GameLevel prefab).spawnedGameLevel = variable to store the REAL cloned level in Hierarchy (not blueprint in Project). Hence GameLevel spawnedGameLevel = Real copy in game Scene, which is detroyed later when level is completed and next button is clicked. Instantiate() = Unity clone/photocopy/spawn function. gameLevel = WHAT to clone (the matched prefab, e.g. Level_1). Vector3.zero = WHERE to spawn ,the level prefab at (0,0,0) world origin, because level designed at origin.Quaternion.identity = Rotation, means no rotation (0 degrees). 

                Lander.Instance.transform.position = spawnedGameLevel.GetlanderStartPosition();            // LANDER POSITON PLACING logic: Put Lander at this level's flag position that we set visually by moving landrStartPosition game object in game scene. gameLevel.GetLanderStartPosition() = Ask spawned/cloned level in scene "where is your spawn flag?" returns Vector3 of child LanderStartPosition. 
            }
        }
    }

    private void Lander_OnCoinPickUp(object sender, System.EventArgs e){             // This method is called when the OnCoinPickUp event is triggered in the Lander script. It handles the logic for when a coin is picked up.
       AddCoinScore(500) ;                                                           // Call the AddScore method to increment the score by 500 points when a coin is picked up.
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e){              // This method is called when the OnLanded event is triggered in the Lander script. It handles the logic for when the lander successfully lands on a landing pad.
        AddCoinScore(e.finalScore) ;                                                      // Call the AddScore function to get the score of coin(which is 500) then add it with the final score by using 'e.finalScore'(dot adds the 500 with final score) using calculated based on the landing conditions.The argument(finalScore) of event is added to argument of AddCoinScore function through "e." .
    }

    private void AddCoinScore(int addCoinScoreAmount){                                   // This method can be used to add score when a coin is picked up. The addScoreAmount parameter specifies how much score to add.
        coinScore += addCoinScoreAmount ;                                                // Increment the score by the specified amount.
                                                   
    }  

    public int GetScore(){

        return coinScore ;                                                                  // Return the current score. This method can be used to retrieve the player's score for display or other purposes.
    }

    public float GetTime(){

        return time ;
    }

}

