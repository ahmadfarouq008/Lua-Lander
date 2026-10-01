using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Rendering;


public class CinemachineCameraZoom2D : MonoBehaviour {                                     
    
   
    public static CinemachineCameraZoom2D Instance { get; private set; }                             // Singleton - global access point, so GameManager can call CinemachineCameraZoom2D.Instance.Set...

    
    private void Awake() {
        
        Instance = this;
    }
   
    [SerializeField] private CinemachineCamera cinemachineCamera;                                    // Reference to Cinemachine Camera - drag in Inspector, we change its Lens.OrthographicSize

    private const float NORMAL_ORTHOGRAPHIC_SIZE = 10f;                                              // Just const - normal gameplay zoom = 10f, same for all levels, never changes, all caps for const naming variation.


    
    private float targetOrthographicSize = NORMAL_ORTHOGRAPHIC_SIZE ;                                // Current zoom we want - starts at NORMAL (10) - this variable CHANGES to 30/40/50 according to level area size for overview, then back to 10

    
    private void Update(){                                                                           // This runs every frame, like 60 times per second
        float zoomSpeed = 2f ;                                                                       // How fast the zoom animation should be - 2 is smooth slow, bigger number is faster

        cinemachineCamera.Lens.OrthographicSize =                                                                     // How fast to zoom in/out - higher = faster, lower = slower. This is just a constant value we set here, not a variable to change in Inspector.
            Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);  // cinemachineCamera.Lens.OrthographicSize = Start from where camera zoom is RIGHT NOW , targetOrthographicSize = Go towards where we WANT it to be - 10 for close, 30 for far overview ,  Time.deltaTime * zoomSpeed = Move only a tiny step each frame, so it looks smooth not jumpy. Time.deltaTime makes it same speed on all PCs
                                                                                                                      // Lerp = "Take current, take target, move a little bit from current towards target". we do not make current equal target ( because of instant pop), but current = current + little step to target every frame.So zoom goes 30... 28... 26... 24... slowly to 10 - smooth animation
        }
    
    public void SetTargetOrthographicSize(float targetOrthographicSizeParameterasZoomedOutInput){   // Public method to set any zoom size from inspector - called from GameManager when level spawns.Here functions Parameter comes from GameManager -> GameManager gets it from GetZoomedOutOrthographicSize() of GameLevel.cs -> which you set in prefab Level_1,2,3 Inspector.

       
        targetOrthographicSize = targetOrthographicSizeParameterasZoomedOutInput ;                  // Set current zoom to the incoming value - e.g. 30 for big level overview.
    }

    
    public void SetNormalOrthographicSize(){                                                        // Function to zoom back to normal gameplay when game starts
        
        targetOrthographicSize = NORMAL_ORTHOGRAPHIC_SIZE ;                                         // Reset current zoom to normal constant 10f
    }
}