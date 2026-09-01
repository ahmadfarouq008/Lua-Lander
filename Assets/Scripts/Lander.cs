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
    
    // FLOW: [Unity] Lander hits Terrain -> [Unity] creates Collision2D arg with speed/hit data -> [Unity->Code] calls OnCollisionEnter2D(arg) param gets it -> [Code] if speed>4 log "hard" & return to Unity else if Dot(up,nose)<0.9 log "steep" & return to Unity else log "success" & return to Unity   
    private void OnCollisionEnter2D(Collision2D collision){
        // now in this space od code line, we get the argument input(speed/hit data), when lander falls and this function is invoked automatically by untiy and argument input (that we got in unity) is stored in collision(parameter) as writen in below line "if" code. 
        float softLandingVelocityMagnitude = 4f ;
        if (collision.relativeVelocity.magnitude > softLandingVelocityMagnitude ){
        Debug.Log("Landed too hard!");
        return ;
        }       
        
        float dotVector = Vector2.Dot(Vector2.up,transform.up) ;
        float minDotVector = .90f ;
        if (dotVector < minDotVector){
        Debug.Log("Landed on a too steep angle!") ;
        return;
        }

        Debug.Log("Landed successfully!");
    }

}
    



