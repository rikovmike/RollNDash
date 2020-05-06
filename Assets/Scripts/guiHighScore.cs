using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guiHighScore : MonoBehaviour
{

    private Text myText;
    private GameObject myTextNew;
   

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.text="Hi:"+GameManager.hiscore;
        
        
    }
}
