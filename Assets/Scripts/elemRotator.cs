using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class elemRotator : MonoBehaviour
{

    public float minSpeed=1.0f;
    public float maxSpeed=1.0f;

    public bool inverse=false;

    public bool allowRandInverse=true;

    private float speed=1.0f;

    private Vector3 spinVector=Vector3.forward;
    

    // Start is called before the first frame update
    void Start()
    {
        speed=Random.Range(minSpeed,maxSpeed);
        if(allowRandInverse){
            if (Random.Range(0,10)>5){
                spinVector=Vector3.back;
            }
        }else{
            if (inverse){
                spinVector=Vector3.back;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
           transform.Rotate(spinVector * Time.deltaTime*speed);    
    }
}
