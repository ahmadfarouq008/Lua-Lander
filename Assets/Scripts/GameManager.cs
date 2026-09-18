using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get ; private set;}

    private int coinScore ;                                                            // Variable to keep track of the player's score, which will be incremented when a coin is picked up.
    private float time; 

    private void Awake() {
        Instance = this ;
    } 

    private void Start(){

        Lander.Instance.OnCoinPickUp += Lander_OnCoinPickUp ;                    // Subscribe to the OnCoinPickUp event from the Lander script. When the event is triggered, the Lander_OnCoinPickUp method will be called.Lander.instance is just a refference to the Lander script(insted of declaring Lander then drag and drop things), which is a singleton class that manages the lander's behavior and state. 
        Lander.Instance.OnLanded += Lander_OnLanded ;                            // Subscribe to the OnLanded event from the Lander script. When the event is triggered, the Lander_OnLanded method will be called. This allows the GameManager to react to the lander successfully landing on a landing pad and handle scoring or other game logic related to landing.
    }
    private void Update(){
        time += Time.deltaTime ;
    }

    private void Lander_OnCoinPickUp(object sender, System.EventArgs e){         // This method is called when the OnCoinPickUp event is triggered in the Lander script. It handles the logic for when a coin is picked up.
       AddCoinScore(500) ;                                                           // Call the AddScore method to increment the score by 500 points when a coin is picked up.
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e){     // This method is called when the OnLanded event is triggered in the Lander script. It handles the logic for when the lander successfully lands on a landing pad.
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

