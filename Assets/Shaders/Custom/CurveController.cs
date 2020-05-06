using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurveController : MonoBehaviour
{
 
    public Transform CurveOrigin;
 
    [Range(-500f, 500f)]
    [SerializeField]
    float x = 0f;
 
    [Range(-500f, 500f)]
    [SerializeField]
    float y = 0f;
 
    [Range(0f, 50f)]
    [SerializeField]
    float falloff = 0f;

    private float toX=0.0f;
    private float toY=0.0f;
 
    private Vector2 bendAmount = Vector4.zero;
 
    // Global shader property ids
    private int bendAmountId;
    private int bendOriginId;
    private int bendFalloffId;
 
    void Start ()
    {
        bendAmountId = Shader.PropertyToID("_BendAmount");
        bendOriginId = Shader.PropertyToID("_BendOrigin");
        bendFalloffId = Shader.PropertyToID("_BendFalloff");

        InvokeRepeating("ChangeRoad",4.0f,10.0f);
        
    }
    
    void Update ()
    {


        
        /*
        if (x<toX){x=x+1;} if (x>toX){x=x-1;}
        if (y<toY){y=y+1;} if (y>toY){y=y-1;}
        */
        
        x=Mathf.Lerp(x,toX,0.02f);
        y=Mathf.Lerp(y,toY,0.02f);

        bendAmount.x=x;
        bendAmount.y=y;

        Shader.SetGlobalVector(bendAmountId, bendAmount);
        Shader.SetGlobalVector(bendOriginId, CurveOrigin.position);
        Shader.SetGlobalFloat(bendFalloffId, falloff);

    }

    
    
   
    
    void ChangeRoad(){
        if (!GameManager.playerDead){
            toX=Random.Range(-25.0f,25.0f);
            toY=Random.Range(-15.0f,15.0f);
        }

    }

   

        
}