using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {                      // static class = no need to attach to GameObject, no Instance needed - global loader

    public enum Scene {                                // Enum list of all scenes - names MUST match scene file names exactly - trick for ToString()
        MainMenu,                                      // Enum value 0 - matches Scenes/MainMenuScene.unity
        GameScene,                                     // Enum value 1 - matches Scenes/GameScene.unity
    }
    
    public static void LoadScene (Scene scene ){       // Public static function - can call from anywhere like MainMenuUI, GameManager - takes enum

        SceneManager.LoadScene(scene.ToString() ) ;    // ToString() converts enum to string - Scene.GameScene.ToString() = "GameScene" - SceneManager loads scene by name - looks in Build Profiles Scene List
    }
}
