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

    
    private void Update(){
        
        cinemachineCamera.Lens.OrthographicSize = targetOrthographicSize ;                          // Apply current target size to real camera - Lens.OrthographicSize controls zoom, low=zoom in, high=zoom out
    }

    
    
    public void SetTargetOrthographicSize(float targetOrthographicSizeParameterasZoomedOutInput){   // Public method to set any zoom size from inspector - called from GameManager when level spawns.Here functions Parameter comes from GameManager -> GameManager gets it from GetZoomedOutOrthographicSize() of GameLevel.cs -> which you set in prefab Level_1,2,3 Inspector.

       
        targetOrthographicSize = targetOrthographicSizeParameterasZoomedOutInput ;                  // Set current zoom to the incoming value - e.g. 30 for big level overview.
    }

    
    public void SetNormalOrthographicSize(){                                                        // Function to zoom back to normal gameplay when game starts
        
        targetOrthographicSize = NORMAL_ORTHOGRAPHIC_SIZE ;                                         // Reset current zoom to normal constant 10f
    }
}