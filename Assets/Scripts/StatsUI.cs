using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI ;

public class StatsUI : MonoBehaviour{

[SerializeField] private TextMeshProUGUI statsTextMesh ;                            // private but show in Inspector, drag Stats Text here, shows Score/Time/Speed
    [SerializeField] private GameObject speedUpArrowGameObject ;                    // drag Up Arrow icon, will show when speedY >= 0
    [SerializeField] private GameObject speedDownArrowGameObject ;                  // drag Down Arrow icon, will show when speedY < 0
    [SerializeField] private GameObject speedRightArrowGameObject ;                 // drag Right Arrow icon, will show when speedX >= 0
    [SerializeField] private GameObject speedLeftArrowGameObject ;                  // drag Left Arrow icon, will show when speedX < 0

    [SerializeField]private Image fuelImage;                                        // Image = Type. StatsUI knows "there is a class called Image that has fillAmount".Private but drag yellow Fuel Bar Image here, Image Type must be Filled for fillAmount to work. Image class holds image type(filled).It's a Component - UnityEngine.UI.Image - the yellow bar in your Canvas. The one that has fillAmount 0 to 1. So this Image component(which is also a class) hold a reference to that bar.When you set Type = Filled, Unity shows fillAmount slider 0 to 1. Your code fuelImage.fillAmount = 0.5f drives that slider.Image has 4 Image Type modes in Inspector(simple,sliced,tiled, filled.)Filled <- only this mode has fillAmount.

    private void Update(){                                                          // called every frame
        StatsTextMesh() ;                                                           // call our custom method every frame to update UI
    }

    private void StatsTextMesh(){                                                   // updates all stats UI

        speedUpArrowGameObject.SetActive(Lander.Instance.GetSpeedY() >= 0) ;        // if vertical speed >=0 = moving up, show Up arrow, hide Down
        speedDownArrowGameObject.SetActive(Lander.Instance.GetSpeedY() < 0) ;       // if vertical speed <0 = falling down, show Down arrow
        speedRightArrowGameObject.SetActive(Lander.Instance.GetSpeedX() >= 0) ;     // if horizontal speed >=0 = moving right, show Right arrow
        speedLeftArrowGameObject.SetActive(Lander.Instance.GetSpeedX() < 0) ;       // if horizontal speed <0 = moving left, show Left arrow

        fuelImage.fillAmount = Lander.Instance.GetFuelAmountNormalized() ;          // get 0-1 fuel from Lander singleton, set Image fillAmount, yellow fuel bar image reduces horizontally (because of fill amount property from 0 - 1) as fuel drains.

        statsTextMesh.text =                                                        // set TMP text to multi-line string
            GameManager.Instance.GetScore() + "\n" +                                // line 1: score from GameManager singleton
            Mathf.Round(GameManager.Instance.GetTime()) + "\n" +                    // line 2: time, Rounded to no decimal
            Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedX() * 10f) ) + "\n" +     // line 3: horizontal speed *10 for readability as large value, Round + Abs = no negative sign and deimal.
            Mathf.Abs(Mathf.Round (Lander.Instance.GetSpeedY() * 10f) ) ;           // line 4: vertical speed *10 for readability as large value, Abs to get only positive vlaues.
    }
}
