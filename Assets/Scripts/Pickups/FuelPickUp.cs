using UnityEngine;

public class FuelPickUp : MonoBehaviour{

    public void DestroySelf(){        // this function is called from Lander.cs file when lander collides with fuel pickup object. This function destroys the fuel pickup object after collision.
        Destroy(gameObject) ;
    }

  
}
