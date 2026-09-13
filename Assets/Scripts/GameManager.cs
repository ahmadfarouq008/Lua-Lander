using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour {

    int Score = 0 ;                                                        // Variable to keep track of the player's score, which will be incremented when a coin is picked up.

    private void Start(){

        Lander.Instance.OnCoinPickUp += Lander_OnCoinPickUp ;              // Subscribe to the OnCoinPickUp event from the Lander script. When the event is triggered, the Lander_OnCoinPickUp method will be called.Lander.instance is just a refference to the Lander script(insted of declaring Lander then drag and drop things), which is a singleton class that manages the lander's behavior and state. 
    }

    private void Lander_OnCoinPickUp(object sender, System.EventArgs e){   // This method is called when the OnCoinPickUp event is triggered in the Lander script. It handles the logic for when a coin is picked up.
       AddScore(500) ;                                                     // Call the AddScore method to increment the score by 500 points when a coin is picked up.
    }

    private void AddScore(int addScoreAmount){                             // This method can be used to add score when a coin is picked up. The addScoreAmount parameter specifies how much score to add.
        Score += addScoreAmount ;                                          // Increment the score by the specified amount.
        Debug.Log("Score: " + Score) ;                                     // Log the updated score to the console for debugging purposes.
    }





}

