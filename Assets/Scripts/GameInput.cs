using UnityEngine; 

public class GameInput : MonoBehaviour {                          //  Created a seprate code file for controls input which is known as Input System wrapper so Lander.cs never touches Keyboard directly.Get Free Gamepad/Controller buttons - LanderUp action has Gamepad binding too, no extra code.Rebindable - Player can change W to Space in options, Lander code never changes.Decoupled - Want mobile joystick later? Change only GameInput.cs, not Lander.cs.No magic strings - Keyboard.current.wKey is string-like, InputActions is type-safe
    public static GameInput Instance { get; private set; } 

    private InputActions inputActions;                            // InputActions = the C# class auto-generated from your InputActions.inputactions asset file. It contains your Player Map + LanderUp/Left/Right Actions + all bindings.Acts as initialization to input system as null at this point(and we will add to it the mapping and binding we did in inspector's edit, in upcoming code lines by using 'new'), just a box, no object inside yet.

    private void Awake() {                                   

        Instance = this;                                        

        inputActions = new InputActions();                      // Create new instance of InputActions - loads the Player Map + LanderUp/Left/Right actions at every start of game. Without new, inputActions is null -> inputActions.Enable() would crash NullReferenceException.After new, inputActions now holds all your bindings you set in editor - W, UpArrow, A, D etc.We cretae this because GameInput.cs dont know InputActions file and we store it as a new data/Instanc inside inputAction variable.  

        inputActions.Enable();                                  // IMPORTANT - Enable the Action Map, if you don't Enable, IsPressed() will always be false
    }

    private void OnDestroy() {                                 // Called when this GameObject is destroyed / scene reloads
        inputActions.Disable();                                // Disable actions to free memory / stop listening - clean up, prevents ghost inputs
    }

    public bool IsUpActionPressed() {                        // Public helper - Lander calls this, doesn't know about InputActions asset. IsPressed() itself returns bool - pressed? then true, (returns it) otherwise false(no return).
        return inputActions.Player.LanderUp.IsPressed();     // Player = Action Map, LanderUp = Action you created (W + UpArrow + Gamepad South). IsPressed() = true if held
    }

    public bool IsLeftActionPressed() {                     // Helper for left rotation
        return inputActions.Player.LanderLeft.IsPressed();  // Checks A + LeftArrow + Gamepad Left Shoulder - all in one call, no Keyboard.current needed
    }

    public bool IsRightActionPressed() {                     // Helper for right rotation
        return inputActions.Player.LanderRight.IsPressed();  // Checks D + RightArrow + Gamepad Right Shoulder
    }
}