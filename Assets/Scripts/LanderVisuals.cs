using UnityEngine;

public class LanderVisuals : MonoBehaviour {
    [SerializeField] ParticleSystem leftThrusterParticleSystem ;
    [SerializeField] ParticleSystem middleThrusterParticleSystem ;
    [SerializeField] ParticleSystem rightThrusterParticleSystem ;
    

    private void Start() {

        ParticleSystem.EmissionModule emissionModule = leftThrusterParticleSystem.emission ;
        emissionModule.enabled = false;  

    }
}