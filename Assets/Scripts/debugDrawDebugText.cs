using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class debugDrawDebugText : MonoBehaviour
{


    private Text debugText;
    // Start is called before the first frame update
    void Start()
    {
        debugText = GetComponent<Text>() as Text;
                
    }

    // Update is called once per frame
    void Update()
    {


		if (GameManager.drawDebug){
			debugText.text="DEBUG INFO\n";
			debugText.text+="----------------\n";
			debugText.text+="In Cinematic   : "+GameManager.inCinematic.ToString()+"\n";
			debugText.text+="UseGyro: "+GameManager.useGyro.ToString()+"\n";
			debugText.text+="Miniboss Active: "+GameManager.minibossActive.ToString()+"\n";
			debugText.text+="CustomString1: "+GameManager.CustomString1+"\n";
			
		}else{
			debugText.text=" ";
		}

        
    }
}
