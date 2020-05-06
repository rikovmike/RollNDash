using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animValveDonutControl : MonoBehaviour
{

    Animator animator;

    public float speed=0.0f;
    public float offset=0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (speed==0.0f){
            speed=Random.Range(0.1f,1.0f);
        }
        if (offset==0.0f){
            offset=Random.Range(0.0f,0.5f);
        }
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed",speed);
        animator.SetFloat("Offset",offset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
