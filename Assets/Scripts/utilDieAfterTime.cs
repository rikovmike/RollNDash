using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utilDieAfterTime : MonoBehaviour {

	public float timeout;
	// Use this for initialization
	void Start () {
		Invoke("doDie",timeout);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void doDie(){
		Destroy(gameObject);
	}

}
