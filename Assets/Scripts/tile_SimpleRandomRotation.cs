using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile_SimpleRandomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation=Quaternion.Euler(0.0f,0.0f,Random.Range(0.0f,360.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
