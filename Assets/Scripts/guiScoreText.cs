using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guiScoreText : MonoBehaviour
{

    public enum VariableCheck {Score,Crystals};
    public VariableCheck varToDisplay;
    private Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myText=GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (varToDisplay == VariableCheck.Score){
            myText.text = GameManager.score.ToString();
        }
        if (varToDisplay == VariableCheck.Crystals){
            myText.text = GameManager.crystals.ToString();
        }


    }
}
