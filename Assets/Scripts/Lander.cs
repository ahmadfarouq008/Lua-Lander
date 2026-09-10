using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {

    // Events declared for thruster forces, which can be subscribed to by other scripts (like LanderVisuals) to react to these forces being applied :
    public event EventHandler OnUpForce ;
    public event EventHandler OnRightForce ;
    public event EventHandler OnLeftForce ;
    public event EventHandler OnBeforeForce ;

    private Rigidbody2D LanderRigidbody2D ;

    private void Awake() {

        LanderRigidbody2D = GetComponent<Rigidbody2D>() ;
    }
    private void FixedUpdate(){
        OnBeforeForce?.Invoke(this, EventArgs.Empty) ;                             // fire off / invoke the OnBeforeForce event before checking for any thruster forces being applied. This allows any subscribers/listeners to prepare for the upcoming forces.

        if (Keyboard.current.upArrowKey.isPressed ){

            float force = 700f ;
            LanderRigidbody2D.AddForce(force * transform.up * Time.deltaTime);

            OnUpForce?.Invoke(this, EventArgs.Empty) ;                             // fire off / invoke the OnUpForce event when the up arrow key is pressed and the upward force is applied. This allows any subscribers/listeners to react to the upward force being applied.
        }
        if (Keyboard.current.rightArrowKey.isPressed){

            float turnSpeed = -100f ;
            LanderRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);

            OnRightForce?.Invoke(this, EventArgs.Empty) ;                          // fire off / invoke the OnRightForce event when the right arrow key is pressed and the rightward torque is applied. This allows any subscribers/listeners to react to the rightward force being applied.
        }
        if (Keyboard.current.leftArrowKey.isPressed){

            float turnSpeed = +100f ;
            LanderRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);

            OnLeftForce?.Invoke(this, EventArgs.Empty) ;                           // fire off / invoke the OnLeftForce event when the left arrow key is pressed and the leftward torque is applied. This allows any subscribers/listeners to react to the leftward force being applied.
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
        float softLandingVelocityMagnitude = 5f ;                                 //  softLandingVelocityMagnitude means max allowed speed.
        float relativeVelocityMagnitude = collision.relativeVelocity.magnitude ;  // relativeVeloctyMagnitue means landing speed value.
        if (relativeVelocityMagnitude > softLandingVelocityMagnitude ){
        Debug.Log("Landed too hard!");
        return ;
        }       
        
        float dotVector = Vector2.Dot(Vector2.up,transform.up) ;
        float minDotVector = .95f ;                                               //  dotVector means landing angle value.
        if (dotVector < minDotVector){
        Debug.Log("Landed on a too steep angle!") ;
        return;
        }

        Debug.Log("Landed successfully!");

        // Calculation of score based on landing angle and landing speed.
        
        // ANGLE SCORE - how straight you are. (1 = perfect up, 0 = sideways, -1 = upside down)
        float maxScoreLandingAngle = 100f ;    // 0.97 * 100 = 97
        float angleScore = Mathf.Clamp01(dotVector) * maxScoreLandingAngle ; // Clamp01 means if dotVector is less than 0(e.g -0.3) then it will become 0 and if it is more than 1(which cant be because straight tip = 1) then it will be 1 .

        // SPEED SCORE - how slow you are.  
        float maxScoreLandingSpeed = 100f ;   // Speed 2 -> 2/5=0.4 -> 1-0.4=0.6 -> 0.6*100 = 60 score
        float speedScore = ( 1f - relativeVelocityMagnitude / softLandingVelocityMagnitude ) * maxScoreLandingSpeed ;   // 1f means 100% (Human language ) = 1.0(Computer language) i.e. How much speed is LEFT from 1 (invert) and then we get point(.) somthing value which is then multiplied with 100 to get score in 0-100.(ones to <= hundreds) range.

        // AVERAGE SCORE - average of angle and speed scores.
        float averageScore  = (angleScore + speedScore) / 2f ;

        // Final Multiplied Score - average score multiplied by the landing pad's score multiplier. e.g. if average score = 80 and landing pad multiplier = 5x then final score =  400 points.
        int finalScore = Mathf.RoundToInt(averageScore * landingPad.GetScoreMultiplier()) ; // landingPad.GetScoreMultiplier() asks that specific pad (by the help of landingPad variable) "what is your value?"(this function calls the GetScoreMultiplier() in LandingPad.cs ) -> returns the score multiplier for that specific landing pad.

        
        Debug.Log( $" Angle Score: {angleScore:F0}/100 | Speed Score: {speedScore:F0}/100 | Final Score: {averageScore:F0} x {landingPad.GetScoreMultiplier()} = {finalScore} Points." ) ;  // landingPad.GetScoreMultiplier() means(returns) scoreMultiplier value.
    }

}
    



