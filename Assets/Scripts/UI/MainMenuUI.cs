using UnityEngine; 
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour { 
    [SerializeField] private Button playButton;                         // Play button reference - drag in Inspector - SerializeField makes private visible in Inspector
    [SerializeField] private Button quitButton;                         // Quit button reference

    private void Awake() {                                              // Awake() runs when MainMenu scene loads - before Start - setup listeners here
        playButton.onClick.AddListener(() => {                          // Add lambda listener to Play Button onClick event - when player clicks Play
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);         // Call static SceneLoader.LoadScene() inside SceneLoader.cs - pass enum GameScene - this will load GameScene
        }); 

        quitButton.onClick.AddListener(() => {                         // Add listener to Quit Button
            Application.Quit();                                        // Quit the application - works in build, ignored in Editor
        }); 
    } 
}
