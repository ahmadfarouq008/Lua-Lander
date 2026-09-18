using TMPro;
using UnityEngine;

public class LandedUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI titleTextMesh ;
    [SerializeField] private TextMeshProUGUI statsTextMesh ;

    private void Start(){
        Lander.Instance.OnLanded += Lander_OnLanded ;

        Hide() ;        
    }
    private void Lander_OnLanded(object sender,Lander.OnLandedEventArgs e) {
        
        if (e.landingType == Lander.LandingType.success){
            titleTextMesh.text = "SUCCESSFUL LANDING!" ;
        }
        if (e.landingType == Lander.LandingType.crashedOnTerrain){
            titleTextMesh.text = "CRASHED!" ;
        }
        if (e.landingType == Lander.LandingType.toohardLanding){
            titleTextMesh.text = "LANDED TOO HARD!" ;
        }
        if (e.landingType == Lander.LandingType.tooSteepAngle){
            titleTextMesh.text = "LANDED TOO STEEP!" ;
        }

        statsTextMesh.text =
                            Mathf.Round(e.landingSpeed * 2f) + "\n" +
                            Mathf.Round(e.landingAngle * 100f) + "\n" +
                            "x" + e.scoreMultiplier + "\n" +
                            e.finalScore ;

        Show() ;                    
    }
    private void Show(){

        gameObject.SetActive(true) ;
    }
    private void Hide(){

        gameObject.SetActive(false) ;
    }
}
