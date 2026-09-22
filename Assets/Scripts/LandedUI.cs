using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandedUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI titleTextMesh;                   // drag Title text - shows SUCCESS or CRASH
    [SerializeField] private TextMeshProUGUI statsTextMesh;                   // drag Stats text - shows speed, angle, x, score
    [SerializeField] private Button nextButton ;                              // Reference to the UI Button (Next / Restart).


    private void Awake(){

        nextButton.onClick.AddListener ( ()=> {                              // nextButton.onClick = UnityEvent that fires when player clicks button and this event is used for click logics. AddListener() = Subscribe - tell button what function to call on click. () => {... } = lambda function - anonymous/short form function with no name,no parameters and written in one line. Using lambda function because restart logic is only 1 line and only used by this button,so Use lambda when function is small, used only ONCE, and you don't want to make a separate named method.If we dont create lambda the we do this :- "nextButton.onClick.AddListener(RestartGame); n void RestartGame() { SceneManager.LoadScene(0); }
           SceneManager.LoadScene(0) ;                                       // LoadScene(0) = Load scene at Build Index 0 (your first Lander level)
        }) ;    
    }

    private void Start(){
        Lander.Instance.OnLanded += Lander_OnLanded ;

        Hide() ;                                                              // hide banner at start of game - OFF by default.'gameObject.SetActive(false)' Must be in Start() not Awake() - if Hide() is inside Awake() then Start() never runs -> never subscribes -> banner never shows.
    }
    private void Lander_OnLanded(object sender,Lander.OnLandedEventArgs e) {
        
        if (e.landingType == Lander.LandingType.success){                         
            titleTextMesh.text = "SUCCESSFUL LANDING!" ;                       // here the success enum is identified beacause it was the LandingType declared in lander.cs when lander lands successfully.
        }
        if (e.landingType == Lander.LandingType.crashedOnTerrain){            // here the crashedOnTerrain enum is identified beacause it was the LandingType declared in lander.cs when lander lands on terrain.
            titleTextMesh.text = "CRASHED!" ;
        }
        if (e.landingType == Lander.LandingType.toohardLanding){              // here the tooHardLandins enum is identified beacause it was the LandingType declared in lander.cs when lander lands too hard.
            titleTextMesh.text = "LANDED TOO HARD!" ;
        }
        if (e.landingType == Lander.LandingType.tooSteepAngle){               // here the tooSteepAngle enum is identified beacause it was the LandingType declared in lander.cs when lander lands on a too steep angle.
            titleTextMesh.text = "LANDED TOO STEEP!" ;
        }

        statsTextMesh.text =                                                 // Fill stats text with multiple data(landingSpeed,landingAngle,scoreMultiplier,finalScore) transfered here as event args from Lander.cs
                            Mathf.Round(e.landingSpeed * 2f) + "\n" +
                            Mathf.Round(e.landingAngle * 100f) + "\n" +
                            "x" + e.scoreMultiplier + "\n" +
                            e.finalScore ;

        Show() ;                                                             // turn ON banner when this event(OnLanded event) is subcribed.
    }
    private void Show(){
        gameObject.SetActive(true);                                          // enable this panel GameObject - makes banner visible
    }

    private void Hide(){
        gameObject.SetActive(false);                                         // disable panel - hides banner until next landing
    }
}
