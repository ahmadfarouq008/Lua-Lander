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

        //now in this space of code line, we need to identify where the lander collides (landing pad or tearrain) so we create an empty landingPad.cs script(which just includes landing pad class) to identify the landing pad game object, so we want two outputs from this below function thats why we use 'out'(to get more then one output) .Wheather the thing that had collision with the lander contains 'LandingPad' script or not , if not then we get Debug.log/msg "lander crashed on terrain" but if it does contain 'LandingPad' script then we get the second output which is the reference to that landing pad game object and we store it in a variable called 'landingPad' (which is of type LandingPad class) and we can use this variable to access the properties of that landing pad game object in future (e.g when we will use multiple landing pads in future).
 // Because C# methods can only return ONE thing, but TryGetComponent(function that finds landing pad script + did it found.) needs to return TWO things(thats why we use 'out').It needs to return:
//1. bool - Did I find it? true/false (through return) --> if terrain then false and out function will not be invoked and below point 2 will not work, just "crash on terrain" is returned and we can not use the variable 'landingPad' to access the properties of that landing pad game object in future.
//2. LandingPad - The actual component (through 'out' with parameter ) -->  if landingpad then true and out function will be invoked and this point 2 will work and we can use the variable 'landingPad' to access the properties of that landing pad game object in future. 


        if (!collision.gameObject.TryGetComponent(out LandingPad landingPad)) {
            Debug.Log("Crashed on the Terrain!!") ;
            return ;
        }

        // now in this space of code line, we get the argument input(speed/hit data), when lander falls and this function is invoked automatically by untiy and argument input (that we got in unity) is stored in collision(parameter) as writen in below line "if" code. 
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
    



