using TMPro;
using UnityEngine;

public class LandingPadVisuals : MonoBehaviour{
    [SerializeField] private TextMeshPro scoreMultiplierText ;              // refers to the text(it's the typing area inside text(TMP) inspector where we write score multiplier values like x1 or x5.) that will be displayed on the landing pad, showing the score multiplier.And also in Hierarchy drag its child Text (TMP) and drop it into Score Multiplier Text slot on the right because the LandingPadVisuals script dont know where the text(TMP) (which is children of LandingPad(behaviour) prefab) is, so we need to tell it where the text is by dragging and dropping it into the slot on the right as referrence. 
                                                                            // TextMeshPro is on the CHILD object. GetComponent only searches same object, not children. So Unity doesn't know which text you mean - there could be 100 texts in scene.You can avoid drag if you want, with this code inside awake():- "" scoreMultiplierTextMesh = GetComponentInChildren<TextMeshPro>(); ""
    private void Awake() {                                                  // Runs once when game starts, before player even moves, It sets the text early.

        LandingPad landingPad = GetComponent <LandingPad>() ;               // Because this LandingPadVisuals.cs script dont know where is landingpad.cs and what is inside it so this code line Finds LandingPad.cs on this same pad object and store it in variable landingPad.i.e store the code of LandingPad.cs script in variable landingPad so that we can access the properties of LandingPad.cs script in below line of code.

        scoreMultiplierText.text = "x" + landingPad.GetScoreMultiplier() ;  // .text is property of TextMeshPro. "x" + 5 = "x5" string in C#. So if you change multiplier to 10 in Inspector, text auto becomes x10.
        
    }

}
