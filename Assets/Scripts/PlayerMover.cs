using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {

	private CharacterController controller;
	private GameObject myBody;

	public AudioClip pickupAudio;
	public AudioClip deathAudio;
	public AudioClip turboAudio;
	public AudioClip tileExplodeAudio;

	private AudioSource myAudioSource;
	public  GameObject ffxTakenCrystal;
	public float speed = 15.0f;
	private float targetSpeed=0.0f;
	private float totalSpeed = 0.0f;
	private float turnSpeed = 1.0f;

	public float mouseSensivity = 150.0f;
	public float touchSensivity = 15.0f;

	public float gyroSensivity = 1.0f;

	private bool turboMode=false;

	public float turboSpeed=55.0f;

	private Vector3 moveVerctor ;
	
	private Vector3 currentRotation;

	public GameObject myTurboPrefab;
	public GameObject myExplodePrefab;

	public GameObject myTileExplodePrefab;

	private GameObject myTurbo;
	private GameObject myExplode;

	private bool finished=false;

	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		moveVerctor = transform.position;
		
		

		if (GameManager.playerDead||GameManager.playerFinish){
			totalSpeed=Mathf.Lerp(totalSpeed,0,0.1f);
			
		}else{

			totalSpeed=Mathf.Lerp(totalSpeed,(speed),0.005f);



				float pointer_x = Input.GetAxis("Mouse X");

				turnSpeed=mouseSensivity;
				
				if (GameManager.useGyro){
					turnSpeed=gyroSensivity;
					pointer_x = -Input.acceleration.x;
				}else{
					if (Input.touchCount > 0)
					{
						turnSpeed=touchSensivity;
						pointer_x = Input.touches[0].deltaPosition.x;
					}
				}

				GameManager.controlX=pointer_x * Time.deltaTime * turnSpeed;

			transform.Rotate(new Vector3(0.0f, 0.0f, GameManager.controlX));



		}

		moveVerctor.z=moveVerctor.z+totalSpeed*Time.deltaTime;

		transform.position=moveVerctor;

		GameManager.levelPassed=transform.position.z;
		
	}

    void OnTriggerEnter(Collider col){





        if (col.gameObject.tag == "FINISH"&&GameManager.playerFinish==false)
        {
			Invoke("DoRestart",1.0f);
			GameManager.playerFinish=true;
		}



        if (col.gameObject.tag == "DEATH")
        {
			

			if (!turboMode){
					if (!GameManager.playerDead){
						GameManager.playerDead=true;
						myAudioSource.PlayOneShot(deathAudio);
						throwEffect();
					
						GameManager.GameOver();
					}
			}else{
				GameObject go;
				go = Instantiate (myTileExplodePrefab) as GameObject;
				go.transform.SetParent(transform,false);
						myAudioSource.PlayOneShot(tileExplodeAudio);

			}


        }


        if (col.gameObject.tag == "bonus")
        {
			if ((GameManager.score+1)%20==0){
				GameManager.earnCrystal();
				throwCrystalEffect();
			}
			Destroy(col.gameObject);
			GameManager.score+=1;
			myAudioSource.PlayOneShot(pickupAudio);

		}

        if (col.gameObject.tag == "bonusT")
        {
			if (!turboMode){
				turboMode=true;
				totalSpeed=turboSpeed;
				Destroy(col.gameObject);
				myAudioSource.PlayOneShot(turboAudio); 

				myTurbo = Instantiate (myTurboPrefab) as GameObject;
				myTurbo.transform.SetParent(transform,false);
		
				Invoke("turnOffTurbo",5);
			}

		}


    }



void turnOffTurbo(){
	turboMode=false;
	Destroy(myTurbo);
	totalSpeed=(speed)/3;
}

void throwEffect(){
	myExplode = Instantiate (myExplodePrefab) as GameObject;
	myExplode.transform.SetParent(transform,false);
}


void throwCrystalEffect(){

				GameObject fire;
				fire = Instantiate (ffxTakenCrystal) as GameObject;
				fire.transform.SetParent(transform,false);

}

void DoRestart(){
	GameManager.finishLevel();
}


}
