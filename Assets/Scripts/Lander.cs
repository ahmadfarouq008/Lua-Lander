using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {

    private Rigidbody2D LanderRigidbody2D ;

    private void Awake() {

        LanderRigidbody2D = GetComponent<Rigidbody2D>() ;
    }
    private void FixedUpdate(){
        if (Keyboard.current.upArrowKey.isPressed ){

            float force = 700f ;
            LanderRigidbody2D.AddForce(force * transform.up * Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed){

            float turnSpeed = -100f ;
            LanderRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
        }
        if (Keyboard.current.leftArrowKey.isPressed){

            float turnSpeed = +100f ;
            LanderRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        // now in this space od code line, we get the argument input(speed/hit data), when lander falls and this function is invoked automatically by untiy and argument input (that we got in unity) is stored in collision(parameter) as writen in below line "if" code. 
        float softLandingVelocityMagnitude = 4f ;
        if (collision.relativeVelocity.magnitude > softLandingVelocityMagnitude ){
        Debug.Log("Landed too hard!");
        return ;
        }       
        Debug.Log("Landed successfully!");    
    }

}
    



