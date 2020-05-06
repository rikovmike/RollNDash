using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backdropCamera : MonoBehaviour
{

    private Transform playerTransform;
	private float playerLastZ=0.0f;
	private float playerSpeed;
	private Vector3 moveVector;
    
        // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    
    

	//	if (playerTransform.position.y>0){
			transform.Rotate(new Vector3(0.0f, 0.0f, GameManager.controlX/3));
	//	}

		playerSpeed = playerTransform.position.z-playerLastZ;
		playerLastZ=playerTransform.position.z;
		
		moveVector = transform.position;


		moveVector.z = transform.position.z+playerSpeed/1000;
		moveVector.x=transform.position.x;
		moveVector.y=transform.position.y;

	//	transform.position = moveVector;
    
    
    
    
    }
}
