using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI ;

public class StatsUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI statsTextMesh ;
    [SerializeField] private GameObject speedUpArrowGameObject ;
    [SerializeField] private GameObject speedDownArrowGameObject ;
    [SerializeField] private GameObject speedRightArrowGameObject ;
    [SerializeField] private GameObject speedLeftArrowGameObject ;

    [SerializeField]private Image fuelImage;                          // There is a class called Image that has fillAmount property .For UI bars like Fuel, Health, Loading - there is only ONE class "Image" that has fillAmount. Image has 4 Image Type modes(simpe,sliced,tiled,filled) in Inspector.Filled <- only this mode has fillAmount.When you set Type = Filled, Unity shows fillAmount slider 0 to 1. Your code fuelImage.fillAmount = 0.5f drives that slider.Slider component = actually uses Image (with fillAmount inside). Slider is just a wrapper.If you don't set Type = Filled in Inspector, fillAmount line does nothing - bar stays full.


    private void Update(){
        StatsTextMesh() ;
    } 

    private void StatsTextMesh(){

        speedUpArrowGameObject.SetActive(Lander.Instance.GetSpeedY() >= 0) ; 
        speedDownArrowGameObject.SetActive(Lander.Instance.GetSpeedY() < 0) ; 
        speedRightArrowGameObject.SetActive(Lander.Instance.GetSpeedX() >= 0) ; 
        speedLeftArrowGameObject.SetActive(Lander.Instance.GetSpeedX() < 0) ;

        fuelImage.fillAmount = Lander.Instance.GetFuelAmountNormalized() ;

        statsTextMesh.text =
                            GameManager.Instance.GetScore() + "\n" + 
                            Mathf.Round(GameManager.Instance.GetTime() ) + "\n" + 
                            Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedX() * 10f) ) + "\n" + 
                            Mathf.Abs(Mathf.Round (Lander.Instance.GetSpeedY() * 10f) ) ;                    
    }
}
