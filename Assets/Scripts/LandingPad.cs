using UnityEngine;

public class LandingPad : MonoBehaviour {
    [SerializeField] private int scoreMultiplier ; // Set multiplier value inside inspector. This script is attached to each landing pad in your scene. This multiplier just holds how much this pad is worth as we set inside inpector.
    public int GetScoreMultiplier(){    // this function is written here because lander dont have multiplier value, but landing pad has multiplier value, so we need to get that value from landing pad and use it in lander.cs file. So we write this function here in landing pad script and call it in lander.cs file to get the multiplier value of that specific landing pad. 

        return scoreMultiplier ;

    }

    
}
