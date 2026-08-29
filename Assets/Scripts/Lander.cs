using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {

    private Rigidbody2D LanderRigidbody2D ;

    private void Awake() {
        
        LanderRigidbody2D = GetComponent<Rigidbody2D>() ;
    }    
    
}
    



