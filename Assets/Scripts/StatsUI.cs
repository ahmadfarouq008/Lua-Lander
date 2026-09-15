using System;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI statsTextMesh ;
    [SerializeField] private GameObject speedUpArrowGameObject ;
    [SerializeField] private GameObject speedDownArrowGameObject ;
    [SerializeField] private GameObject speedRightArrowGameObject ;
    [SerializeField] private GameObject speedLeftArrowGameObject ;


    private void Update(){
        StatsTextMesh() ;
    } 

    private void StatsTextMesh(){

        statsTextMesh.text =
                            GameManager.Instance.GetScore() + "\n" + 
                            Mathf.Round(GameManager.Instance.GetTime() ) + "\n" + 
                            Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedX() * 10f) ) + "\n" + 
                            Mathf.Abs(Mathf.Round (Lander.Instance.GetSpeedY() * 10f) ) + "\n" + 
                            Mathf.Round(Lander.Instance.GetFuel())  ;


        speedUpArrowGameObject.SetActive(Lander.Instance.GetSpeedY() >= 0) ; 
        speedDownArrowGameObject.SetActive(Lander.Instance.GetSpeedY() < 0) ; 
        speedRightArrowGameObject.SetActive(Lander.Instance.GetSpeedX() >= 0) ; 
        speedLeftArrowGameObject.SetActive(Lander.Instance.GetSpeedX() < 0) ; 
    }
}
