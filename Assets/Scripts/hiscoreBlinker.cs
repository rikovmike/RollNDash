using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class hiscoreBlinker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

IEnumerator Flash()
    {
        Text myText;
        myText =  GetComponent<Text>();

        for (int n = 0; n < 2000; n++)
        {
            myText.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            myText.color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
    }


}
