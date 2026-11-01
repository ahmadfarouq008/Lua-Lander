using UnityEngine.UI;
using UnityEngine;


public class PausedUI : MonoBehaviour {

    [SerializeField] private Button resumeButton ;                           // this is a reference to the resume button in the UI. It is used to resume the game when clicked.

    private void Awake() {                                                
        resumeButton.onClick.AddListener( () => {                            // this is an anonymous function that is called when the resume button is clicked. It calls the UnPauseGame() method in the GameManager class to resume the game.
            GameManager.Instance.UnPauseGame() ;
            
        }) ;
    } 

    private void Start(){                                                 
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused ;             // this is an event that is called when the game is paused. It calls the Show() method to show the paused UI.
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnPaused ;        // this is an event that is called when the game is unpaused. It calls the Hide() method to hide the paused UI.

        Hide() ;                                                                   // this hides the paused UI when the game starts. It is called in Start() because we want to hide the paused UI when the game starts, not in Awake() because we want to show the paused UI when the game is paused.
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e){      // this is an event that is called when the game is paused. It calls the Show() method to show the paused UI.
        Show() ;
    }
    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e){    // this is an event that is called when the game is unpaused. It calls the Hide() method to hide the paused UI.
        Hide() ;
    }

    private void Show() {  
        gameObject.SetActive(true) ;
    }

    
    private void Hide() {
        gameObject.SetActive(false) ;
    }

}
