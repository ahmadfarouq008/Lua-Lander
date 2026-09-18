using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {

    public static Lander Instance { get; private set; }                            // Singleton pattern to ensure that there is only one instance of the Lander class in the scene, which can be accessed globally through the Instance property. This allows other scripts to easily access the Lander instance and subscribe to its events or call its methods without needing a direct reference to the Lander object. Main purpose -> It removes drag & drop , Guarantee only ONE(Lander as singleton , not its copies as lander 1 and lander 2, that's why its static.) exists and Global access by lander.instance. with events subscription. Singleton is made by 'static' keyword. { get; private set; } --> anyone can READ Lander.Instance, but only Lander class itself can SET it. Inside Awake() { Instance = this; } is allowed, outside is blocked, this is encapsulation. Summary: static itself is allowed with 2 landers by which you can have public static int totalLanders; to count how many landers exist. But public static Lander Instance that holds ONE lander only works when you guarantee there is only ONE Lander.That's why seniors say: Only Managers should be Singleton, Player should NOT. GameManager, AudioManager = Singleton. Lander, Enemy, Bullet = NOT Singleton.That is the whole singleton trick: one static field holds only one instance.Hence static means Lander belongs to the CLASS itself, not to any object. There is only ONE memory slot for Lander.Instance for the whole game.


                                                                                  // Events declared for thruster forces, which can be subscribed to by other scripts (like LanderVisuals) to react to these forces being applied :
    public event EventHandler OnUpForce ;
    public event EventHandler OnRightForce ;
    public event EventHandler OnLeftForce ;
    public event EventHandler OnBeforeForce ;
    
    public event EventHandler OnCoinPickUp ;                                      // Event declared for coin pickup, which can be subscribed to by other scripts (like GameManager) to react to the coin being picked up.
    
    public event EventHandler <OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs : EventArgs {                                  // This custom OnLandedEventArgs class that belongs event arguments is used to pass additional data (the score) when the OnLanded event is triggered. It inherits from EventArgs, which is a base class for classes containing event data. The score property will hold the final score calculated based on the landing conditions.
        public LandingType landingType ;
        public int finalScore ;
        public float landingSpeed ;
        public float landingAngle ;
        public float scoreMultiplier ;
    }
    public enum LandingType {
        success,
        crashedOnTerrain,
        tooSteepAngle,
        toohardLanding,
    }

    private Rigidbody2D LanderRigidbody2D ;
    private float fuelAmount ;                                                     // current fuel left in tank, will go 10 -> 0
    private float fuelAmountMax = 10f ;                                            // max capacity of tank, 10 units

    private void Awake() {

        LanderRigidbody2D = GetComponent<Rigidbody2D>() ;

        Instance = this ;                                                          // Assign the current instance of the Lander class to the static Instance property, allowing global access to this instance. 'this' = the real Lander GameObject in your scene. Now the static CLASS slot points to the one real INSTANCE in scene.
        
        fuelAmount = fuelAmountMax ;                                               // Awake/Start: fill tank full on game start, 10 = 10
    }
    private void FixedUpdate(){
        OnBeforeForce?.Invoke(this, EventArgs.Empty) ;                             // fire off / invoke the OnBeforeForce event before checking for any thruster forces being applied. This allows any subscribers/listeners to prepare for the upcoming forces.
             
        
        if (fuelAmount <= 0f){                                                     // if fuel is 0 or less then we dont want to apply any force so we just return(the funtion does not do any work) and lander stops working.
            return;    
        }

        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.leftArrowKey.isPressed || Keyboard.current.rightArrowKey.isPressed) {        // if any of the arrow keys are pressed either simultaneously or individually, the fuel is consumed per second and we call the ConsumeFuel() function to decrease the fuel amount.
            
            ConsumeFuel();                                             
        }

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

        //now in this space of code line, we need to identify where the lander collides (landing pad or terrain) so we create an empty landingPad.cs script(which just includes landing pad class) to identify the landing pad game object, so we want two outputs from this below function thats why we use 'out'(to get more then one output) .Wheather the thing that had collision with the lander contains 'LandingPad' script or not , if not then we get Debug.log/msg "lander crashed on terrain" but if it does contain 'LandingPad' script then we get the second output which is the reference to that landing pad game object and we store it in a variable called 'landingPad' (which is of type LandingPad class) and we can use this variable to access the properties of that landing pad game object in future (e.g when we will use multiple landing pads in future).
 // Because C# methods can only return ONE thing, but TryGetComponent(function that finds landing pad script + did it found.) needs to return TWO things(thats why we use 'out').It needs to return:
//1. bool - Did I find it? true/false (through return) --> if terrain then false and out function will not be invoked and below point 2 will not work, just "crash on terrain" is returned and we can not use the variable 'landingPad' to access the properties of that landing pad game object in future.
//2. LandingPad - The actual component (through 'out' with parameter ) -->  if landingpad then true and out function will be invoked and this point 2 will work and we can use the variable 'landingPad' to access the properties of that landing pad game object in future. 


        if (!collision.gameObject.TryGetComponent(out LandingPad landingPad)) {
            Debug.Log("Crashed on the Terrain!!") ;

            OnLanded?.Invoke(this, new OnLandedEventArgs {
            landingType = LandingType.crashedOnTerrain,
            
            landingSpeed = 0f,
            landingAngle = 0f,
            scoreMultiplier = 0f,
            finalScore = 0,                                                       
        }) ;
            return ;
        }

        // now in this space of code line, we get the argument input(speed/hit data), when lander falls and this function is invoked automatically by untiy and argument input (that we got in unity) is stored in collision(parameter) as writen in below line "if" code. 
        float softLandingVelocityMagnitude = 5f ;                                 //  softLandingVelocityMagnitude means max allowed speed.
        float relativeVelocityMagnitude = collision.relativeVelocity.magnitude ;  // relativeVeloctyMagnitue means landing speed value.
        if (relativeVelocityMagnitude > softLandingVelocityMagnitude ){
        Debug.Log("Landed too hard!");

        OnLanded?.Invoke(this, new OnLandedEventArgs {
            landingType = LandingType.toohardLanding,
            
            landingSpeed = relativeVelocityMagnitude,
            landingAngle = 0f,
            scoreMultiplier = 0f,
            finalScore = 0,                                                       
        }) ;
        return ;
        }       
        
        float dotVector = Vector2.Dot(Vector2.up,transform.up) ;
        float minDotVector = .95f ;                                               //  dotVector means landing angle value.
        if (dotVector < minDotVector){
        Debug.Log("Landed on a too steep angle!") ;

        OnLanded?.Invoke(this, new OnLandedEventArgs {
            landingType = LandingType.tooSteepAngle,
            
            landingSpeed = 0f,
            landingAngle = dotVector,
            scoreMultiplier = 0f,
            finalScore = 0,                                                       
        }) ;
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
        
        OnLanded?.Invoke(this, new OnLandedEventArgs {
            landingType = LandingType.success,
            
            landingSpeed = relativeVelocityMagnitude,
            landingAngle = dotVector,
            scoreMultiplier = landingPad.GetScoreMultiplier(),
            finalScore = finalScore,                                                        // Invoke the OnLanded event and pass the final score as an argument to any subscribers/listeners that are interested in the landing event. Here we are creating new OnLandedEventArgs class and setting its score property to the calculated finalScore. This allows any subscribers to access the final score when they handle the OnLanded event.
        }) ;  
        //       |            |
        //       |            |--> (final multiplied score)
        //       |
        //       |---> (final score passed by event as argument )             
        //          
    }

    private void OnTriggerEnter2D(Collider2D collider) {                          // Beacause we want to pick up fuel when we collide with fuel pickup object with 'is trigger option' checked, so we use OnTriggerEnter2D function which is invoked automatically by unity when lander collides with fuel pickup object and we get the argument input (collider variable stores the data that something is collided with fuel game object) which is stored in collider variable (parameter) as writen in below line "if" code. 
        if (collider.gameObject.TryGetComponent(out FuelPickUp fuelPickup)) {     // the TryGetComponent function checks/identifies if the collided object has FuelPickUp script attached to it or not, if yes then it returns true and we get the reference to that fuel pickup game object(i.e. it is idendified that we are collided with fuel game object) and we store it in a variable called 'fuelPickup' (which is of type FuelPickUp class) and we can use this variable to access the properties of that fuel pickup game object in future (e.g when we will use multiple fuel pickups in future). If no then it returns false and we do nothing.
            
            float addFuelAmount = 10f ;
            fuelAmount += addFuelAmount ;

            if (fuelAmount > fuelAmountMax) {                                    // check: did we overfill? if already the fuel bar is 80% filled amd we get the fuel then we get fuel amount of 180% which we donot want, i.e. 8 + 10 = 18 > 10
                fuelAmount = fuelAmountMax ;                                     // set back to max, no overflow, stays 10 (100% on fuel pick up.)
            }
            fuelPickup.DestroySelf() ;                                            // we call the DestroySelf() function in FuelPickUp.cs public file to destroy the fuel pickup game object after collision with lander. Here fuelPickup is used beacause it is the reference to that fuel pickup game object.
        }  
                          
        if (collider.gameObject.TryGetComponent(out CoinPickUp coinPickUp)) {     // the TryGetComponent function checks/identifies if the collided object has CoinPickUp script attached to it or not, if yes then it returns true and we get the reference to that coin pickup game object(i.e. it is idendified that we are collided with coin game object) and we store it in a variable called 'coinPickUp' (which is of type CoinPickUp class) and we can use this variable to access the properties of that coin pickup game object in future (e.g when we will use multiple coin pickups in future). If no then it returns false and we do nothing.    
            
            OnCoinPickUp?.Invoke(this, EventArgs.Empty);                          // Invoke the coin pickup event
            coinPickUp.DestroySelf() ;                                            // we call the DestroySelf() function in CoinPickUp.cs public file to destroy the coin pickup game object after collision with lander. Here coinPickUp is used beacause it is the reference to that coin pickup game object.
        }  
    }

    private void ConsumeFuel() {                                                  // By this function we are consuming/decreasing fuel amount per second.e.g. 2 sec = 2 unit of  fuel consumed.
        float fuelConsumptionAmount = 1f ;                                        // 1 unit of fuel per second
        fuelAmount -= fuelConsumptionAmount * Time.deltaTime ;                    // Time.deltaTime is used to make the fuel consumption frame-rate independent, ensuring consistent fuel usage regardless of the frame rate.          
    }

    public float GetFuel(){
        return fuelAmount ;
        
    }
    public float GetFuelAmountNormalized(){                                       // gives UI a 0-1 value, Normalized means conversion into 0 - 1 range. Image.fillAmount ONLY understands 0-1. e.g. fuelAmount = 5, max = 10 => 5/10 = 0.5 = 50% , here the bar will be 50% filled. As the fuel amount decreaes, the values comes in this function and get normalized so on until fuel amount becomes 0.  
        return fuelAmount / fuelAmountMax ;
        
    }

    public float GetSpeedX(){ 
        return LanderRigidbody2D.linearVelocityX ;   
    }

    public float GetSpeedY(){ 
        return LanderRigidbody2D.linearVelocityY ;   
    }

}
    



