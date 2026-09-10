using UnityEngine;

public class LanderVisuals : MonoBehaviour {

    // References to the particle systems for the left, middle, and right thrusters, which will be used to visually represent the thruster forces being applied to the lander:
    
    [SerializeField] ParticleSystem leftThrusterParticleSystem ;
    [SerializeField] ParticleSystem middleThrusterParticleSystem ;
    [SerializeField] ParticleSystem rightThrusterParticleSystem ;
    private Lander lander ;                                                                           // reference to the Lander script, which will be used to subscribe to the events declared in the Lander script and react to the thruster forces being applied.
    
    private void Awake(){

        lander = GetComponent<Lander>() ;                                                            // stores the lander script reference in the lander variable, which will be used to subscribe to the events declared in the Lander script and react to the thruster forces being applied.
      
                                                                                                     // subscribed/listened/got signaled by the all events declared in the Lander script, which will be invoked when the up,right,left arrow key is pressed and the upward, rightward, and leftward forces are applied. This allows the LanderVisuals script to react to the thruster forces being applied by enabling the particle systems for the left, middle, and right thrusters.
        lander.OnUpForce += lander_OnUpForce ;                                 
        lander.OnRightForce += lander_OnRightForce ;             
        lander.OnLeftForce += lander_OnLeftForce ;
        lander.OnBeforeForce += lander_OnBeforeForce ;
                                                                               
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);       
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);                       //  Invokes/calls the SetEnabledThrusterParticleSystem / Helper function to disable the emission of the particle systems for the left, middle, and right thrusters at the start of the game, which allows the LanderVisuals script to react to the thruster forces being applied by enabling the particle systems for the left, middle, and right thrusters when the up arrow key is pressed and the upward force is applied.
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, false);
    }
    private void lander_OnBeforeForce(object sender, System.EventArgs e) {                           // defination of the event handler/lander_OnBeforeForce function that will be called when the OnBeforeForce event is invoked in the Lander script. This method will disable the particle systems for the left, middle, and right thrusters before checking for any thruster forces being applied, which allows any subscribers/listeners to prepare for the upcoming forces.
       
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);                       // calls the helper function.
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, false);
    } 
    private void lander_OnUpForce(object sender, System.EventArgs e) {                               // defination of the event handler/lander_OnUpForce function that will be called when the OnUpForce event is invoked in the Lander script. This method will enable the particle systems for the left, middle, and right thrusters when the up arrow key is pressed and the upward force is applied.

        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, true);                        // calls the helper function.
        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, true);
    }
    private void lander_OnRightForce(object sender, System.EventArgs e) {                            // defination of the event handler/lander_OnRightForce function that will be called when the OnRightForce event is invoked in the Lander script. This method will enable the particle systems for the left thruster when the right arrow key is pressed and the rightward force is applied.

        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);                          // calls the helper function.
    }
    private void lander_OnLeftForce(object sender, System.EventArgs e) {                             // defination of the event handler/lander_OnLeftForce function that will be called when the OnLeftForce event is invoked in the Lander script. This method will enable the particle systems for the right thruster when the left arrow key is pressed and the leftward force is applied.

        SetEnabledThrusterParticleSystem(rightThrusterParticleSystem, true);                         // calls the helper function.
    }
    private void SetEnabledThrusterParticleSystem(ParticleSystem particleSystem, bool enabled) {     // Helper function defined to enable or disable the emission of a particle system, which is used to visually represent the thruster forces being applied to the lander.
        
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission ;                     // EmissionModule means the emission of particles from the particle system, which is used to visually represent the thruster forces being applied to the lander. The emission module is a property of the particle system that controls how particles are emitted from the particle system. The emission module has a property called "enabled" that can be set to true or false to enable or disable the emission of particles from the particle system. The SetEnabledThrusterParticleSystem function takes a ParticleSystem and a bool as parameters and sets the enabled property of the emission module of the particle system to the value of the bool parameter. This allows the LanderVisuals script to react to the thruster forces being applied by enabling or disabling the emission of particles from the particle systems for the left, middle, and right thrusters.
        emissionModule.enabled = enabled;  
    }
}