using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {

    private Rigidbody2D LanderRigidbody2D ;

    private void Awake() {

        LanderRigidbody2D = GetComponent<Rigidbody2D>() ;
    }
    private void FixedUpdate(){
        if (Keyboard.current.upArrowKey.isPressed ){
            float force = 700f;
            LanderRigidbody2D.AddForce(force * transform.up * Time.deltaTime);
        }
    
    }

}
    



