using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour {


	private Transform lookAt;
	private float lookAtY;
	private Vector3 startOffset;
	private Vector3 moveVector;
	private Vector3 lookVector;

	// Use this for initialization
	void Start () {
		lookAt = GameObject.FindGameObjectWithTag("Player").transform;
		lookAtY=lookAt.position.y;
	//	startOffset = transform.position - lookAt.position;
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	/*
		moveVector=lookAt.position + startOffset;

		moveVector.x=moveVector.x/1.3f;

		moveVector.y = Mathf.Clamp(moveVector.y,startOffset.y,5);
 

		if (lookAt.position.y>0){
			lookVector.y=lookAtY+4.5f;
		}else{
			lookVector.y = lookAt.position.y+4.5f;
		}


		transform.position = moveVector;
*/


		//lookVector=lookAt.position;

		//transform.LookAt(lookVector);



	}
}
