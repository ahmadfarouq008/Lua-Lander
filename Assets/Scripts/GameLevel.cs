using UnityEngine;

public class GameLevel : MonoBehaviour{                            // this script is attached to all the level_1,2,3.. prefabs as parent script so all each levels will have their own script with their seperate identification of level no. and lander start position.

    [SerializeField] int levelNumber ;                            // Level_1 prefab says levelNumber = 1, Level_2 says levelNumber = 2 and so on. This is self-identification.e.g. I am Level 1 and my spawn flag is this child (below line code).Its actually just a label of level no. on every level's prefab. 
    [SerializeField] Transform landerStartPositionTransform ;     // Each level needs its OWN spawn position. That's why this field exists.Invisible flag you can move visually to set the spawn position.The landerStartPosition child of level_1,2,3.. prefab is dragged in this serialized field for refference. 
   
    public int GetLevelNumber() {
        return levelNumber ;
    } 

    public Vector3 GetlanderStartPosition(){                      // Returns Vector3 = three numbers (x,y,z) or position of lander . This is where Lander should spawn.we used Vector 3 here because we only want the position to be accessed/returned, not the whole Transform, so they can't accidentally move the flag.

        return landerStartPositionTransform.position ;            // if we use Tranform as returning output then we use here .Tranform, but we only want .position in the end of code. Tansform is a class which includes Position, rotation and scale of an object.since we only want the position getter function.
    }
    
}