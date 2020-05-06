using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setMyColorToLvlColor : MonoBehaviour
{

    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        float H, S, V;
        Color.RGBToHSV(GameManager.levelColor, out H, out S, out V);
        S=0.5f;
        V=V-0.4f;
        rend = GetComponent<Renderer> ();
        rend.material.color = Color.HSVToRGB(H, S, V);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
